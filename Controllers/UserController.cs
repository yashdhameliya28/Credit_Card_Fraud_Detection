using Credit_Card_Fraud_Detection.Data;
using Credit_Card_Fraud_Detection.Dtos;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Credit_Card_Fraud_Detection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private AppDbContext _context;
        private IValidator<UserDto> _Validator;

        public UserController(AppDbContext context, IValidator<UserDto> userValidator)
        {
            _context = context;
            _Validator = userValidator;
        }


        #region Get all users
        [HttpGet]
        public async Task<IActionResult> getAllUser()
        {
            var users = await _context.Users
                .Select(u => new UserDto
                {
                    UserId = u.userID,
                    Name = u.name,
                    Email = u.email,
                    Country = u.country,
                    Gender = u.gender,
                    Job = u.job,
                    JoinDate = u.joinDate,
                    State = u.state,
                    CityPop = u.city_pop
                })
                .ToListAsync();
            return Ok(users);
        }
        #endregion

        #region Get user by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> getByID(long id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(user);
        }
        #endregion

        #region Delete user
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteUser(long id)
        {
            var existUser = await _context.Users.FindAsync(id);
            if (existUser == null) return NotFound(new { message = "User not found" });

            _context.Users.Remove(existUser);
            await _context.SaveChangesAsync();
            return Ok(new { message = "User remove successfully" });
        }
        #endregion

        #region Add user
        [HttpPost]
        public async Task<IActionResult> addUser(UserDto dto)
        {
            var result = _Validator.Validate(dto);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors.Select(e => new {
                    Property = e.PropertyName,
                    Error = e.ErrorMessage
                }));
            }
            var user = new Users
            {
                name = dto.Name,
                email = dto.Email,
                country = dto.Country,
                gender = dto.Gender,
                joinDate = dto.JoinDate,
                job = dto.Job,
                state = dto.State,
                city_pop = dto.CityPop
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new { message = "User craeted successfully..." });
        }
        #endregion

        #region Update user
        [HttpPut("{id}")]
        public async Task<IActionResult> updateUser(long id, UserDto dto)
        {

            var result = _Validator.Validate(dto);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null) return NotFound();

            existingUser.name = dto.Name;
            existingUser.email = dto.Email;
            existingUser.country = dto.Country;
            existingUser.gender = dto.Gender;
            existingUser.joinDate = dto.JoinDate;
            existingUser.job = dto.Job;
            existingUser.state = dto.State;
            existingUser.city_pop = dto.CityPop;

            await _context.SaveChangesAsync();
            return Ok(new { message = "user updated sucessfully..." });
        }
        #endregion
    }
}
