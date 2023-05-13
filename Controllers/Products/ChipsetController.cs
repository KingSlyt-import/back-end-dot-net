﻿// Namespace imports 
using Back_End_Dot_Net.Data;
using Back_End_Dot_Net.Models;
using Back_End_Dot_Net.DTOs;
// Library imports
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Back_End_Dot_Net.Controllers
{
    [Route("api/chipset")]
    [ApiController]
    public class ChipsetController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly FeaturesValidator _validator;
        private readonly IMapper _mapper;

        public ChipsetController(ApplicationDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _validator = new FeaturesValidator(dbContext);
        }

        [Route("get-all-chipset"), Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chipset>>> GetAllChipset()
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
        public async Task<ActionResult<IEnumerable<Chipset>>> GetChipsets(string sort = "name_asc")
        {
            var chipsetsQuery = _dbContext.Chipsets.Where(chipset => chipset.Hide == false);

            // Determine sorting order based on the sort parameter
            switch (sort)
            {
                case "most_popular":
                    chipsetsQuery = chipsetsQuery.OrderBy(chipset => chipset.AccessTime);
                    break;
                case "release_date_asc":
                    chipsetsQuery = chipsetsQuery.OrderBy(chipset => chipset.CreatedDate);
                    break;
                case "release_date_desc":
                    chipsetsQuery = chipsetsQuery.OrderByDescending(chipset => chipset.CreatedDate);
                    break;
                // Default sorting order is by name ascending
                default:
                    chipsetsQuery = chipsetsQuery.OrderBy(chipset => chipset.Name);
                    break;
            }

            var chipsets = await chipsetsQuery
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
                .FirstOrDefaultAsync();

            if (chipset == null)
            {
                return NotFound();
            }

            chipset.AccessTime = chipset.AccessTime + 1; // Update the AccessTime field by 1

            _dbContext.Update(chipset); // Mark the entity as modified
            await _dbContext.SaveChangesAsync(); // Save the changes to the database

            return Ok(new
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
            });
        }

        [Route("compare-chipsets/{name1}/{name2}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chipset>>> CompareChipsets(string name1, string name2)
        {
            var chipset1Result = await GetChipsetByName(name1);
            var chipset2Result = await GetChipsetByName(name2);

            var chipsets = new[] { chipset1Result, chipset2Result };
            return new OkObjectResult(chipsets);
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

            return Ok(top5Chipsets);
        }

        [Route("create-chipset"), Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Chipset>> CreateChipset(ChipsetDTO chipsetDto)
        {
            var chipset = _mapper.Map<Chipset>(chipsetDto);

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

            // Performance features validation
            if (chipset.PerformanceFeatures != null)
            {
                foreach (var feature in chipset.PerformanceFeatures)
                {
                    // Validate each feature using FeaturesValidator
                    var featureModel = new Features
                    {
                        Name = feature,
                        Type = FeaturesType.Performance.GetDisplayName(),
                        Category = FeaturesCategory.Chipset.GetDisplayName()
                    };

                    var (isValid, errors) = await _validator.ValidateFeatureAsync(featureModel);

                    if (!isValid)
                    {
                        return BadRequest(errors); // Return any validation errors for the invalid feature(s)
                    }
                }
            }

            // Generate UUID for new item
            chipset.Id = Guid.NewGuid();

            _dbContext.Chipsets.Add(chipset);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChipsets), new { name = chipset.Name }, chipset);
        }

        [Route("bulk-create-chipsets"), Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Chipset>>> BulkCreateChipsets(BulkCreateDTO<ChipsetDTO> chipsets)
        {
            foreach (var chipset in chipsets.Items)
            {
                await CreateChipset(chipset);
            }

            return CreatedAtAction(nameof(GetChipsets), chipsets);
        }

        [Route("update-chipset/{id}"), Authorize(Roles = "Admin")]
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

            // Performance features validation
            if (existingChipset.PerformanceFeatures != null)
            {
                foreach (var feature in existingChipset.PerformanceFeatures)
                {
                    // Validate each feature using FeaturesValidator
                    var featureModel = new Features
                    {
                        Name = feature,
                        Type = FeaturesType.Performance.GetDisplayName(),
                        Category = FeaturesCategory.Chipset.GetDisplayName()
                    };

                    var (isValid, errors) = await _validator.ValidateFeatureAsync(featureModel);

                    if (!isValid)
                    {
                        return BadRequest(errors); // Return any validation errors for the invalid feature(s)
                    }
                }
            }

            // Save the changes to the database
            await _dbContext.SaveChangesAsync();

            return Ok(existingChipset);
        }

        [Route("delete-chipset/{id}"), Authorize(Roles = "Admin")]
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

            var response = new
            {
                Message = $"Chipset with UUID {id} had been deleted.",
                ChipsetName = chipsetToDelete.Name
            };

            return Ok(response);
        }
    }
}
