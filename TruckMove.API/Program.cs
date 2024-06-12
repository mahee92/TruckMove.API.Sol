using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog.Events;
using Serilog;
using System.Text;
using TruckMove.API.BLL;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.JobDTOs;
using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.BLL.Models.PrimaryDTO;
using TruckMove.API.BLL.Models.UserManagmentDTO;
using TruckMove.API.BLL.Services;
using TruckMove.API.BLL.Services.JobServices;
using TruckMove.API.BLL.Services.Primary;
using TruckMove.API.BLL.Services.PrimaryServices;
using TruckMove.API.DAL.Models;
using TruckMove.API.DAL.Repositories;
using TruckMove.API.DAL.Repositories.JobRepositories;
using TruckMove.API.DAL.Repositories.PrimaryRepositories;
using TruckMove.API.Helper;
using TruckMove.API.Settings;

internal class Program
{
    private static void Main(string[] args)
    {


        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var logger = new LoggerConfiguration()
          .ReadFrom.Configuration(builder.Configuration)
          .Enrich.FromLogContext()
          .CreateLogger(); ;

        Log.Logger = logger; // Set the global logger

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(logger);
        // Add services to the container
        ConfigureServices(builder);

        var app = builder.Build();

        Log.Information("Application started successfully.");


        // Configure the HTTP request pipeline
        ConfigureMiddleware(app, builder.Configuration);

        app.Run();
        Log.CloseAndFlush();
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();

        // Configure CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder => builder.AllowAnyOrigin()
                                  .AllowAnyHeader()
                                  .AllowAnyMethod());
        });


        // Configure app settings
        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        // Configure JWT authentication
        ConfigureAuthentication(builder);

        // Configure Swagger
        ConfigureSwagger(builder);

        // Configure custom services
        ConfigureCustomServices(builder);

        // Configure DI
        ConfigureDI(builder);

        // Configure EF Core DbContext
        builder.Services.AddDbContext<TrukMoveContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("MyDatabaseConnection")));

        // Configure Auto Mapper
        ConfigureAutoMapper(builder);

        // Add other necessary services
        builder.Services.AddHttpContextAccessor();

    }
    private static void ConfigureAutoMapper(WebApplicationBuilder builder)
    {
        // Configure AutoMapper
        builder.Services.AddAutoMapper(cfg =>
        {
            var profile = new MapProfile();
            cfg.AddProfile(profile);
            profile.CreateGenericMap<UserInputDto, User>();
            profile.CreateGenericMap<User, UserOutputDto>();
            profile.CreateGenericMap<Role, RoleDto>();
            profile.CreateGenericMap<Company, CompanyDto>();
            profile.CreateGenericMap<CompanyDto, Company>();
            profile.CreateGenericMap<CompanyDtoUpdate, Company>();
            profile.CreateGenericMap<Company, CompanyDtoUpdate>();
            profile.CreateGenericMap<Contact, ContactDto>();
            profile.CreateGenericMap<ContactDto, Contact>();
            profile.CreateGenericMap<Contact, CompanyDtoUpdate>();
            profile.CreateGenericMap<CompanyDtoUpdate, Contact>();
            profile.CreateGenericMap<JobDto, Job>();
            profile.CreateGenericMap<Job, JobDto>();
            profile.CreateGenericMap<Job, JobOutPutDTO>();
        }, typeof(Program));
    }
    private static void ConfigureAuthentication(WebApplicationBuilder builder)
    {
        var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:SecretKey"]);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration.GetValue<string>("JwtSettings:Issuer"),
                ValidAudience = builder.Configuration.GetValue<string>("JwtSettings:Audience"),
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });

        builder.Services.AddScoped<IAuthUserService, AuthUserService>();
        builder.Services.AddSingleton<JwtTokenGenerator>();
    }

    private static void ConfigureSwagger(WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

            // Add JWT Authentication
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            };
            c.AddSecurityDefinition("Bearer", securityScheme);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            };
            c.AddSecurityRequirement(securityRequirement);
        });

        builder.Services.AddEndpointsApiExplorer();
    }

    private static void ConfigureCustomServices(WebApplicationBuilder builder)
    {
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
        builder.Services.Configure<MySettings>(builder.Configuration.GetSection("MySettings"));


    }
    private static void ConfigureDI(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICompanyService, CompanyService>();
        builder.Services.AddScoped<IContactService, ContactService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IJobService, JobService>();
        builder.Services.AddScoped<IMasterDataService, MasterDataService>();

        builder.Services.AddScoped<IRepository<Company>, Repository<Company>>();
        builder.Services.AddScoped<IRepository<Contact>, Repository<Contact>>();
        builder.Services.AddScoped<IRepository<User>, Repository<User>>();
        builder.Services.AddScoped<IRepository<Job>, Repository<Job>>();
        builder.Services.AddScoped<IRepository<JobContact>, Repository<JobContact>>();
        builder.Services.AddScoped<IContactRepository, CompanyRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IJobRepository, JobRepository>();
        builder.Services.AddScoped<IMasterDataRepository, MasterDataRepository>();
    }
    private static void ConfigureMiddleware(WebApplication app, IConfiguration configuration)
    {
        // Serve static files
        app.UseStaticFiles();

        // Serve static files from the external directory
        var uploadPath = configuration.GetValue<string>("MySettings:FileLocation");
        if (!string.IsNullOrEmpty(uploadPath) && Directory.Exists(uploadPath))
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(uploadPath),
                RequestPath = "/uploads"
            });
        }

        // Swagger setup
        app.UseSwagger();
        if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
        {
            app.UseSwaggerUI();
        }

        app.UseCors("AllowAll");

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        // Custom middleware
        app.UseMiddleware<RequestResponseLoggingMiddleware>();
        app.UseMiddleware<BlacklistMiddleware>();
        app.UseMiddleware<UserInfoMiddleware>();

        app.MapControllers();

    }
}
