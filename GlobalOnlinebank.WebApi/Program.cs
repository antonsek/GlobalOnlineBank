using GlobalOnlinebank.Application.Interfaces;
using GlobalOnlinebank.Application.Services;
using GlobalOnlinebank.Domain.Interfaces;
using GlobalOnlinebank.Infrastructure.Data;
using GlobalOnlinebank.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IContragentRepository, ContragentRepository>();
builder.Services.AddScoped<IContragentService, ContragentService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<ITariffRepository, TariffRepository>();
builder.Services.AddScoped<ITariffService, TariffService>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();