using CreditGrid.Notifier.Domain.Interfaces;
using CreditGrid.Notifier.Infrastructure.Persistence;
using CreditGrid.Notifier.Infrastructure.Persistence.Repositories;
using CreditGrid.Notifier.Infrastructure.Services;
using CreditGrid.Notifier.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient();

builder.Services.AddDbContext<CreditGridNotifierContext>(options => 
        options.UseSqlite(builder.Configuration.GetConnectionString("CreditGridDbConnection")));

builder.Services.AddRepositories();
builder.Services.AddTransient<IReminderService, ReminderService>();

builder.Services.AddEmailClientService(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await MigrateAsync();

async Task MigrateAsync()
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    var dbContext = services.GetRequiredService<CreditGridNotifierContext>();

    bool newDatabase = !dbContext.Database.GetService<IRelationalDatabaseCreator>().Exists();
    dbContext.Database.Migrate();

    if (newDatabase || !dbContext.Templates.Any())
    {
        await dbContext.SeedTemplatesAsync();
    }

    if (newDatabase || !dbContext.CustomerCreditInformation.Any())
    {
        await dbContext.SeedCustomerCreditInformation();
    }

}

app.Run();
