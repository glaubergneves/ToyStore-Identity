using Microsoft.EntityFrameworkCore;
using ToyStore.Application.Interfaces;
using ToyStore.Application.Repositories;
using ToyStore.Application.Services;
using ToyStore.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IToyService, ToyService>();
builder.Services.AddScoped<IStoreService, StoreService>();

builder.Services.AddScoped<IToyRepository, ToyRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
