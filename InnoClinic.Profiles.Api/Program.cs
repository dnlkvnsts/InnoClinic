using InnoClinic.Profiles.Application.Interfaces;
using InnoClinic.Profiles.Infrastructure.Persistence;
using InnoClinic.Profiles.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddDbContext<ProfilesDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ProfilesConnection")));



builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();

// Add services to the container.

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

app.Run();
