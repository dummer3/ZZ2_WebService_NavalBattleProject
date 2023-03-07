using Microsoft.EntityFrameworkCore;
using NavalWar.Business;
using NavalWar.DAL;
using NavalWar.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// allow localhost:4200
builder.Services.AddCors(options =>
{
    options.AddPolicy(
     "CorsPolicy",
     builder => builder.WithOrigins("http://localhost:4200")
     .AllowAnyMethod()
     .AllowAnyHeader()
     .AllowCredentials());
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<NavalContext>(opt => opt.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-53bc9b9d-9d6a-45d4-8429-2a2761773502;Trusted_Connection=True;MultipleActiveResultSets=true"));
builder.Services.AddDbContext<NavalContext>(opt => opt.UseSqlServer("Server=tcp:navalwar-server.database.windows.net,1433;Initial Catalog=NavalWar_Database;Persist Security Info=False;User ID=myadmin;Password=&SQLdatabase2023&;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));

//add Your dependencies
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();



var app = builder.Build();

app.UseCors("CorsPolicy");

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
