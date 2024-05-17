using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Text.Json.Serialization;
using TruckMove.API.BLL;
using TruckMove.API.BLL.Services.Primary;
using TruckMove.API.BLL.Services.PrimaryServices;
using TruckMove.API.DAL;
using TruckMove.API.DAL.Models;
using TruckMove.API.DAL.Repositories;
using TruckMove.API.DAL.Repositories.Primary;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Load configuration based on the current environment
        var environment = builder.Environment.EnvironmentName;
        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        builder.Services.AddControllers().AddJsonOptions(x =>
         x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

        builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

        // builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddTransient<ICompanyService, CompanyService>();
         builder.Services.AddTransient<IContactService,ContactService>();
        builder.Services.AddTransient<IRepository<CompanyModel>, Repository<CompanyModel>>();
        builder.Services.AddTransient<IRepository<ContactModel>, Repository<ContactModel>>();
        builder.Services.AddTransient<IContactService, ContactService>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<TrukMoveLocalContext>(option =>
        option.UseSqlServer(builder.Configuration.GetConnectionString("MyDatabaseConnection")));
        //"Server=(localdb)\\localdbtest;Database=TrukMoveLocal;Trusted_Connection=True;"


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
    }
}

