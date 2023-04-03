using Back_End_Dot_Net.Data;
using Back_End_Dot_Net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back_End_Dot_Net.Controllers
{
    [Route("api/phone")]
    [ApiController]
    public class PhoneController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;

        public PhoneController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("get-phones")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Phone>>> GetPhone()
        {
            if (_dbContext.Phones == null)
            {
                return NotFound();
            }

            var phone = await _dbContext.Phones
                .Select(phone => new
                {
                    phone.Name,
                    phone.Image,
                    phone.BatteryPower,
                    phone.Charging,
                    phone.InStorage,
                    phone.Nits,
                    phone.RAM,
                    phone.ScreenHz
                })
                .ToListAsync();

            if (phone == null)
            {
                return NotFound();
            }

            return Ok(phone);
        }

        [Route("get-phone-by-name/{name}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Phone>>> GetPhoneByName(string name)
        {
            if (_dbContext.Phones == null)
            {
                return NotFound();
            }

            var phone = await _dbContext.Phones
                .Where(phone => phone.Name == name)
                .Select(phone => new
                {
                    phone.Name,
                    phone.Image,
                    phone.BatteryPower,
                    phone.Charging,
                    phone.FrontCameraMP,
                    phone.InStorage,
                    phone.MainCameraMP,
                    phone.Nits,
                    phone.RAM,
                    phone.ScreenHz
                })
                .ToListAsync();

            if (phone == null)
            {
                return NotFound();
            }

            return Ok(phone);
        }

        [Route("top-5-accessed-phones")]
        [HttpGet]
        public IActionResult GetTop5AccessedPhones()
        {
            var top5Phones = _dbContext.Phones.OrderByDescending(p => p.AccessTime).Take(5).ToList();
            return Ok(top5Phones);
        }

        [Route("create-phone")]
        [HttpPost]
        public async Task<ActionResult<Phone>> PostPhone(Phone phone)
        {
            phone.Id = Guid.NewGuid();

            _dbContext.Phones.Add(phone);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPhone), new { id = phone.Id }, phone);
        }
    }
}
