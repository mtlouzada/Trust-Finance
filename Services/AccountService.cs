using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TF.Data;
using TF.Models;
using TF.ViewModels;

namespace TF.Services;

public class AccountService
{
    private readonly TFDataContext _context;
    private readonly PasswordHasher<User> _passwordHasher;

    public AccountService(TFDataContext context)
    {
        _context = context;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<User> RegisterAsync(RegisterUserViewModel model)
    {
        var user = new User
        {
            Name = model.Name,
            Email = model.Email,
            Image = model.Image,
            Slug = model.Slug,
            Role = "admin"
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<string> LoginAsync(LoginViewModel model, TokenService tokenService)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == model.Email);

        if (user == null)
            throw new UnauthorizedAccessException("Usuário ou senha inválidos");

        var result = _passwordHasher.VerifyHashedPassword(
            user, user.PasswordHash, model.Password);

        if (result == PasswordVerificationResult.Failed)
            throw new UnauthorizedAccessException("Usuário ou senha inválidos");

        return tokenService.GenerateToken(user);
    }
}
