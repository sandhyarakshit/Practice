using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolApplication.Models;
using SchoolApplication.Repositories;
using System.Net.Http;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddCors((options) =>
{
    options.AddPolicy("angularApplication", (builder) =>
    {
        builder.WithOrigins("http://localhost:4200").AllowAnyHeader().WithMethods("GET", "POST", "PUT", "DELETE")
        .WithExposedHeaders("*");
    });
});
builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("StudentDb");
builder.Services.AddDbContext<StudentDbContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x => {
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "localhost",
        ValidAudience = "localhost",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwtConfig:Key"])),
        ClockSkew = TimeSpan.Zero

    };
});
//builder.Services.AddDbContext<StudentDbContext>(Options => Options.UseSqlServer(IConfiguration.GetConnectionString("StudentDb")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IStudentRepository,SqlStudentRepository>();
builder.Services.AddScoped<IImageRepository, LocalStorageImageRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Student).Assembly);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("angularApplication");
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
