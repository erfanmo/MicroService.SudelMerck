using Microsoft.EntityFrameworkCore;
using SudelMerck.BasketService.Infrastracture.Context;
using SudelMerck.BasketService.Infrastracture.MappingProfile;
using SudelMerck.BasketService.Model.Services;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(BasketMappingProfile));
builder.Services.AddTransient<IBasketServices, BasketService>();

builder.Services.AddDbContext<BasketDataBaseContext>(p => p.UseSqlServer(configuration["BasketConnection"]));

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

app.Run();
