using BestFit.Application.Mappings;
using BestFit.Application.Services;
using BestFit.Domain.Entities;
using BestFit.Domain.Interfaces;
using BestFit.Infrastructure.Data;
using BestFit.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Injecting the DB Context Class
builder.Services.AddDbContext<BestFitDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BestFitConnectionString"));
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<BestFitDbContext>()
    .AddDefaultTokenProviders();



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
