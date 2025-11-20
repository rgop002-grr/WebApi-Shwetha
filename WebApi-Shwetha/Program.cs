using BusinessLayer;
using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApi_Shwetha.Services_LifetimeDemo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SwethaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add dependencies for DI
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<EmployeeService>();

// Register each with different lifetime
builder.Services.AddTransient<TransientService>();
builder.Services.AddScoped<ScopedService>();
builder.Services.AddSingleton<SingletonService>();


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthorization();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var secretKey = "mysupersecretkeyyyyyyyyyyyyyyyyyyy1234567890";
var issuer = "https://localhost:7096";
var audience = "https://localhost:7096";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "JWT Demo API", Version = "v1" });

    // ? Add JWT Bearer Authorization to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token.\n\nExample: Bearer eyJhbGciOiJIUzI1NiIs..."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
