using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using TruckMove.API.BLL.Services;
using TruckMove.API.DAL;
using TruckMove.API.DAL.Models;
using TruckMove.API.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddTransient<ICompanyService, CompanyService>();   
builder.Services.AddTransient<ICompanyDataRepository, CompanyDataRepository>(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TrukMoveLocalContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("MyDatabaseConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
