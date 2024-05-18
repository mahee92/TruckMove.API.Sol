using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Text.Json.Serialization;
using TruckMove.API;
using TruckMove.API.BLL;
using TruckMove.API.BLL.Services.Primary;
using TruckMove.API.BLL.Services.PrimaryServices;
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

        builder.Configuration
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
           .AddEnvironmentVariables();

        builder.Services.Configure<MySettings>(builder.Configuration.GetSection("MySettings"));

        // builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddTransient<ICompanyService, CompanyService>();
        builder.Services.AddTransient<IContactService, ContactService>();
        builder.Services.AddTransient<IRepository<CompanyModel>, Repository<CompanyModel>>();
        builder.Services.AddTransient<IRepository<ContactModel>, Repository<ContactModel>>();
        builder.Services.AddTransient<IContactRepository, CompanyRepository>();
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
        if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        });

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseStaticFiles();

        // Serve static files from the external directory
        var uploadPath = builder.Configuration.GetValue<string>("MySettings:FileLocation");
      
        if (!string.IsNullOrEmpty(uploadPath) && Directory.Exists(uploadPath))
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(uploadPath),
                RequestPath = "/uploads"
            });
        }


        app.MapControllers();

        app.Run();
    }
}

