namespace TF.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TF.Services;
    using Microsoft.EntityFrameworkCore;
    using TF.Data;
    using System.Threading.Tasks;
    using TF.Extensions;
    using TF.ViewModels;
    using Microsoft.AspNetCore.Identity;
    using TF.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
       [HttpPost("login")]
        public async Task<IActionResult> Login(
       [FromBody] LoginViewModel model,
       [FromServices] TFDataContext context,
       [FromServices] TokenService tokenService)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            var user = await context
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user == null)
                return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));

            var passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

            if (result == PasswordVerificationResult.Failed)
                return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));

            try
            {
                var token = tokenService.GenerateToken(user);
                return Ok(new ResultViewModel<string>(token, null));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("05X04 - Falha interna no servidor"));
            }
        }
    }
}