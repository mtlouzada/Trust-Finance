using Microsoft.EntityFrameworkCore;
using TF.Data;
using TF.Models;

namespace Trust_Finance.Services;

public class TransactionService
{
    private readonly TFDataContext _context;

    public TransactionService(TFDataContext context)
    {
        _context = context;
    }

    public async Task<List<Transaction>> GetAllAsync(int userId)
    {
        return await _context
            .Transactions
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }

    public async Task<Transaction?> GetByIdAsync(int id, int userId)
    {
        return await _context
            .Transactions
            .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
    }

    public async Task<Transaction> CreateAsync(
        string description,
        decimal amount,
        DateTime date,
        int categoryId,
        int userId)
    {
        var transaction = new Transaction
        {
            Description = description,
            Amount = amount,
            Date = date,
            CategoryId = categoryId,
            UserId = userId
        };

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return transaction;
    }

    public async Task<Transaction> UpdateAsync(
        int id,
        string description,
        decimal amount,
        DateTime date,
        int categoryId,
        int userId)
    {
        var transaction = await GetByIdAsync(id, userId)
            ?? throw new KeyNotFoundException("Transaction not found");

        transaction.Description = description;
        transaction.Amount = amount;
        transaction.Date = date;
        transaction.CategoryId = categoryId;

        await _context.SaveChangesAsync();
        return transaction;
    }

    public async Task<Transaction> DeleteAsync(int id, int userId)
    {
        var transaction = await GetByIdAsync(id, userId)
            ?? throw new KeyNotFoundException("Transaction not found");

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();

        return transaction;
    }
}
