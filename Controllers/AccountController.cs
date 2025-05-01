namespace TF.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using TF.Data;
    using TF.Models;
    using TF.Services;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly TFDataContext _context;
        private readonly TokenServices _tokenServices;
        public AccountController(TFDataContext context, TokenServices tokenServices)
        {
            _context = context;
            _tokenServices = tokenServices;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            if (await UserExists(user.Name))
                return BadRequest("User already exists");

            var passwordHash = new PasswordHasher<User>();
            user.PasswordHash = passwordHash.HashPassword(user, user.PasswordHash);
            _context.Users.Add(user);

            await _context.SaveChangesAsync();
            return Ok(new { token = _tokenServices.GenerateToken(user) });
        }
        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.Name == username.ToLower());
        }
    }
}