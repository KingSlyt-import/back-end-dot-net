using System.ComponentModel;
using System.Reflection;
using Back_End_Dot_Net.Data;
using Back_End_Dot_Net.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Serilog;

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
            if (_dbContext.Chipsets == null)
            {
                return NotFound();
            }

            var chipsets = await _dbContext.Chipsets
                .Where(chipset => chipset.Hide == false)
                .ToListAsync();

            if (chipsets == null)
            {
                return NotFound();
            }

            var response = new
            {
                Total = chipsets.Count(),
                Data = chipsets
            };

            return Ok(response);
        }

        [Route("get-chipsets")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chipset>>> GetChipsets()
        {
            if (_dbContext.Chipsets == null)
            {
                return NotFound();
            }

            var chipsets = await _dbContext.Chipsets
                .Where(chipset => chipset.Hide == false)
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
                    chipset.TDP,
                    chipset.RAMSpeed
                })
                .ToListAsync();

            if (chipsets == null)
            {
                return NotFound();
            }

            var response = new
            {
                Total = chipsets.Count(),
                Data = chipsets
            };

            return Ok(response);
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
                .Where(chipset =>
                    chipset.Name == name &&
                    chipset.Hide == false
                )
                .Select(chipset => new
                {
                    // Overview info
                    chipset.Name,
                    chipset.Image,
                    // General info
                    chipset.Type,
                    chipset.CPUSocket,
                    chipset.TDP,
                    chipset.semiconductorSize,
                    chipset.CPUTemp,
                    chipset.Pci,
                    // Performance info
                    chipset.CpuSpeedBase,
                    chipset.CpuSpeedBoost,
                    chipset.CpuThread,
                    chipset.PerformanceFeatures,
                    // Memory info
                    chipset.RAMSpeed,
                    chipset.RAMVersion,
                    chipset.MemoryChannels
                })
                .ToListAsync();

            if (chipset == null)
            {
                return NotFound();
            }

            return Ok(chipset);
        }

        [Route("top-5-accessed-chipsets")]
        [HttpGet]
        public IActionResult GetTop5AccessedChipsets()
        {
            var top5Chipsets = _dbContext.Chipsets
                .Where(chipset => chipset.Hide == false)
                .Select(chipset => new
                {
                    chipset.Id,
                    chipset.Name,
                    chipset.Image,
                    chipset.AccessTime,
                    chipset.CreatedDate
                })
                .OrderByDescending(p => p.AccessTime)
                .Take(5)
                .ToList();

            var response = new
            {
                Total = top5Chipsets.Count(),
                Data = top5Chipsets
            };

            return Ok(response);
        }

        [Route("create-chipset")]
        [HttpPost]
        public async Task<ActionResult<Chipset>> CreateChipset(Chipset chipset)
        {
            // Validate against schemas that define along with model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate duplicate chipset
            var existingChipset = await _dbContext.Chipsets
                .FirstOrDefaultAsync(c => c.Name.ToLower() == chipset.Name.ToLower());
            if (existingChipset != null)
            {
                var errorMessage = new ErrorResponse(ErrorTitle.ValidationTitle, ErrorStatus.BadRequest, ErrorType.Validation);
                errorMessage.AddError($"A chipset with the name '{chipset.Name}' already exists.");

                return BadRequest(errorMessage);
            }

            // Validate Performance Features
            if (chipset.PerformanceFeatures != null)
            {
                foreach (var feature in chipset.PerformanceFeatures)
                {
                    // Get the description of the enum value
                    var descriptionAttribute = typeof(ChipsetPerformanceFeatures)
                        .GetMember(feature.ToString())
                        .FirstOrDefault()?
                        .GetCustomAttribute<DescriptionAttribute>();

                    Log.Information("The value of descriptionAttribute is {Value}", descriptionAttribute);

                    if (descriptionAttribute == null)
                    {
                        var errorMessage = new ErrorResponse(ErrorTitle.ValidationTitle, ErrorStatus.BadRequest, ErrorType.Validation);
                        errorMessage.AddError($"The value '{feature}' is not a valid ChipsetPerformanceFeatures value.");

                        return BadRequest(errorMessage);
                    }
                }
            }

            // Generate UUID for new item
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
                await CreateChipset(chipset);
            }

            return CreatedAtAction(nameof(GetChipsets), chipsets);
        }

        [Route("update-chipset/{id}")]
        [HttpPatch]
        public async Task<ActionResult<Chipset>> UpdateChipset(Guid id, [FromBody] JsonPatchDocument<Chipset> chipsetPatch)
        {
            var existingChipset = await _dbContext.Chipsets.FindAsync(id);
            if (existingChipset == null)
            {
                return NotFound();
            }

            // Apply the patch to the existing phone object
            chipsetPatch.ApplyTo(existingChipset);

            // Validate against schemas that define along with model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate Performance Features
            if (existingChipset.PerformanceFeatures != null)
            {
                foreach (var feature in existingChipset.PerformanceFeatures)
                {
                    if (!Enum.IsDefined(typeof(ChipsetPerformanceFeatures), feature))
                    {
                        var errorMessage = new ErrorResponse(ErrorTitle.ValidationTitle, ErrorStatus.BadRequest, ErrorType.Validation);
                        errorMessage.AddError($"The value '{feature}' is not a valid ChipsetPerformanceFeatures value.");

                        return BadRequest(errorMessage);
                    }
                }
            }

            // Save the changes to the database
            await _dbContext.SaveChangesAsync();

            return Ok(existingChipset);
        }

        [Route("delete-chipset/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteChipset(Guid id)
        {
            var chipsetToDelete = await _dbContext.Chipsets.FindAsync(id);

            if (chipsetToDelete == null)
            {
                return NotFound();
            }

            _dbContext.Chipsets.Remove(chipsetToDelete);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
