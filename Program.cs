using TF.Data;
using Microsoft.EntityFrameworkCore;
using TF.Services;
using TF.Configuration;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<TFDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddTransient<TokenService>();
builder.Services.AddControllers();
builder.Services.AddDbContext<TFDataContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();