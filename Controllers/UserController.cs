namespace TF.Controllers
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Mvc;
    using TF.Models;
    using TF.Data;
    using TF.ViewModels;
    using TF.Extensions;
    using System.Security.Cryptography;
    using System.Text;
    using Microsoft.AspNetCore.Identity;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("users")]
        public async Task<IActionResult> GetAsync(
            [FromServices] TFDataContext context)
        {
            try
            {
                var users = await context.Users.ToListAsync();
                return Ok(new ResultViewModel<List<User>>(users));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<User>>("05X04 - Falha interna no servidor"));
            }
        }
        [HttpGet("users/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] TFDataContext context)
        {
            try
            {
                var user = await context
                    .Users
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                    return NotFound(new ResultViewModel<User>("Conteúdo não encontrado"));
                return Ok(new ResultViewModel<User>(user));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<User>("Falha interna no servidor"));
            }
        }
        [HttpPost("users")]
        public async Task<IActionResult> PostAsync(
            [FromBody] EditorUserViewModel model,
            [FromServices] TFDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<User>(ModelState.GetErrors()));

            try
            {
                var user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Image = model.Image,
                    Slug = model.Slug
                };

                var passwordHasher = new PasswordHasher<User>();
                user.PasswordHash = passwordHasher.HashPassword(user, model.Password);

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
                return Created($"api/users/{user.Id}", new ResultViewModel<User>(user));
            }
            catch (DbUpdateException e)
            {
                return StatusCode(400, new ResultViewModel<User>($"05X03 - {e.InnerException.Message}"));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<User>($"05X04 - Falha interna no servidor: { e.Message }"));
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        [HttpPut("users/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] EditorUserViewModel model,
            [FromServices] TFDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<User>(ModelState.GetErrors()));
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                    return NotFound(new ResultViewModel<User>("Conteúdo não encontrado"));
                user.Name = model.Name;
                user.Email = model.Email;
                user.Image = model.Image;
                user.Slug = model.Slug;
                context.Users.Update(user);
                await context.SaveChangesAsync();
                return Ok(new ResultViewModel<User>(user));
            }
            catch (DbUpdateConcurrencyException e)
            {
                return StatusCode(400, new ResultViewModel<User>($"05X03 - {e.InnerException.Message}"));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<User>($"05X04 - Falha interna no servidor: {e.Message}"));
            }
        }
        [HttpDelete("users/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id,
            [FromServices] TFDataContext context)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                    return NotFound(new ResultViewModel<User>("Conteúdo não encontrado"));
                context.Users.Remove(user);
                await context.SaveChangesAsync();
                return Ok(new ResultViewModel<User>(user));
            }
            catch (DbUpdateConcurrencyException e)
            {
                return StatusCode(400, new ResultViewModel<User>($"05X03 - {e.InnerException.Message}"));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<User>($"05X04 - Falha interna no servidor: {e.Message}"));
            }
        }
    }
}
