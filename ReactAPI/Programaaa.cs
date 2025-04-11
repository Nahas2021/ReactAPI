using ReactAPI;
using ReactAPI.Infrastructure.Data;
using ReactAPI.Infrastructure.Repositories;
using ReactAPI.Services.Interfaces;
using ReactAPI.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ReactAPI.Filters;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test", Version = "v1" });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapGet("/", () => "Hello World");
app.Run();