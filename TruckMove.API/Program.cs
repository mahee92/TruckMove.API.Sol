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

        var app = builder.Build();

        app.UseSwagger();
        // app.UseSwaggerUI();
        app.UseStaticFiles();
        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        //{
        //    app.UseSwagger();
        //    app.UseSwaggerUI(c =>
        //    {
        //        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        //        c.RoutePrefix = string.Empty; // Sets Swagger UI at the app's root
        //    });
        //}
        //else
        //{
        //    app.UseSwagger();
        //    app.UseSwaggerUI(c =>
        //    {
        //        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        //        c.RoutePrefix = string.Empty; // Sets Swagger UI at the app's root
        //    });
        //}

        app.UseSwaggerUI(c =>
        {
            if (app.Environment.IsDevelopment())
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API V1");
            }
            else
            {
                // To deploy on IIS
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API V1");
            }

        });

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

