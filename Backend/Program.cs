using Backend.Data;
using Backend.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING"),
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure());
});

builder.Services.AddScoped<ITemplateService, TemplateService>();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var databaseExists = await dbContext.Database.CanConnectAsync();

    if (!databaseExists)
    {
        await Seeder.CreateDb(dbContext);
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
