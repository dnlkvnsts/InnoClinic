using InnoClinic.Appointments.Application.Interfaces;
using InnoClinic.Appointments.Infrastructure.Persistence;
using InnoClinic.Appointments.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

using InnoClinic.Appointments.Application.Features.Appointments.Commands.CreateAppointment;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<AppointmentsDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("AppointmentsConnection")));

builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateAppointmentCommand).Assembly);

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
