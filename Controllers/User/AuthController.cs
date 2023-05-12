// Namespace imports 
using Back_End_Dot_Net.Models;
using Back_End_Dot_Net.Data;
using Back_End_Dot_Net.DTOs;
// Library imports
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using AutoMapper;

namespace Back_End_Dot_Net.Controllers
{
    [Route("api/authenticate")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDBContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        [Route("get-all-user"), Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUser()
        {
            if (_dbContext.Users == null)
            {
                return NotFound();
            }

            var users = await _dbContext.Users
                .Where(user => user.Hide == false)
                .Select(user => _mapper.Map<GetAllUserDTO>(user))
                .ToListAsync();

            if (users == null)
            {
                return NotFound();
            }

            var response = new
            {
                Total = users.Count(),
                Data = users
            };

            return Ok(response);
        }

        [Route("register")]
        [HttpPost]
        public async Task<ActionResult<User>> Register(UserRegisterDTO userRegisterDto)
        {
            // Validate against schemas that define along with model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _dbContext.Users
                .SingleOrDefaultAsync(user => user.Email.ToLower() == userRegisterDto.Email.ToLower());
            if (existingUser != null)
            {
                var errorMessage = new ErrorResponse(ErrorTitle.ValidationTitle, ErrorStatus.BadRequest, ErrorType.Validation);
                errorMessage.AddError($"A user with email '{userRegisterDto.Email}' already exists.");

                return BadRequest(errorMessage);
            }

            // Convert string from user input --> byte[] to store in database
            byte[] password = Encoding.ASCII.GetBytes(userRegisterDto.Password);
            CreatePasswordHash(password, out byte[] passwordHashed, out byte[] passwordSalt);

            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
                Email = userRegisterDto.Email,
                Gender = userRegisterDto.Gender,
                UserName = userRegisterDto.UserName,
                Password = passwordHashed,
                PasswordSalt = passwordSalt,
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            var response = new
            {
                Status = 200,
                Message = "User registration successfully"
            };

            return user;
        }

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<User>> Login(UserLoginDTO loginDto)
        {
            var existingEmail = await _dbContext.Users
                .Where(user => user.Email.ToLower() == loginDto.Email.ToLower())
                .Select(user => user.Email)
                .SingleOrDefaultAsync();
            if (existingEmail == null)
            {
                var errorMessage = new ErrorResponse(ErrorTitle.ValidationTitle, ErrorStatus.BadRequest, ErrorType.Authentication);
                errorMessage.AddError("Email or Password in invalid");

                return BadRequest(errorMessage);
            }

            var existingUser = await _dbContext.Users
                .Where(user => user.Email.ToLower() == loginDto.Email.ToLower())
                .SingleOrDefaultAsync();
            if (!VerifyPasswordHash(loginDto.Password, existingUser.Password, existingUser.PasswordSalt))
            {
                var errorMessage = new ErrorResponse(ErrorTitle.ValidationTitle, ErrorStatus.BadRequest, ErrorType.Authentication);
                errorMessage.AddError("Email or Password in invalid");

                return BadRequest(errorMessage);
            }

            string token = CreateToken(existingUser);

            var response = new
            {
                Status = "Success",
                Message = "Login successfully",
                Token = token
            };

            return Ok(response);
        }

        [Route("change-password")]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> ChangePassword(UserChangePasswordDTO model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest();
            }

            // Check if the new password is the same as the current password
            if (model.CurrentPassword == model.NewPassword)
            {
                var errorMessage = new ErrorResponse(ErrorTitle.ValidationTitle, ErrorStatus.BadRequest, ErrorType.Validation);
                errorMessage.AddError("New password cannot be the same as the current password.");

                return BadRequest(errorMessage);
            }

            // Retrieve the user from the database by ID
            var existingUser = await _dbContext.Users.FindAsync(new Guid(userId));
            if (existingUser == null)
            {
                return NotFound();
            }

            // Verify the current password provided by the user
            if (!VerifyPasswordHash(model.CurrentPassword, existingUser.Password, existingUser.PasswordSalt))
            {
                var errorMessage = new ErrorResponse(ErrorTitle.ValidationTitle, ErrorStatus.BadRequest, ErrorType.Authentication);
                errorMessage.AddError("Current password is incorrect.");

                return BadRequest(errorMessage);
            }

            // Generate new password hash and salt for the user
            byte[] newPassword = Encoding.ASCII.GetBytes(model.NewPassword);
            CreatePasswordHash(newPassword, out byte[] newPasswordHashed, out byte[] newPasswordSalt);

            // Update the user's password in the database
            existingUser.Password = newPasswordHashed;
            existingUser.PasswordSalt = newPasswordSalt;

            _dbContext.Users.Update(existingUser);
            await _dbContext.SaveChangesAsync();

            var response = new
            {
                Status = "Success",
                Message = "Password changed successfully",
            };

            return Ok(response);
        }

        private void CreatePasswordHash(byte[] password, out byte[] passwordHashed, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHashed = hmac.ComputeHash(password);
            }
        }

        private bool VerifyPasswordHash(string passwordInput, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var passwordInByte = Encoding.UTF8.GetBytes(passwordInput);
                var computeHash = hmac.ComputeHash(passwordInByte);
                return passwordHash.SequenceEqual(computeHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("userId", user.Id.ToString()),
                new Claim("userName", user.UserName),
                new Claim("email", user.Email),
                new Claim("role", user.Role),
                new Claim("firstName", user.FirstName),
                new Claim("lastName", user.LastName),
                new Claim("gender", user.Gender ? "Male" : "Female")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JwtSettings:SecretKey")));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: cred
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }
    }
}
