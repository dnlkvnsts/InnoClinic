using InnoClinic.Profiles.Application.Consumers;
using InnoClinic.Profiles.Application.Contracts;
using InnoClinic.Profiles.Application.Features.Doctors.Queries.GetDoctors;
using InnoClinic.Profiles.Application.Interfaces;
using InnoClinic.Profiles.Infrastructure.Persistence;
using InnoClinic.Profiles.Infrastructure.Repositories;
using MassTransit;
using MediatR;
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





builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetDoctorsQuery).Assembly);

});



builder.Services.AddMassTransit(x =>
{

    x.AddConsumer<SpecializationCreatedConsumer>();

    x.UsingRabbitMq((context, sfg) =>
    {
        sfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        sfg.ConfigureEndpoints(context);
    });

});




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
