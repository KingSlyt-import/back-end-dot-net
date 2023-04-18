// Namespace imports 
using Back_End_Dot_Net.Data;
using Back_End_Dot_Net.Models;
using Back_End_Dot_Net.DTOs;
// Library imports
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Security.Cryptography;
using System.Text;

namespace Back_End_Dot_Net.Controllers
{
    [Route("api/authenticate")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;

        public AuthController(ApplicationDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [Route("get-all-user")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUser()
        {
            if (_dbContext.Users == null)
            {
                return NotFound();
            }

            var users = await _dbContext.Users
                .Where(user => user.Hide == false)
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
        public async Task<ActionResult<User>> Register(UserRegisterDTO userDto)
        {
            // Convert string from user input --> byte[] to store in database
            byte[] password = Encoding.ASCII.GetBytes(userDto.Password);
            CreatePasswordHash(password, out byte[] passwordHashed, out byte[] passwordSalt);

            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Gender = userDto.Gender,
                UserName = userDto.UserName,
                Password = passwordHashed,
                PasswordSalt = passwordSalt,
                Avatar = userDto.Avatar ?? null
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(byte[] password, out byte[] passwordHashed, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHashed = hmac.ComputeHash(password);
            }
        }
    }
}
