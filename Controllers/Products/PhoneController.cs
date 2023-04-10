using Back_End_Dot_Net.Data;
using Back_End_Dot_Net.Models;
using Microsoft.AspNetCore.JsonPatch;
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

        [Route("get-all-phone")]
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

            var response = new
            {
                Total = phone.Count(),
                Data = phone
            };

            return Ok(response);
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

            var response = new
            {
                Total = top5Phones.Count(),
                Data = top5Phones
            };

            return Ok(response);
        }

        [Route("create-phone")]
        [HttpPost]
        public async Task<ActionResult<Phone>> CreatePhone(Phone phone)
        {
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

            // Validate Performance Features
            if (phone.PerformanceFeatures != null)
            {
                foreach (var feature in phone.PerformanceFeatures)
                {
                    if (!Enum.IsDefined(typeof(PhonePerformanceFeatures), feature))
                    {
                        var errorMessage = new ErrorResponse(ErrorTitle.ValidationTitle, ErrorStatus.BadRequest, ErrorType.Validation);
                        errorMessage.AddError($"The value '{feature}' is not a valid PhonePerformanceFeatures value.");

                        return BadRequest(errorMessage);
                    }
                }
            }

            // Validate Screen Features
            if (phone.ScreenFeatures != null)
            {
                foreach (var feature in phone.ScreenFeatures)
                {
                    if (!Enum.IsDefined(typeof(PhoneScreenFeatures), feature))
                    {
                        var errorMessage = new ErrorResponse(ErrorTitle.ValidationTitle, ErrorStatus.BadRequest, ErrorType.Validation);
                        errorMessage.AddError($"The value '{feature}' is not a valid PhoneScreenFeatures value.");

                        return BadRequest(errorMessage);
                    }
                }
            }

            // Validate Design Features
            if (phone.DesignFeatures != null)
            {
                foreach (var feature in phone.DesignFeatures)
                {
                    if (!Enum.IsDefined(typeof(PhoneDesignFeatures), feature))
                    {
                        var errorMessage = new ErrorResponse(ErrorTitle.ValidationTitle, ErrorStatus.BadRequest, ErrorType.Validation);
                        errorMessage.AddError($"The value '{feature}' is not a valid PhoneDesignFeatures value.");

                        return BadRequest(errorMessage);
                    }
                }
            }

            // Validate Features
            if (phone.Features != null)
            {
                foreach (var feature in phone.Features)
                {
                    if (!Enum.IsDefined(typeof(PhoneFeatures), feature))
                    {
                        var errorMessage = new ErrorResponse(ErrorTitle.ValidationTitle, ErrorStatus.BadRequest, ErrorType.Validation);
                        errorMessage.AddError($"The value '{feature}' is not a valid PhoneFeatures value.");

                        return BadRequest(errorMessage);
                    }
                }
            }

            // Generate UUID for new item
            phone.Id = Guid.NewGuid();

            _dbContext.Phones.Add(phone);
            await _dbContext.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetPhones), new { id = phone.Id }, phone);
        }

        [Route("create-phones")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Phone>>> CreatePhones(IEnumerable<Phone> phones)
        {
            foreach (var phone in phones)
            {
                await CreatePhone(phone);
            }

            return CreatedAtAction(nameof(GetPhones), phones);
        }

        [Route("update-phones/{id}")]
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

            // Validate Performance Features
            if (existingPhone.PerformanceFeatures != null)
            {
                foreach (var feature in existingPhone.PerformanceFeatures)
                {
                    if (!Enum.IsDefined(typeof(PhonePerformanceFeatures), feature))
                    {
                        var errorMessage = new ErrorResponse(ErrorTitle.ValidationTitle, ErrorStatus.BadRequest, ErrorType.Validation);
                        errorMessage.AddError($"The value '{feature}' is not a valid PhonePerformanceFeatures value.");

                        return BadRequest(errorMessage);
                    }
                }
            }

            // Validate Screen Features
            if (existingPhone.ScreenFeatures != null)
            {
                foreach (var feature in existingPhone.ScreenFeatures)
                {
                    if (!Enum.IsDefined(typeof(PhoneScreenFeatures), feature))
                    {
                        var errorMessage = new ErrorResponse(ErrorTitle.ValidationTitle, ErrorStatus.BadRequest, ErrorType.Validation);
                        errorMessage.AddError($"The value '{feature}' is not a valid PhoneScreenFeatures value.");

                        return BadRequest(errorMessage);
                    }
                }
            }

            // Validate Design Features
            if (existingPhone.DesignFeatures != null)
            {
                foreach (var feature in existingPhone.DesignFeatures)
                {
                    if (!Enum.IsDefined(typeof(PhoneDesignFeatures), feature))
                    {
                        var errorMessage = new ErrorResponse(ErrorTitle.ValidationTitle, ErrorStatus.BadRequest, ErrorType.Validation);
                        errorMessage.AddError($"The value '{feature}' is not a valid PhoneDesignFeatures value.");

                        return BadRequest(errorMessage);
                    }
                }
            }

            // Validate Features
            if (existingPhone.Features != null)
            {
                foreach (var feature in existingPhone.Features)
                {
                    if (!Enum.IsDefined(typeof(PhoneFeatures), feature))
                    {
                        var errorMessage = new ErrorResponse(ErrorTitle.ValidationTitle, ErrorStatus.BadRequest, ErrorType.Validation);
                        errorMessage.AddError($"The value '{feature}' is not a valid PhoneFeatures value.");

                        return BadRequest(errorMessage);
                    }
                }
            }

            // Save the changes to the database
            await _dbContext.SaveChangesAsync();

            return Ok(existingPhone);
        }

        [Route("delete-phone/{id}")]
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

            return NoContent();
        }
    }
}
