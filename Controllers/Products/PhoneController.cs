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
            if (phone.PhonePerformanceFeatures != null)
            {
                foreach (var feature in phone.PhonePerformanceFeatures)
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
            if (phone.PhoneScreenFeatures != null)
            {
                foreach (var feature in phone.PhoneScreenFeatures)
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
            if (phone.PhoneDesignFeatures != null)
            {
                foreach (var feature in phone.PhoneDesignFeatures)
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
            if (phone.PhoneFeatures != null)
            {
                foreach (var feature in phone.PhoneFeatures)
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
        [HttpPut]
        public async Task<ActionResult<Phone>> UpdatePhone(Guid id, Phone phone)
        {
            var existingPhone = await _dbContext.Phones.FindAsync(id);
            if (existingPhone == null)
            {
                return NotFound();
            }

            // Validate against schemas that define along with model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate Performance Features
            if (phone.PhonePerformanceFeatures != null)
            {
                foreach (var feature in phone.PhonePerformanceFeatures)
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
            if (phone.PhoneScreenFeatures != null)
            {
                foreach (var feature in phone.PhoneScreenFeatures)
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
            if (phone.PhoneDesignFeatures != null)
            {
                foreach (var feature in phone.PhoneDesignFeatures)
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
            if (phone.PhoneFeatures != null)
            {
                foreach (var feature in phone.PhoneFeatures)
                {
                    if (!Enum.IsDefined(typeof(PhoneFeatures), feature))
                    {
                        var errorMessage = new ErrorResponse(ErrorTitle.ValidationTitle, ErrorStatus.BadRequest, ErrorType.Validation);
                        errorMessage.AddError($"The value '{feature}' is not a valid PhoneFeatures value.");

                        return BadRequest(errorMessage);
                    }
                }
            }

            // Update the existing phone with the values from the incoming phone
            existingPhone.Name = phone.Name;
            existingPhone.Price = phone.Price;
            existingPhone.Image = phone.Image;
            existingPhone.Description = phone.Description;
            existingPhone.Manufacture = phone.Manufacture;
            existingPhone.CPUName = phone.CPUName;
            existingPhone.CPUType = phone.CPUType;
            existingPhone.CPUSpeedBase = phone.CPUSpeedBase;
            existingPhone.CPUSpeedBoost = phone.CPUSpeedBoost;
            existingPhone.RAM = phone.RAM;
            existingPhone.RAMSpeed = phone.RAMSpeed;
            existingPhone.InStorage = phone.InStorage;
            existingPhone.PhonePerformanceFeatures = phone.PhonePerformanceFeatures;
            existingPhone.ScreenSize = phone.ScreenSize;
            existingPhone.Resolution = phone.Resolution;
            existingPhone.ScreenHz = phone.ScreenHz;
            existingPhone.Nits = phone.Nits;
            existingPhone.Ppi = phone.Ppi;
            existingPhone.PhoneScreenFeatures = phone.PhoneScreenFeatures;
            existingPhone.Weight = phone.Weight;
            existingPhone.Height = phone.Height;
            existingPhone.Width = phone.Width;
            existingPhone.PhoneDesignFeatures = phone.PhoneDesignFeatures;
            existingPhone.MainCameraMP = phone.MainCameraMP;
            existingPhone.FrontCameraMP = phone.FrontCameraMP;
            existingPhone.BatteryPower = phone.BatteryPower;
            existingPhone.Charging = phone.Charging;
            existingPhone.MagSafe = phone.MagSafe;
            existingPhone.PhoneFeatures = phone.PhoneFeatures;

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
