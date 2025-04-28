namespace TF.Controllers
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Mvc;
    using TF.Models;
    using TF.Data;
    using TF.ViewModels;
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("categories")]
        public async Task<IActionResult> GetAsync(
            [FromServices] TFDataContext context)
        {     
             try
                {
                    var categories = await context.Categories.ToListAsync();
                    return Ok(new ResultViewModel<List<Category>>(categories));
                }
             catch
                {
                    return StatusCode(500, new ResultViewModel<List<Category>>("05X04 - Falha interna no servidor"));
                }
        }
      

        [HttpGet("categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] TFDataContext context)
        {
            try
            {
                var category = await context
                    .Categories
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound(new ResultViewModel<Category>("Conteúdo não encontrado"));

                return Ok(new ResultViewModel<Category>(category));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Category>("Falha interna no servidor"));
            }
        }

        [HttpPost("categories")]
        public async Task<IActionResult> PostAsync(
            [FromBody] CreateCategoryViewModel model,
            [FromServices] TFDataContext context)
        {
            try
            {
                var category = new Category
                {
                    Name = model.Name,
                    Slug = model.Slug
                }
                await context.Categories.AddAsync(model);
                await context.SaveChangesAsync();

                return Created($"api/categories/{category.Id}", category);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Não foi possível incluir a categoria");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Falha interna no servidor");
            }
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