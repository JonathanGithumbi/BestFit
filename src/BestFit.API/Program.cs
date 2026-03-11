using BestFit.Application.Mappings;
using BestFit.Application.Services;
using BestFit.Domain.Entities;
using BestFit.Domain.Interfaces;
using BestFit.Infrastructure.Data;
using BestFit.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using BestFit.API.Middlewares;
using Microsoft.Extensions.FileProviders;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logger = new LoggerConfiguration()
    .WriteTo.File("logs/BestFit_Logs.txt",rollingInterval: RollingInterval.Day)
    .MinimumLevel.Warning()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
builder.Services.AddScoped<ITokenRepository,TokenRepository>();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<CustomerMeasurementProfileService>();
builder.Services.AddScoped<OrderDetailsService>();
builder.Services.AddScoped<OrderProductService>();
builder.Services.AddScoped<ProductImageService>();
builder.Services.AddScoped<ProductMeasurementProfileService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<FeaturedContentService>();
builder.Services.AddScoped<HomeService>();
builder.Services.AddScoped<ProfileService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "BestFit API", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme="Oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
        });
});


//Injecting the DB Context Class
builder.Services.AddDbContext<BestFitDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BestFitConnectionString"));
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options=>options.SignIn.RequireConfirmedAccount=false)
    .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("BestFit")
    .AddEntityFrameworkStores<BestFitDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders()
    ;

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;  
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

//Session & Cookie
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(
    opts =>
    {
        opts.IdleTimeout = TimeSpan.FromMinutes(120);
        opts.Cookie.HttpOnly = true;
    });

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseHttpsRedirection();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","assets","images")),
    RequestPath = "/wwwroot/images"
});

app.MapControllers();

app.Run();
