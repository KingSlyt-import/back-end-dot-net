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

            return await _dbContext.Phones.ToListAsync();
        }

        [Route("get-phones-by-id/{id}")]
        [HttpGet]
        public async Task<ActionResult<Phone>> GetPhonesById(int id)
        {
            if (_dbContext.Phones == null)
            {
                return NotFound();
            }

            var phone = await _dbContext.Phones.FindAsync(id);
            if (phone == null)
            {
                return NotFound();
            }

            return phone;
        }
    }
}
