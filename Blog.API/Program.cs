using Blog.Application.Configuration;
using Blog.Application.Hubs;
using Blog.Domain.DTOs;
using MyApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment.EnvironmentName;
Console.WriteLine($"Environment: {env}");
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true);

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services
    .AddSingleton(jwtSettings)
    .AddRepository(builder.Configuration)
    .AddService()
    .AddApiConfiguration(jwtSettings!);

var app = builder.Build();

app.RunMigrate();
app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<PostNotificationHub>("/hub/PostNotification");

Console.WriteLine("Blog API is running...");
app.Run();
