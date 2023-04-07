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

            var laptops = await _dbContext.Laptops
                .Where(phone => phone.Hide == false)
                .ToListAsync();

            if (laptops == null)
            {
                return NotFound();
            }

            var response = new
            {
                Total = laptops.Count(),
                Data = laptops
            };

            return Ok(response);
        }

        [Route("get-laptops")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Laptop>>> GetLaptops()
        {
            if (_dbContext.Phones == null)
            {
                return NotFound();
            }

            var laptops = await _dbContext.Laptops
                .Where(phone => phone.Hide == false)
                .Select(laptop => new
                {
                    laptop.Name,
                    laptop.Image,
                    laptop.CPU,
                    laptop.Ram,
                    laptop.RamSpeed,
                    laptop.ScreenSize,
                    laptop.InStorage,
                    laptop.Weight,
                })
                .ToListAsync();

            if (laptops == null)
            {
                return NotFound();
            }

            var response = new
            {
                Total = laptops.Count(),
                Data = laptops
            };

            return Ok(response);        
        }

        [Route("get-laptop-by-name/{name}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Laptop>>> GetLaptopByName(string name)
        {
            if (_dbContext.Phones == null)
            {
                return NotFound();
            }

            var laptop = await _dbContext.Laptops
                .Where(laptop => laptop.Name == name)
                .Select(laptop => new
                {
                    laptop.Name,
                    laptop.Image,
                    laptop.CPU,
                    laptop.Ram,
                    laptop.RamSpeed,
                    laptop.ScreenSize,
                    laptop.InStorage,
                    laptop.Weight,
                })
                .ToListAsync();

            if (laptop == null)
            {
                return NotFound();
            }

            var response = new
            {
                Total = laptop.Count(),
                Data = laptop
            };

            return Ok(response);         }

        [Route("top-5-accessed-laptops")]
        [HttpGet]
        public IActionResult GetTop5AccessedLaptops()
        {
            var top5Laptops = _dbContext.Laptops
               .Where(laptop => laptop.Hide == false)
                .Select(laptop => new
                {
                    laptop.Id,
                    laptop.Name,
                    laptop.Image,
                    laptop.AccessTime,
                    laptop.CreatedDate
                })
                .OrderByDescending(p => p.AccessTime)
                .Take(5)
                .ToList();

            var response = new
            {
                Total = top5Laptops.Count(),
                Data = top5Laptops
            };

            return Ok(response);         }

        [Route("create-laptop")]
        [HttpPost]
        public async Task<ActionResult<Laptop>> CreateLaptop(Laptop laptop)
        {
            laptop.Id = Guid.NewGuid();

            _dbContext.Laptops.Add(laptop);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLaptops), new { name = laptop.Name }, laptop);
        }

        [Route("delete-laptop/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteLaptop(Guid id)
        {
            var laptopToDelete = await _dbContext.Laptops.FindAsync(id);

            if (laptopToDelete == null)
            {
                return NotFound();
            }

            _dbContext.Laptops.Remove(laptopToDelete);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
