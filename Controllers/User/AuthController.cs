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
                Avatar = userRegisterDto.Avatar ?? null
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

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

            var response = new {
                Status = "Success",
                Message = "Login successfully",
                Token = token
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
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Gender, user.Gender ? "Male" : "Female")
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
