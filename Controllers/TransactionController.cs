using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TF.Models;
using TF.Data;
using TF.ViewModels;
using TF.Extensions;

namespace TF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        [HttpGet("transactions")]
        public async Task<IActionResult> GetAsync([FromServices] TFDataContext context)
        {
            try
            {
                var userId = User.GetUserId();
                var transactions = await context
                    .Transactions
                    .Where(t => t.UserId == userId)
                    .ToListAsync();

                return Ok(new ResultViewModel<List<Transaction>>(transactions));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Transaction>>("05X04 - Falha interna no servidor"));
            }
        }

        [HttpGet("transactions/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] TFDataContext context)
        {
            try
            {
                var userId = User.GetUserId();
                var transaction = await context
                    .Transactions
                    .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

                if (transaction == null)
                    return NotFound(new ResultViewModel<Transaction>("Conte�do n�o encontrado"));

                return Ok(new ResultViewModel<Transaction>(transaction));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Transaction>("Falha interna no servidor"));
            }
        }

        [HttpPost("transactions")]
        public async Task<IActionResult> PostAsync(
            [FromBody] EditorTransactionViewModel model,
            [FromServices] TFDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Transaction>(ModelState.GetErrors()));

            try
            {
                var transaction = new Transaction
                {
                    Description = model.Description,
                    Amount = model.Amount,
                    Date = model.Date,
                    CategoryId = model.CategoryId,
                    UserId = User.GetUserId(),
                };

                await context.Transactions.AddAsync(transaction);
                await context.SaveChangesAsync();

                return Created($"api/transactions/{transaction.Id}", new ResultViewModel<Transaction>(transaction));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("transactions/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] EditorTransactionViewModel model,
            [FromServices] TFDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Transaction>(ModelState.GetErrors()));

            try
            {
                var userId = User.GetUserId();
                var transaction = await context
                    .Transactions
                    .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

                if (transaction == null)
                    return NotFound(new ResultViewModel<Transaction>("Conte�do n�o encontrado"));

                transaction.Description = model.Description;
                transaction.Amount = model.Amount;
                transaction.Date = model.Date;
                transaction.CategoryId = model.CategoryId;

                context.Transactions.Update(transaction);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Transaction>(transaction));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Transaction>("Falha interna no servidor"));
            }
        }

        [HttpDelete("transactions/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id,
            [FromServices] TFDataContext context)
        {
            try
            {
                var userId = User.GetUserId();
                var transaction = await context
                    .Transactions
                    .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

                if (transaction == null)
                    return NotFound(new ResultViewModel<Transaction>("Conte�do n�o encontrado"));

                context.Transactions.Remove(transaction);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Transaction>(transaction));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Transaction>("Falha interna no servidor"));
            }
        }
    }
}
