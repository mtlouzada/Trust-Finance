namespace TF.Controllers
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Mvc;
    using TF.Models;
    using TF.Data;
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("categories")]
        public async Task<IActionResult> GetAsync(
            [FromServices] TFDataContext context)
        {
            var categories = await context.Categories.ToListAsync();
            return Ok(categories);
        }

        [HttpGet("categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] TFDataContext context)
        {
            var categories = await context
                .Categories
                .FirstOrDefaultAsync(x=>x.Id == id);
            if (categories == null)
                return NotFound(new { Message = "Category not found" });
            
            return Ok(categories);
        }

        [HttpPost("categories")]
        public async Task<IActionResult> PostAsync(
            [FromBody] Category model,
            [FromServices] TFDataContext context)
        {
            await context.Categories.AddAsync(model);
            await context.SaveChangesAsync();
            return Created($"api/categories/{model.Id}", model);
        }

        [HttpPut("categories/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] Category model,
            [FromServices] TFDataContext context)
        {
            var categories = await context
                .Categories
                .FirstOrDefaultAsync(x => x.Id == id);
            if (categories == null)
                return NotFound(new { Message = "Category not found" });

            categories.Name = model.Name;
            categories.Slug = model.Slug;
            context.Categories.Update(categories);

            await context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpDelete("categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id,
            [FromServices] TFDataContext context)
        {
            var category = await context
                .Categories
                .FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound(new { Message = "Category not found" });

            context.Categories.Remove(category);

            await context.SaveChangesAsync();

            return Ok(category);
        }



    }
}