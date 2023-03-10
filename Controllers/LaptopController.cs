using Back_End_Dot_Net.Data;
using Back_End_Dot_Net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back_End_Dot_Net.Controllers
{
    [Route("api/laptop")]
    [ApiController]
    public class LaptopController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;

        public LaptopController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("get-laptops")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Laptop>>> GetLaptop()
        {
            if (_dbContext.Phones == null)
            {
                return NotFound();
            }

            var phone = await _dbContext.Laptops
                .Select(laptop => new
                {
                    laptop.Name,
                    laptop.Image,
                    laptop.Cpu,
                    laptop.CpuSpeedBase,
                    laptop.CpuSpeedBoost,
                    laptop.Ram,
                    laptop.RamSpeed,
                    laptop.ScreenSize,
                    laptop.InStorage,
                    laptop.Weight,
                })
                .ToListAsync();

            if (phone == null)
            {
                return NotFound();
            }

            return Ok(phone);
        }

        [Route("get-laptop-by-name/{name}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Laptop>>> GetLaptopByName(string name)
        {
            if (_dbContext.Phones == null)
            {
                return NotFound();
            }

            var phone = await _dbContext.Laptops
                .Where(laptop => laptop.Name == name)
                .Select(laptop => new
                {
                    laptop.Name,
                    laptop.Image,
                    laptop.Cpu,
                    laptop.CpuSpeedBase,
                    laptop.CpuSpeedBoost,
                    laptop.Ram,
                    laptop.RamSpeed,
                    laptop.ScreenSize,
                    laptop.InStorage,
                    laptop.Weight,
                })
                .ToListAsync();

            if (phone == null)
            {
                return NotFound();
            }

            return Ok(phone);
        }

        [Route("create-laptop")]
        [HttpPost]
        public async Task<ActionResult<Laptop>> CreateLaptop(Laptop laptop) 
        {
            _dbContext.Laptops.Add(laptop);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLaptop), new {name = laptop.Name}, laptop);
        }
    }
}
