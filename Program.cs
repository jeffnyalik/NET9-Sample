using Microsoft.Extensions.Primitives;
using NLog;
using Webbs.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.configureCors();
builder.Services.configureIIsConfiguration();
builder.Services.configureLoggerServices();
builder.Services.configureSqlServer(builder.Configuration);
builder.Services.configureIdentitySqlServer(builder.Configuration);
builder.Services.AddControllers();
builder.Services.configureJsonSerializerOptions();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.configureStockRepository();
builder.Services.configureCommentRepository();
builder.Services.configureJwt(builder.Configuration);
builder.Services.configureIdentity(builder.Configuration);
LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Weather App");

    });

}
//app.MapOpenApi();
//app.UseSwaggerUI(options =>
//{
//    options.SwaggerEndpoint("/openapi/v1.json", "Weather App");
//});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
