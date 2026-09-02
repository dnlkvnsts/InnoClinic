using FluentValidation;
using InnoClinic.Profiles.Application.Behaviors;
using InnoClinic.Profiles.Application.Features.Doctors.Queries.GetDoctors;
using InnoClinic.Profiles.Application.Interfaces;
using InnoClinic.Profiles.Application.Validators;
using InnoClinic.Profiles.Infrastructure.Consumers;
using InnoClinic.Profiles.Infrastructure.Persistence;
using InnoClinic.Profiles.Infrastructure.Repositories;
using InnoClinic.Shared.Contracts;
using MassTransit;
using FluentValidation;
using InnoClinic.Profiles.Application.Validators;
using InnoClinic.Profiles.Application.Behaviors;

using MediatR;
using Microsoft.EntityFrameworkCore;

using MediatR;
using MassTransit;
using FluentValidation;
using InnoClinic.Profiles.Application.Validators;
using InnoClinic.Profiles.Application.Behaviors;


var builder = WebApplication.CreateBuilder(args);




builder.Services.AddDbContext<ProfilesDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ProfilesConnection")));



builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();

builder.Services.AddScoped<IPatientRepository, PatientRepository>();

// Add services to the container.




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetDoctorsQuery).Assembly);

    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

});



builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<SpecializationCreatedConsumer>();
    x.AddConsumer<DoctorAccountCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbitHost = builder.Configuration["RabbitMQ:Host"] ?? "localhost";
        var rabbitVHost = builder.Configuration["RabbitMQ:VirtualHost"] ?? "/";
        var rabbitUser = builder.Configuration["RabbitMQ:Username"] ?? "guest";
        var rabbitPass = builder.Configuration["RabbitMQ:Password"] ?? "guest";

        cfg.Host(rabbitHost, rabbitVHost, h =>
        {
            h.Username(rabbitUser);
            h.Password(rabbitPass);
        });

        cfg.ConfigureEndpoints(context);
    });
});



builder.Services.AddValidatorsFromAssemblyContaining<CreatePatientValidator>();



builder.Services.AddValidatorsFromAssemblyContaining<CreatePatientValidator>();


var app = builder.Build();


app.UseMiddleware<InnoClinic.Profiles.Api.Middleware.ValidationExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<InnoClinic.Profiles.Api.Middlewares.ValidationExceptionMiddleware>();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
