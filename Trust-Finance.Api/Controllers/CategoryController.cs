using Microsoft.AspNetCore.Mvc;
using TF.Extensions;
using TF.Models;
using TF.ViewModels;
using Trust_Finance.Services;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(
        [FromServices] CategoryService service)
        => Ok(new ResultViewModel<List<Category>>(await service.GetAllAsync()));

    [HttpPost]
    public async Task<IActionResult> Post(
        [FromBody] EditorCategoryViewModel model,
        [FromServices] CategoryService service)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));

        try
        {
            var category = await service.CreateAsync(model.Name, model.Slug);
            return Created($"api/categories/{category.Id}", category);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ResultViewModel<Category>(ex.Message));
        }
    }
}
