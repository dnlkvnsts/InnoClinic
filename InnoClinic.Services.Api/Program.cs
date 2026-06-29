using InnoClinic.Services.Application.Features.Services.Queries.GetServices;
using InnoClinic.Services.Application.Interfaces;
using InnoClinic.Services.Infrastructure.Persistence;
using InnoClinic.Services.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<ServicesDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ServicesConnection")));


builder.Services.AddScoped<IServiceRepository, ServiceRepository>();



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetServicesQuery).Assembly);

});




builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, sfg) =>
    {
        sfg.Host("localhost", "/", h  =>
        {
            h.Username("guest");
            h.Password("guest");
        });


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
