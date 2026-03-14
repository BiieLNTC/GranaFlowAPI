using GranaFlow.API.Extensions;
using GranaFlow.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

builder.Services.AddDatabase(builder.Configuration)
                .AddApiDocumentation()
                .AddApplicationServices(builder.Configuration)
                .AddJWTConfig(builder.Configuration)
                .AddApiCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CorrelationIdMiddleware>()
    .UseMiddleware<ExceptionMiddleware>()
    .UseMiddleware<RequestLoggingMiddleware>()
    .UseCors("DefaultCors")
    .UseHttpsRedirection()
    .UseAuthentication()
    .UseAuthorization();

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
