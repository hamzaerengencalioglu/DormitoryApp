using System.Data.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using YurtApps.Application.Interfaces;
using YurtApps.Application.Services;
using YurtApps.Domain.IRepositories;
using YurtApps.Infrastructure;
using YurtApps.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<AppDbContext>();
builder.Services.AddScoped(typeof(IEFRepository<>), typeof(EFRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDormitoryService, DormitoryService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
