using Back_End_Dot_Net.Data;
using Back_End_Dot_Net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back_End_Dot_Net.Controllers
{
    [Route("api/index")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;

        public ImageController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("get-images")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Image>>> GetImages()
        {
            if (_dbContext.Images == null)
            {
                return NotFound();
            }

            return await _dbContext.Images.ToListAsync();
        }

        [Route("get-image-by-id/{id}")]
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
