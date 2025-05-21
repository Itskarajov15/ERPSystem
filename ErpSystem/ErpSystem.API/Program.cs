using ErpSystem.API.Extensions;
using ErpSystem.API.Middlewares;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseMiddleware<ExceptionHandlingMiddleware>()
    .UseHttpsRedirection()
    .UseCors("CorsPolicy")
    .UseAuthorization();

app.MapControllers();

app.Run();
