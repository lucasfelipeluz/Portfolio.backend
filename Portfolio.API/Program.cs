using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Context;
using Portfolio.Infra.Interfaces;
using Portfolio.Infra.Repositories;
using Portfolio.Services;
using Portfolio.Services.Dto;
using Portfolio.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Mapper's Service
MapperConfiguration autoMapperConfig = new(cfg =>
{
  cfg.CreateMap<Project, ProjectDto>().ReverseMap();
});
builder.Services.AddSingleton(autoMapperConfig.CreateMapper());

// Add Service and Repository of Project
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

builder.Services.AddScoped<ISkillRepository, SkillRepository>();

builder.Services.AddScoped<IAboutMeRepository, AboutMeRepository>();

// Connect to database
String connectionString = builder.Configuration.GetConnectionString("Banco");
builder.Services.AddDbContext<PortfolioContext>(
  options => options.UseMySql(
    connectionString,
    ServerVersion.AutoDetect(connectionString)
  )
);

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
