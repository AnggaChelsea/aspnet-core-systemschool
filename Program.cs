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
using Microsoft.AspNetCore.StaticFiles;
using NetAngularAuthWebApi.Services.Email;
using DinkToPdf.Contracts;
using DinkToPdf;
using Serilog;
using NetAngularAuthWebApi.helpers;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using ITfoxtec.Identity.Saml2;
using ITfoxtec.Identity.Saml2.Schemas.Metadata;
using ITfoxtec.Identity.Saml2.MvcCore.Configuration;


Log.Logger = new LoggerConfiguration()
.MinimumLevel.Debug()
.WriteTo.Console()
.WriteTo.File("logs/controller.txt", rollingInterval: RollingInterval.Day).CreateLogger();


 Log.Information("Starting web application");

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));


var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IMailService, LocalMailService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IExcel, ExcelService>();
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

builder.Services.AddScoped<pdfGenerateDinkToPdf>();
//for response file
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

//saml

builder.Services.AddRazorPages();
builder.Services.Configure<Saml2Configuration>(builder.Configuration.GetSection("Saml2"));

builder.Services.Configure<Saml2Configuration>(saml2Configuration =>
    {
        saml2Configuration.AllowedAudienceUris.Add(saml2Configuration.Issuer);

        var entityDescriptor = new EntityDescriptor();
        entityDescriptor.ReadIdPSsoDescriptorFromUrl(new Uri(builder.Configuration["Saml2:IdPMetadata"]));
        if (entityDescriptor.IdPSsoDescriptor != null)
        {
            saml2Configuration.SingleSignOnDestination = entityDescriptor.IdPSsoDescriptor.SingleSignOnServices.First().Location;
            saml2Configuration.SignatureValidationCertificates.AddRange(entityDescriptor.IdPSsoDescriptor.SigningCertificates);
        }
        else
        {
            throw new Exception("IdPSsoDescriptor not loaded from metadata.");
        }
    });

    builder.Services.AddSaml2();

   

//end

//add inject service
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

//add automapper object dto
builder.Services.AddAutoMapper(typeof(Program).Assembly);
//inject the mediatr to our DI
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));

builder.Services.AddAuthorization(options => {
    options.AddPolicy("AllowStudentandTeacher", policy => policy.RequireRole("teacher", "student"));
});
builder.Services.AddControllers(options => {
    options.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters();
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

builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));



builder.Services.AddControllersWithViews();

var app = builder.Build();
//for view image
app.UseStaticFiles();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();
 app.UseSaml2();
app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("MyPolic");
app.UseAuthentication();
app.UseAuthorization();

// app.UseEndpoints(endpoints =>
// {
//     endpoints.MapRazorPages();

//     endpoints.MapControllerRoute(
//         name: "default",
//         pattern: "{controller=Home}/{action=Index}/{id?}");
// });


// app.Run(async (context) => {
//     await context.Response.WriteAsync("Hallo check di https://localhost:7023");
// });


app.MapControllers();

app.Run();
