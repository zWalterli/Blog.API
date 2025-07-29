using Blog.Application.Configuration;
using Blog.Application.Hubs;
using Blog.Domain.DTOs;
using MyApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<PostNotificationHub>("/hub/PostNotification");

Console.WriteLine("Blog API is running...");
app.Run();