using CCFD.Dtos;
using Credit_Card_Fraud_Detection.Data;
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
        public UserController(AppDbContext context)
        {
            _context = context; 
        }

        [HttpGet]
        public async Task<IActionResult> getAllUser()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("id")]
        public async Task<IActionResult> getByID(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return Ok(user);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> deleteUser(int id)
        {
            var existUser = await _context.Users.FindAsync(id);
            if (existUser == null) return BadRequest("User not exist");

            _context.Users.Remove(existUser);
            await _context.SaveChangesAsync();
            return Ok(new {message = "User remove successfully" });
        }

        [HttpPost]
        public async Task<IActionResult> addUser(UserDto dto)
        {
            var user = new Users
            {

            }
        }
    }
}
