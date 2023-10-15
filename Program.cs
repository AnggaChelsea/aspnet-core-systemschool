using Microsoft.EntityFrameworkCore;
using NetAngularAuthWebApi.Configuration;
using NetAngularAuthWebApi.Context;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;
using NetAngularAuthWebApi.Services;
using NetAngularAuthWebApi.Services.Student;
using Sieve.Services;

Log.Logger = new LoggerConfiguration()
.MinimumLevel.Debug()
.WriteTo.Console()
.WriteTo.File("logs/controller.txt", rollingInterval: RollingInterval.Day).CreateLogger();



var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IMailService, LocalMailService>();
builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddAuthorization(options => {
    options.AddPolicy("AllowStudentandTeacher", policy => policy.RequireRole("teacher", "student"));
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<SieveProcessor>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolic", builder =>
    {
          builder.WithOrigins("http://localhost:4200", "http://localhost:4300")
          .AllowAnyMethod()
          .AllowAnyHeader()
          .AllowCredentials();
    });
});
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultString"));
});
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt => {
    var key = Encoding.ASCII.GetBytes("J1LK223PKYD7HXXD5Q7V9AKS5ZTLSY-J1LK223PKYD7HXXD5Q7V9AKS5ZTLSY");
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters(){
        ValidateIssuerSigningKey = false,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false, //for development purposes
        ValidateAudience = false, //for development purposes
        RequireExpirationTime = false, //for development purposes
        ValidateLifetime = true, //for development
    };
});


builder.Services.AddControllersWithViews();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("MyPolic");
app.UseAuthentication();
app.UseAuthorization();


// app.Run(async (context) => {
//     await context.Response.WriteAsync("Hallo check di https://localhost:7023");
// });


app.MapControllers();

app.Run();