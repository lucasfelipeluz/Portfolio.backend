using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Portfolio.API.ViewModels;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Context;
using Portfolio.Infra.Interfaces;
using Portfolio.Infra.Repositories;
using Portfolio.Services;
using Portfolio.Services.Dto;
using Portfolio.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson(
  options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
  );

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

# region Add Service and Repository of Project
string connectionString = builder.Configuration.GetConnectionString("Banco");
builder.Services.AddDbContext<PortfolioContext>(
  options => options.UseMySql(
    connectionString,
    ServerVersion.AutoDetect(connectionString)
  )
);

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IAboutMeRepository, AboutMeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ISkillService, SkillService>();

// Add Mapper's Service
MapperConfiguration autoMapperConfig = new(cfg =>
{
  cfg.CreateMap<Project, ProjectDto>().ReverseMap();
  cfg.CreateMap<CreateProjectViewModel, ProjectDto>().ReverseMap();
  cfg.CreateMap<UpdateProjectViewModel, ProjectDto>().ReverseMap();
  cfg.CreateMap<Skill, SkillDto>().ReverseMap();
  cfg.CreateMap<CreateSkillViewModel, SkillDto>().ReverseMap();
  cfg.CreateMap<UpdateSkillViewModel, SkillDto>().ReverseMap();
});
builder.Services.AddSingleton(autoMapperConfig.CreateMapper());
#endregion

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
