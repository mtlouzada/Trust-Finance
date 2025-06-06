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
    using Microsoft.AspNetCore.Authorization;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
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
                    return NotFound(new ResultViewModel<User>("Conte�do n�o encontrado"));
                return Ok(new ResultViewModel<User>(user));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<User>("Falha interna no servidor"));
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

        [Authorize(Roles = "admin")]
        [HttpPut("users/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] RegisterUserViewModel model,
            [FromServices] TFDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<User>(ModelState.GetErrors()));
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                    return NotFound(new ResultViewModel<User>("Conte�do n�o encontrado"));
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

        [Authorize(Roles = "admin")]
        [HttpDelete("users/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id,
            [FromServices] TFDataContext context)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                    return NotFound(new ResultViewModel<User>("Conte�do n�o encontrado"));
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
