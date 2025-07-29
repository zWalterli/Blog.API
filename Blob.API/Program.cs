using System.Text;
using Blog.Application.Configuration;
using Blog.Application.Hubs;
using Blog.Domain.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// 2. JWT Settings
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services
    .AddSingleton(jwtSettings)
    .AddRepository(builder.Configuration)
    .AddService()
    .AddApiConfiguration(jwtSettings!);


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<PostNotificationHub>("/hub/PostNotification");

app.Run();