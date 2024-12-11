using Bulkify.Repository.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BulkifyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

var _context = services.GetRequiredService<BulkifyDbContext>();
var _loggerfactory = services.GetRequiredService<ILoggerFactory>();

try
{
    _context.Database.MigrateAsync();

}catch(Exception ex)
{
    var logger = _loggerfactory.CreateLogger<Program>();
    logger.LogError(ex, "error whil migrating");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
