using Jul.API;
using Jul.Repository.Interfaces;
using Jul.Repository.Logic;
using Jul.Repository.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//HeroControllerTest gg = new HeroControllerTest();
//Hero h = new Hero(); // concrete - always getting the same blueprint
//h.hat();
// Add services to the container.
// all the featuters .Core can use

builder.Services.AddControllers();
builder.Services.AddDbContext<Jul.Repository.Dbcontext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("connection")));

// Define our DI -error code cannot resolve serviceprovider, contract
builder.Services.AddScoped<IHeroRepository, HeroRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// this code is called each time F5 or a click, with more is happening
var app = builder.Build();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
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
