namespace TF.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TF.Models;
    using TF.Services;
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = _categoryService.GetAllCategories();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
            public IActionResult CreateCategory([FromBody] Category category)
            {
                if (category == null)
                {
                    return BadRequest();
                }
                var createdCategory = _categoryService.CreateCategory(category);
                return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, createdCategory);
            }

        [HttpPut("{id}")]
            public IActionResult UpdateCategory(int id, [FromBody] Category category)
            {
                if (category == null || id != category.Id)
                {
                    return BadRequest();
                }
                var updatedCategory = _categoryService.UpdateCategory(category);
                if (updatedCategory == null)
                {
                    return NotFound();
                }
                return Ok(updatedCategory);
            }

         [HttpDelete("{id}")]
            public IActionResult DeleteCategory(int id)
            {
                var deletedCategory = _categoryService.DeleteCategory(id);
                if (deletedCategory == null)
                {
                    return NotFound();
                }
                return NoContent();
            }
    }
}