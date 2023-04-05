using Back_End_Dot_Net.Data;
using Back_End_Dot_Net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back_End_Dot_Net.Controllers
{
    [Route("api/chipset")]
    [ApiController]
    public class ChipsetController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;

        public ChipsetController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("get-all-chipset")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Laptop>>> GetAllChipset()
        {
            if (_dbContext.Phones == null)
            {
                return NotFound();
            }

            var phone = await _dbContext.Chipsets.ToListAsync();

            if (phone == null)
            {
                return NotFound();
            }

            return Ok(phone);
        }

        [Route("get-chipsets")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chipset>>> GetChipsets()
        {
            if (_dbContext.Chipsets == null)
            {
                return NotFound();
            }

            var chipset = await _dbContext.Chipsets
                .Select(chipset => new
                {
                    chipset.Name,
                    chipset.Image,
                    chipset.CpuSpeedBase,
                    chipset.CpuSpeedBoost,
                    chipset.CpuThread,
                    chipset.semiconductorSize,
                    chipset.Manufacture,
                    chipset.MemoryChannels
                })
                .ToListAsync();

            if (chipset == null)
            {
                return NotFound();
            }

            return Ok(chipset);
        }

        [Route("get-chipset-by-name/{name}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chipset>>> GetChipsetByName(string name)
        {
            if (_dbContext.Chipsets == null)
            {
                return NotFound();
            }

            var chipset = await _dbContext.Chipsets
                .Where(chipset => chipset.Name == name)
                .Select(chipset => new
                {
                    chipset.Name,
                    chipset.Image,
                    chipset.CpuSpeedBase,
                    chipset.CpuSpeedBoost,
                    chipset.CpuThread,
                    chipset.semiconductorSize,
                    chipset.Manufacture,
                    chipset.MemoryChannels,
                })
                .ToListAsync();

            if (chipset == null)
            {
                return NotFound();
            }

            return Ok(chipset);
        }

        [Route("top-5-accessed-Chipsets")]
        [HttpGet]
        public IActionResult GetTop5AccessedChipsets()
        {
            var top5Chipsets = _dbContext.Chipsets.OrderByDescending(p => p.AccessTime).Take(5).ToList();
            return Ok(top5Chipsets);
        }

        [Route("create-chipset")]
        [HttpPost]
        public async Task<ActionResult<Chipset>> CreateChipset(Chipset chipset)
        {
            Console.Write(chipset);

            chipset.Id = Guid.NewGuid();

            _dbContext.Chipsets.Add(chipset);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChipsets), new { name = chipset.Name }, chipset);
        }

        [Route("create-chipsets")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Chipset>>> CreateChipsets(IEnumerable<Chipset> chipsets)
        {
            foreach (var chipset in chipsets)
            {
                chipset.Id = Guid.NewGuid();

                _dbContext.Chipsets.Add(chipset);
            }

            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChipsets), chipsets);
        }
    }
}
