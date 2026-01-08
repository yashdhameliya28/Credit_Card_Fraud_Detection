using Credit_Card_Fraud_Detection.Data;
using Credit_Card_Fraud_Detection.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Credit_Card_Fraud_Detection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceHistoryController : ControllerBase
    {
        private AppDbContext _context;
        private IValidator<DeviceHistoryDTO> _Validator;

        public DeviceHistoryController(AppDbContext context, IValidator<DeviceHistoryDTO> validator)
        {
            _context = context;
            _Validator = validator;
        }

        #region Get all Devices
        [HttpGet]
        public async Task<IActionResult> getAllDevice()
        {
            var devices = await _context.DeviceHistory
                .Select(x => new DeviceHistoryDTO
                {
                    deviceType = x.deviceType,
                    deviceName = x.deviceName,
                })
                .ToListAsync();

            return Ok(devices);
        }
        #endregion

        #region Get device by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> getByID(long id)
        {
            var device = await _context.DeviceHistory.FindAsync(id);
            if (device == null) return NotFound(new { message = "Device not found" });

            return Ok(device);
        }
        #endregion

        #region Delete Device
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteDevice(long id)
        {
            var existDevice = await _context.DeviceHistory.FindAsync(id);
            if (existDevice == null) return BadRequest(new { message = "Device Not Found..." });

            _context.DeviceHistory.Remove(existDevice);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Device deleted..."});
        }
        #endregion

        #region Add Deivce
        [HttpPost]
        public async Task<IActionResult> addDevice(DeviceHistoryDTO dto)
        {
            var result = _Validator.Validate(dto);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors.Select(e => new {
                    Property = e.PropertyName,
                    Error = e.ErrorMessage
                }));
            }

            var device = new DeviceHistory
            {
                deviceName = dto.deviceName,
                deviceType = dto.deviceType,
                userID = dto.userID
            };

            _context.DeviceHistory.Add(device);
            await _context.SaveChangesAsync();
            return Ok(new {message = "Device added..."});
        }
        #endregion

        #region Update Device
        [HttpPut("{id}")]
        public async Task<IActionResult> updateDevice(long id,  DeviceHistoryDTO dto)
        {
            var result = _Validator.Validate(dto);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var existingDevice = await _context.DeviceHistory.FindAsync(id);
            if (existingDevice == null) return BadRequest(new { message = "Deivce not found..." });

            existingDevice.deviceName = dto.deviceName;
            existingDevice.userID  = dto.userID;
            existingDevice.deviceType = dto.deviceType;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Deivce updated..." });
        }
        #endregion
    }
}
