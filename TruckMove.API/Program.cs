using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.Text;
using System.Text.Json.Serialization;
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
using TruckMove.API.DAL.Repositories.Job;
using TruckMove.API.DAL.Repositories.Primary;
using TruckMove.API.Helper;
using TruckMove.API.Settings;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder => builder.AllowAnyOrigin()
                                  .AllowAnyHeader()
                                  .AllowAnyMethod());
        });


        builder.Services.AddAutoMapper(cfg =>
        {
            var profile = new MapProfile();
            cfg.AddProfile(profile);
            profile.CreateGenericMap<UserInputDto, UserModel>();
            profile.CreateGenericMap<UserModel, UserOutputDto>();
            profile.CreateGenericMap<RoleModel, RoleDto>();
            profile.CreateGenericMap<CompanyModel, CompanyDto>();
            profile.CreateGenericMap<CompanyDto, CompanyModel>();
            profile.CreateGenericMap<CompanyDtoUpdate, CompanyModel>();
            profile.CreateGenericMap<CompanyModel, CompanyDtoUpdate>();
            profile.CreateGenericMap<ContactModel, ContactDto>();
            profile.CreateGenericMap<ContactDto, ContactModel>();
            profile.CreateGenericMap<ContactModel, CompanyDtoUpdate>();
            profile.CreateGenericMap<CompanyDtoUpdate, ContactModel>();
            profile.CreateGenericMap<JobDto, JobModel>();

        }, typeof(Program));

        builder.Configuration
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
           .AddEnvironmentVariables();


        Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                     .Filter.ByExcluding(e => e.Level == LogEventLevel.Debug && // Exclude logs with Debug level for specific categories
                                    (e.MessageTemplate.Text.Contains("Execution plan") || // Exclude logs related to execution plans
                                     e.MessageTemplate.Text.Contains("Executing controller factory") || // Exclude logs related to controller factory execution
                                     e.MessageTemplate.Text.Contains("Executed controller factory") || // Exclude logs related to controller factory execution
                                     e.MessageTemplate.Text.Contains("List of registered output formatters") || // Exclude logs related to output formatters
                                     e.MessageTemplate.Text.Contains("No information found on request") || // Exclude logs related to content negotiation
                                     e.MessageTemplate.Text.Contains("Attempting to select an output formatter") || // Exclude logs related to output formatter selection
                                     e.MessageTemplate.Text.Contains("Selected output formatter"))) // Exclude logs related to selected output formatter
                    .CreateLogger();

       builder.Host.UseSerilog();




        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
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

        builder.Services.AddAuthorization();


        builder.Services.Configure<MySettings>(builder.Configuration.GetSection("MySettings"));


        // builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddScoped<ICompanyService, CompanyService>();
        builder.Services.AddScoped<IContactService, ContactService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IMasterDataService, MasterDataService>();
        builder.Services.AddScoped<IJobRepository, JobRepository>();
        builder.Services.AddScoped<IJobService, JobService>();

        builder.Services.AddScoped<IRepository<CompanyModel>, Repository<CompanyModel>>();
        builder.Services.AddScoped<IRepository<ContactModel>, Repository<ContactModel>>();
        builder.Services.AddScoped<IRepository<UserModel>, Repository<UserModel>>();
        builder.Services.AddScoped<IRepository<JobModel>, Repository<JobModel>>();
        builder.Services.AddScoped<IContactRepository, CompanyRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IMasterDataRepository, MasterDataRepository>();
        

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<IAuthUserService, AuthUserService>();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddDbContext<TrukMoveContext>(option =>
      
        option.UseSqlServer(builder.Configuration.GetConnectionString("MyDatabaseConnection"))
                               .EnableSensitiveDataLogging(false) // Disable sensitive data logging
                                .UseLoggerFactory(LoggerFactory.Create(loggingBuilder =>
                                {
                                    loggingBuilder.AddFilter((category, level) =>
                                        !category.Contains("Microsoft.EntityFrameworkCore")); // Exclude EF Core logs
                                }))); // Disable EF Core logging);

        builder.Services.AddSingleton<JwtTokenGenerator>();

        var app = builder.Build();

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

        app.UseSwagger();
        // app.UseSwaggerUI();
        app.UseStaticFiles();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseSerilogRequestLogging();
        app.UseCors("AllowAll");


        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        

        app.UseMiddleware<RequestResponseLoggingMiddleware>();
        app.UseMiddleware<BlacklistMiddleware>();
        app.UseMiddleware<UserInfoMiddleware>();

        app.MapControllers();

        app.Run();
    }
}

