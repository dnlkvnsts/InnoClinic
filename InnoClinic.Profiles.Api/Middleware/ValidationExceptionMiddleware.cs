using FluentValidation;
using System.Text.Json;

namespace InnoClinic.Profiles.Api.Middleware
{
    public class ValidationExceptionMiddleware
    {

        private readonly RequestDelegate _next;

        public ValidationExceptionMiddleware(RequestDelegate next)
        {

            _next = next;

        }



        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex) 
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";



                var errors = ex.Errors.Select(e => new
                {

                    Field = e.PropertyName,
                    Error = e.ErrorMessage

                });



                var response = JsonSerializer.Serialize(new { Message = "Validation failed", Errors = errors });

                await context.Response.WriteAsync(response);

            }

        }
    }
}
