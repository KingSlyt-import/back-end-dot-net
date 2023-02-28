using Back_End_Dot_Net.Data;
using Back_End_Dot_Net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back_End_Dot_Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;

        public IndexController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("GetImages")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Image>>> GetImages()
        {
            if (_dbContext.Images == null)
            {
                return NotFound();
            }

            return await _dbContext.Images.ToListAsync();
        }

        [Route("GetImage/{id}")]
        [HttpGet]
        public async Task<ActionResult<Image>> GetImagesById(int id)
        {
            if (_dbContext.Images == null)
            {
                return NotFound();
            }

            var image =  await _dbContext.Images.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            return image;
        }
    }
}
