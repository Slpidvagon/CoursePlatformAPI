using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using Models.CourseRepository;
using SudentRepository;
using AuuthRepository;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<StudentRepository>();
builder.Services.AddScoped<AuthRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();
app.Run();