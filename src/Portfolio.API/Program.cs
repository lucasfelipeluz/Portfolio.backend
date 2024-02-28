using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Portfolio.API.Auth;
using Portfolio.API.Middlewares;
using Portfolio.API.Provider;
using Portfolio.API.ViewModels;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Cache;
using Portfolio.Infra.Context;
using Portfolio.Infra.Interfaces;
using Portfolio.Infra.Repositories;
using Portfolio.Services;
using Portfolio.Services.Dto;
using Portfolio.Services.Interfaces;

// Load .env file
DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

bool isDevelopmentMode = Environment.GetEnvironmentVariable("SERVER_MODE") == "development";

builder
	.Services.AddControllers()
	.AddNewtonsoftJson(options =>
		options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
	);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Add Service and Repository of Project
string dbServer = Environment.GetEnvironmentVariable("DB_SERVER");
string dbPort = Environment.GetEnvironmentVariable("DB_PORT");
string dbName = Environment.GetEnvironmentVariable("DB_NAME");
string dbUser = Environment.GetEnvironmentVariable("DB_USER");
string dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

string connectionString = $"Server={dbServer};Port={dbPort};Database={dbName};Uid={dbUser};Pwd={dbPassword}";
builder.Services.AddDbContext<PortfolioContext>(options =>
	options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder.Services.AddMemoryCache();

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IAboutMeRepository, AboutMeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IProjectSkillRepository, ProjectSkillRepository>();
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<IAboutMeService, AboutMeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IProjectSkillService, ProjectSkillService>();
builder.Services.AddScoped<IActivityService, ActivityService>();

builder.Services.AddScoped<ICachingRepository, CachingRepository>();
builder.Services.AddScoped<ITokenManager, TokenManager>();
builder.Services.AddScoped<IS3Service, S3Service>();

// Add Mapper's Service
MapperConfiguration autoMapperConfig =
	new(cfg =>
	{
		cfg.CreateMap<Project, ProjectDto>().ReverseMap();
		cfg.CreateMap<Project, ProjectWithoutIncludeDto>().ReverseMap();
		cfg.CreateMap<CreateProjectViewModel, ProjectDto>().ReverseMap();
		cfg.CreateMap<UpdateProjectViewModel, ProjectDto>().ReverseMap();

		cfg.CreateMap<Skill, SkillDto>().ReverseMap();
		cfg.CreateMap<Skill, SkillWithoutIncludeDto>().ReverseMap();
		cfg.CreateMap<CreateSkillViewModel, SkillDto>().ReverseMap();
		cfg.CreateMap<UpdateSkillViewModel, SkillDto>().ReverseMap();

		cfg.CreateMap<AboutMe, AboutMeDto>().ReverseMap();
		cfg.CreateMap<CreateAboutMeViewModel, AboutMeDto>().ReverseMap();

		cfg.CreateMap<User, UserDto>().ReverseMap();
		cfg.CreateMap<RegisterViewModel, UserDto>().ReverseMap();

		cfg.CreateMap<Image, ImageDto>().ReverseMap();
		cfg.CreateMap<Image, ImageWithoutIncludeDto>().ReverseMap();

		cfg.CreateMap<ProjectSkill, ProjectSkillDto>().ReverseMap();
		cfg.CreateMap<CreateProjectSkillViewModel, ProjectSkillDto>().ReverseMap();

		cfg.CreateMap<Activity, ActivityDto>().ReverseMap();
		cfg.CreateMap<ActivityDto, CreateActivityViewModel>().ReverseMap();
		cfg.CreateMap<ActivityDto, UpdateActivityViewModel>().ReverseMap();
	});
builder.Services.AddSingleton(autoMapperConfig.CreateMapper());
#endregion

#region JWT
var secretKey = Environment.GetEnvironmentVariable("TOKEN_KEY");

builder
	.Services.AddAuthentication(x =>
	{
		x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	})
	.AddJwtBearer(x =>
	{
		x.RequireHttpsMetadata = false;
		x.SaveToken = true;
		x.TokenValidationParameters = new()
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
			ValidateIssuer = false,
			ValidateAudience = false
		};
	});

#endregion

#region Swagger
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc(
		"v1",
		new OpenApiInfo
		{
			Title = "Portfolio API",
			Version = "v1",
			Description = "Portfolio developed in order to allocate my projects.",
			Contact = new OpenApiContact
			{
				Name = "Lucas Luz",
				Email = "lucasfelipeluz.dev@gmail.com",
				Url = new Uri("https://www.linkedin.com/in/lucasfelipeluz/")
			},
		}
	);

	c.AddSecurityDefinition(
		"Bearer",
		new OpenApiSecurityScheme
		{
			In = ParameterLocation.Header,
			Description = "Please, use Bearer <TOKEN>",
			Name = "Authorization",
			Type = SecuritySchemeType.ApiKey
		}
	);
	c.AddSecurityRequirement(
		new OpenApiSecurityRequirement
		{
			{
				new OpenApiSecurityScheme
				{
					Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
				},
				new string[] { }
			}
		}
	);
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (isDevelopmentMode)
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCustomExceptionHandler();

app.MapControllers();

app.Run();
