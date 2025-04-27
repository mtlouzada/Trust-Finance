using TF.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<TFDataContext>();

var app = builder.Build();

app.MapControllers();
app.Run();