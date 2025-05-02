using ReactAPI.Core.Interfaces;
using ReactAPI.Infrastructure.Data;
using ReactAPI.Infrastructure.Repositories;
using ReactAPI.Services.Interfaces;
using ReactAPI.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ReactAPI.Filters;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy => policy.AllowAnyOrigin()//.WithOrigins("http://localhost:3000") // Allow React frontend
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Register DbContext
builder.Services.AddDbContext<EmployeeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

// Register Repositories
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>(); // Add this line
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IRegisterRepository, RegisterRepository>();
builder.Services.AddScoped<ISlotRepository, SlotRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();

// Register Services
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IBookingService, BookingService>(); // Add this line
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<ISlotService, SlotService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
   // c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.OperationFilter<SwaggerFileUploadFilter>();
});
//builder.Services.AddSwaggerGen(options =>
//{
//    options.OperationFilter<FileUploadOperationFilter>();
//});


var app = builder.Build();
app.UseStaticFiles();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseSwaggerUI(c =>
    //{
    //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    //});
    app.UseDeveloperExceptionPage();
}

app.UseCors("AllowAllOrigins"); // Apply CORS policy
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
    
app.Run();