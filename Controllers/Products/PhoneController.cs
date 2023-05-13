﻿// Namespace imports 
using Back_End_Dot_Net.Data;
using Back_End_Dot_Net.Models;
using Back_End_Dot_Net.DTOs;
// Library imports
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Back_End_Dot_Net.Controllers
{
    [Route("api/phone")]
    [ApiController]
    public class PhoneController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly FeaturesValidator _validator;
        private readonly IMapper _mapper;

        public PhoneController(ApplicationDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _validator = new FeaturesValidator(dbContext);
        }

        [Route("get-all-phone"), Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Phone>>> GetAllPhone()
        {
            if (_dbContext.Phones == null)
            {
                return NotFound();
            }

            var phones = await _dbContext.Phones
                .Where(phone => phone.Hide == false)
                .ToListAsync();

            if (phones == null)
            {
                return NotFound();
            }

            var response = new
            {
                Total = phones.Count(),
                Data = phones
            };

            return Ok(response);
        }

        [Route("get-phones")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Phone>>> GetPhones()
        {
            if (_dbContext.Phones == null)
            {
                return NotFound();
            }

            var phone = await _dbContext.Phones
                .Where(phone => phone.Hide == false)
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

            var response = new
            {
                Total = phone.Count(),
                Data = phone
            };

            return Ok(response);
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
                .Where(phone =>
                    phone.Name == name &&
                    phone.Hide == false
                )
                .FirstOrDefaultAsync();

            if (phone == null)
            {
                return NotFound();
            }

            phone.AccessTime = phone.AccessTime + 1; // Update the AccessTime field by 1

            _dbContext.Update(phone); // Mark the entity as modified
            await _dbContext.SaveChangesAsync(); // Save the changes to the database

            var cameraMain = phone.MainCameraMP.Split("+");

            return Ok(new
            {
                // Overview 
                phone.Name,
                phone.Image,
                phone.Description,
                phone.Price,
                // Performance info
                phone.CPUName,
                phone.CPUSpeedBase,
                phone.CPUSpeedBoost,
                phone.PerformanceFeatures,
                // Memory Info
                phone.RAM,
                phone.RAMSpeed,
                phone.InStorage,
                // Display info
                phone.ScreenSize,
                phone.Resolution,
                phone.ScreenHz,
                phone.Nits,
                phone.ScreenFeatures,
                // Design info
                phone.Weight,
                phone.Height,
                phone.Width,
                phone.DesignFeatures,
                // Camera info
                MainCameraMP = cameraMain,
                MainCameraCount = cameraMain.Length,
                phone.FrontCameraMP,
                // Battery info
                phone.BatteryPower,
                phone.MagSafe,
                // Features
                phone.Features
            });
        }

        [Route("top-5-accessed-phones")]
        [HttpGet]
        public IActionResult GetTop5AccessedPhones()
        {
            var top5Phones = _dbContext.Phones
                .Where(phone => phone.Hide == false)
                .Select(phone => new
                {
                    phone.Id,
                    phone.Name,
                    phone.Image,
                    phone.AccessTime,
                    phone.CreatedDate
                })
                .OrderByDescending(p => p.AccessTime)
                .Take(5)
                .ToList();

            return Ok(top5Phones);
        }

        [Route("create-phone"), Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Phone>> CreatePhone(PhoneDTO phoneDto)
        {
            var phone = _mapper.Map<Phone>(phoneDto);

            // Validate against schemas that define along with model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate duplicate phone
            var existingPhone = await _dbContext.Phones
                .FirstOrDefaultAsync(p => p.Name.ToLower() == phone.Name.ToLower());
            if (existingPhone != null)
            {
                var errorMessage = new ErrorResponse(ErrorTitle.ValidationTitle, ErrorStatus.BadRequest, ErrorType.Validation);
                errorMessage.AddError($"A phone with the name '{phone.Name}' already exists.");

                return BadRequest(errorMessage);
            }

            // Performance features validation
            if (phone.PerformanceFeatures != null)
            {
                foreach (var feature in phone.PerformanceFeatures)
                {
                    // Validate each feature using FeaturesValidator
                    var featureModel = new Features
                    {
                        Name = feature,
                        Type = FeaturesType.Performance.GetDisplayName(),
                        Category = FeaturesCategory.Phone.GetDisplayName()
                    };

                    var (isValid, errors) = await _validator.ValidateFeatureAsync(featureModel);

                    if (!isValid)
                    {
                        return BadRequest(errors); // Return any validation errors for the invalid feature(s)
                    }
                }
            }

            // Screen features validation
            if (phone.ScreenFeatures != null)
            {
                foreach (var feature in phone.ScreenFeatures)
                {
                    // Validate each feature using FeaturesValidator
                    var featureModel = new Features
                    {
                        Name = feature,
                        Type = FeaturesType.Screen.GetDisplayName(),
                        Category = FeaturesCategory.Phone.GetDisplayName()
                    };

                    var (isValid, errors) = await _validator.ValidateFeatureAsync(featureModel);

                    if (!isValid)
                    {
                        return BadRequest(errors); // Return any validation errors for the invalid feature(s)
                    }
                }
            }

            // Design features validation
            if (phone.DesignFeatures != null)
            {
                foreach (var feature in phone.DesignFeatures)
                {
                    // Validate each feature using FeaturesValidator
                    var featureModel = new Features
                    {
                        Name = feature,
                        Type = FeaturesType.Design.GetDisplayName(),
                        Category = FeaturesCategory.Phone.GetDisplayName()
                    };

                    var (isValid, errors) = await _validator.ValidateFeatureAsync(featureModel);

                    if (!isValid)
                    {
                        return BadRequest(errors); // Return any validation errors for the invalid feature(s)
                    }
                }
            }

            // Features validation
            if (phone.Features != null)
            {
                foreach (var feature in phone.Features)
                {
                    // Validate each feature using FeaturesValidator
                    var featureModel = new Features
                    {
                        Name = feature,
                        Type = FeaturesType.Default.GetDisplayName(),
                        Category = FeaturesCategory.Phone.GetDisplayName()
                    };

                    var (isValid, errors) = await _validator.ValidateFeatureAsync(featureModel);

                    if (!isValid)
                    {
                        return BadRequest(errors); // Return any validation errors for the invalid feature(s)
                    }
                }
            }


            // Generate UUID for new item
            phone.Id = Guid.NewGuid();

            _dbContext.Phones.Add(phone);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPhones), new { id = phone.Id }, phone);
        }

        [Route("bulk-create-phones"), Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Phone>>> CreatePhones(BulkCreateDTO<PhoneDTO> phones)
        {
            foreach (var phone in phones.Items)
            {
                await CreatePhone(phone);
            }

            return CreatedAtAction(nameof(GetPhones), phones);
        }

        [Route("update-phones/{id}"), Authorize(Roles = "Admin")]
        [HttpPatch]
        public async Task<ActionResult<Phone>> UpdatePhone(Guid id, [FromBody] JsonPatchDocument<Phone> phonePatch)
        {
            var existingPhone = await _dbContext.Phones.FindAsync(id);
            if (existingPhone == null)
            {
                return NotFound();
            }

            // Apply the patch to the existing phone object
            phonePatch.ApplyTo(existingPhone);

            // Validate against schemas that define along with model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Performance features validation
            if (existingPhone.PerformanceFeatures != null)
            {
                foreach (var feature in existingPhone.PerformanceFeatures)
                {
                    // Validate each feature using FeaturesValidator
                    var featureModel = new Features
                    {
                        Name = feature,
                        Type = FeaturesType.Performance.GetDisplayName(),
                        Category = FeaturesCategory.Phone.GetDisplayName()
                    };

                    var (isValid, errors) = await _validator.ValidateFeatureAsync(featureModel);

                    if (!isValid)
                    {
                        return BadRequest(errors); // Return any validation errors for the invalid feature(s)
                    }
                }
            }

            // Screen features validation
            if (existingPhone.ScreenFeatures != null)
            {
                foreach (var feature in existingPhone.ScreenFeatures)
                {
                    // Validate each feature using FeaturesValidator
                    var featureModel = new Features
                    {
                        Name = feature,
                        Type = FeaturesType.Screen.GetDisplayName(),
                        Category = FeaturesCategory.Phone.GetDisplayName()
                    };

                    var (isValid, errors) = await _validator.ValidateFeatureAsync(featureModel);

                    if (!isValid)
                    {
                        return BadRequest(errors); // Return any validation errors for the invalid feature(s)
                    }
                }
            }

            // Design features validation
            if (existingPhone.DesignFeatures != null)
            {
                foreach (var feature in existingPhone.DesignFeatures)
                {
                    // Validate each feature using FeaturesValidator
                    var featureModel = new Features
                    {
                        Name = feature,
                        Type = FeaturesType.Design.GetDisplayName(),
                        Category = FeaturesCategory.Phone.GetDisplayName()
                    };

                    var (isValid, errors) = await _validator.ValidateFeatureAsync(featureModel);

                    if (!isValid)
                    {
                        return BadRequest(errors); // Return any validation errors for the invalid feature(s)
                    }
                }
            }

            // Features validation
            if (existingPhone.Features != null)
            {
                foreach (var feature in existingPhone.Features)
                {
                    // Validate each feature using FeaturesValidator
                    var featureModel = new Features
                    {
                        Name = feature,
                        Type = FeaturesType.Default.GetDisplayName(),
                        Category = FeaturesCategory.Phone.GetDisplayName()
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

            return Ok(existingPhone);
        }

        [Route("delete-phone/{id}"), Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeletePhone(Guid id)
        {
            var phoneToDelete = await _dbContext.Phones.FindAsync(id);

            if (phoneToDelete == null)
            {
                return NotFound();
            }

            _dbContext.Phones.Remove(phoneToDelete);
            await _dbContext.SaveChangesAsync();

            var response = new
            {
                Message = $"Laptop with UUID {id} had been deleted.",
                LaptopName = phoneToDelete.Name
            };

            return Ok(response);
        }
    }
}
