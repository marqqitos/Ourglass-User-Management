using Microsoft.EntityFrameworkCore;
using UserManagementCommons.Helpers;
using UserManagementCommons.Interfaces.Repositories;
using UserManagementCommons.Interfaces.Services;
using UserManagementCommons.Repositories;
using UserManagementCommons.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("UserManagementDatabase")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddCors(p => p.AddPolicy("userManagementCors", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("userManagementCors");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
