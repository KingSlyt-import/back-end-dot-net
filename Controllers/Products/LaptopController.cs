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

        [Route("get-all-laptops")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Laptop>>> GetAllLaptop()
        {
            if (_dbContext.Phones == null)
            {
                return NotFound();
            }

            var phone = await _dbContext.Laptops.ToListAsync();

            if (phone == null)
            {
                return NotFound();
            }

            return Ok(phone);
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
                    laptop.CPUName,
                    laptop.CPUType,
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
                    laptop.CPUName,
                    laptop.CPUType,
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

        [Route("top-5-accessed-laptops")]
        [HttpGet]
        public IActionResult GetTop5AccessedLaptops()
        {
            var top5Laptops = _dbContext.Laptops.OrderByDescending(p => p.AccessTime).Take(5).ToList();
            return Ok(top5Laptops);
        }

        [Route("create-laptop")]
        [HttpPost]
        public async Task<ActionResult<Laptop>> CreateLaptop(Laptop laptop)
        {
            laptop.Id = Guid.NewGuid();

            _dbContext.Laptops.Add(laptop);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLaptop), new { name = laptop.Name }, laptop);
        }
    }
}
