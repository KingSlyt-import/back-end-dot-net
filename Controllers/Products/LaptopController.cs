﻿// Namespace imports 
using Back_End_Dot_Net.Data;
using Back_End_Dot_Net.Models;
// Library imports
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Back_End_Dot_Net.DTOs;

namespace Back_End_Dot_Net.Controllers
{
    [Route("api/laptop")]
    [ApiController]
    public class LaptopController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly FeaturesValidator _validator;
        private readonly IMapper _mapper;


        public LaptopController(ApplicationDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _validator = new FeaturesValidator(dbContext);
        }

        [Route("get-all-laptops")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Laptop>>> GetAllLaptop()
        {
            if (_dbContext.Laptops == null)
            {
                return NotFound();
            }

            var laptops = await _dbContext.Laptops
                .Where(laptop => laptop.Hide == false)
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
            if (_dbContext.Laptops == null)
            {
                return NotFound();
            }

            var laptops = await _dbContext.Laptops
                .Where(laptop => laptop.Hide == false)
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
            if (_dbContext.Laptops == null)
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

            return Ok(laptop);
        }

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

            return Ok(top5Laptops);
        }

        [Route("create-laptop")]
        [HttpPost]
        public async Task<ActionResult<Laptop>> CreateLaptop(LaptopDTO laptopDto)
        {
            var laptop = _mapper.Map<Laptop>(laptopDto);

            // Validate against schemas that define along with model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate duplicate laptop
            var existingLaptop = await _dbContext.Laptops
                .FirstOrDefaultAsync(p => p.Name.ToLower() == laptop.Name.ToLower());
            if (existingLaptop != null)
            {
                var errorMessage = new ErrorResponse(ErrorTitle.ValidationTitle, ErrorStatus.BadRequest, ErrorType.Validation);
                errorMessage.AddError($"A laptop with the name '{laptop.Name}' already exists.");

                return BadRequest(errorMessage);
            }

            // Performance features validation
            if (laptop.PerformanceFeatures != null)
            {
                foreach (var feature in laptop.PerformanceFeatures)
                {
                    // Validate each feature using FeaturesValidator
                    var featureModel = new Features
                    {
                        Name = feature,
                        Type = FeaturesType.Performance.GetDisplayName(),
                        Category = FeaturesCategory.Laptop.GetDisplayName()
                    };

                    var (isValid, errors) = await _validator.ValidateFeatureAsync(featureModel);

                    if (!isValid)
                    {
                        return BadRequest(errors); // Return any validation errors for the invalid feature(s)
                    }
                }
            }

            // Screen features validation
            if (laptop.ScreenFeatures != null)
            {
                foreach (var feature in laptop.ScreenFeatures)
                {
                    // Validate each feature using FeaturesValidator
                    var featureModel = new Features
                    {
                        Name = feature,
                        Type = FeaturesType.Screen.GetDisplayName(),
                        Category = FeaturesCategory.Laptop.GetDisplayName()
                    };

                    var (isValid, errors) = await _validator.ValidateFeatureAsync(featureModel);

                    if (!isValid)
                    {
                        return BadRequest(errors); // Return any validation errors for the invalid feature(s)
                    }
                }
            }

            // Design features validation
            if (laptop.DesignFeatures != null)
            {
                foreach (var feature in laptop.DesignFeatures)
                {
                    // Validate each feature using FeaturesValidator
                    var featureModel = new Features
                    {
                        Name = feature,
                        Type = FeaturesType.Design.GetDisplayName(),
                        Category = FeaturesCategory.Laptop.GetDisplayName()
                    };

                    var (isValid, errors) = await _validator.ValidateFeatureAsync(featureModel);

                    if (!isValid)
                    {
                        return BadRequest(errors); // Return any validation errors for the invalid feature(s)
                    }
                }
            }

            // Features validation
            if (laptop.Features != null)
            {
                foreach (var feature in laptop.Features)
                {
                    // Validate each feature using FeaturesValidator
                    var featureModel = new Features
                    {
                        Name = feature,
                        Type = FeaturesType.Default.GetDisplayName(),
                        Category = FeaturesCategory.Laptop.GetDisplayName()
                    };

                    var (isValid, errors) = await _validator.ValidateFeatureAsync(featureModel);

                    if (!isValid)
                    {
                        return BadRequest(errors); // Return any validation errors for the invalid feature(s)
                    }
                }
            }

            // Generate UUID for new item
            laptop.Id = Guid.NewGuid();

            // If user input CPU ID
            if (laptop.CpuId != null)
            {
                var existingCpu = await _dbContext.Chipsets.FindAsync(laptop.CpuId);
                if (existingCpu == null)
                {
                    var errorMessage = new ErrorResponse(ErrorTitle.ItemsNotFound, ErrorStatus.BadRequest, ErrorType.InvalidInput);
                    errorMessage.AddError($"The value '{laptop.CpuId}' is not found on Chipsets Entity.");

                    return BadRequest(errorMessage);
                }

                laptop.CPU = existingCpu;
            }

            _dbContext.Laptops.Add(laptop);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLaptops), new { name = laptop.Name }, laptop);
        }

        [Route("bulk-create-laptops")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Laptop>>> BulkCreateLaptops(BulkCreateDTO<LaptopDTO> laptops)
        {
            foreach (var laptop in laptops.Items)
            {
                await CreateLaptop(laptop);
            }

            return CreatedAtAction(nameof(GetLaptops), laptops);
        }

        [Route("update-laptops/{id}")]
        [HttpPatch]
        public async Task<ActionResult<Laptop>> UpdateLaptop(Guid id, [FromBody] JsonPatchDocument<Laptop> laptopPatch)
        {
            var existingLaptop = await _dbContext.Laptops.FindAsync(id);
            if (existingLaptop == null)
            {
                return NotFound();
            }

            // Apply the patch to the existing laptop object
            laptopPatch.ApplyTo(existingLaptop);

            // Validate against schemas that define along with model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Performance features validation
            if (existingLaptop.PerformanceFeatures != null)
            {
                foreach (var feature in existingLaptop.PerformanceFeatures)
                {
                    // Validate each feature using FeaturesValidator
                    var featureModel = new Features
                    {
                        Name = feature,
                        Type = FeaturesType.Performance.GetDisplayName(),
                        Category = FeaturesCategory.Laptop.GetDisplayName()
                    };

                    var (isValid, errors) = await _validator.ValidateFeatureAsync(featureModel);

                    if (!isValid)
                    {
                        return BadRequest(errors); // Return any validation errors for the invalid feature(s)
                    }
                }
            }

            // Screen features validation
            if (existingLaptop.ScreenFeatures != null)
            {
                foreach (var feature in existingLaptop.ScreenFeatures)
                {
                    // Validate each feature using FeaturesValidator
                    var featureModel = new Features
                    {
                        Name = feature,
                        Type = FeaturesType.Screen.GetDisplayName(),
                        Category = FeaturesCategory.Laptop.GetDisplayName()
                    };

                    var (isValid, errors) = await _validator.ValidateFeatureAsync(featureModel);

                    if (!isValid)
                    {
                        return BadRequest(errors); // Return any validation errors for the invalid feature(s)
                    }
                }
            }

            // Design features validation
            if (existingLaptop.DesignFeatures != null)
            {
                foreach (var feature in existingLaptop.DesignFeatures)
                {
                    // Validate each feature using FeaturesValidator
                    var featureModel = new Features
                    {
                        Name = feature,
                        Type = FeaturesType.Design.GetDisplayName(),
                        Category = FeaturesCategory.Laptop.GetDisplayName()
                    };

                    var (isValid, errors) = await _validator.ValidateFeatureAsync(featureModel);

                    if (!isValid)
                    {
                        return BadRequest(errors); // Return any validation errors for the invalid feature(s)
                    }
                }
            }

            // Features validation
            if (existingLaptop.Features != null)
            {
                foreach (var feature in existingLaptop.Features)
                {
                    // Validate each feature using FeaturesValidator
                    var featureModel = new Features
                    {
                        Name = feature,
                        Type = FeaturesType.Default.GetDisplayName(),
                        Category = FeaturesCategory.Laptop.GetDisplayName()
                    };

                    var (isValid, errors) = await _validator.ValidateFeatureAsync(featureModel);

                    if (!isValid)
                    {
                        return BadRequest(errors); // Return any validation errors for the invalid feature(s)
                    }
                }
            }

            await _dbContext.SaveChangesAsync();

            return Ok(existingLaptop);
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

            var response = new
            {
                Message = $"Laptop with UUID {id} had been deleted.",
                LaptopName = laptopToDelete.Name
            };

            return Ok(response);
        }
    }
}
