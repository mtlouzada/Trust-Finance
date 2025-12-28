[HttpPost("register")]
public async Task<IActionResult> Register(
    [FromBody] RegisterUserViewModel model,
    [FromServices] AccountService service)
{
    if (!ModelState.IsValid)
        return BadRequest(new ResultViewModel<User>(ModelState.GetErrors()));

    try
    {
        var user = await service.RegisterAsync(model);
        return Created($"api/users/{user.Id}", new ResultViewModel<User>(user));
    }
    catch (DbUpdateException e)
    {
        return BadRequest(new ResultViewModel<User>(e.Message));
    }
}
