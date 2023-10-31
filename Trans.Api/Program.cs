using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;
using Trans.Api.Middleware;
using Trans.Core.Domain;
using Trans.Core.Repositories;
using Trans.Core.Repository;
using Trans.Infrascture.EF_DB;
using Trans.Infrascture.Repositories;
using Trans.Infrastructure.AutoMapper;
using Trans.Infrastructure.Commands;
using Trans.Infrastructure.Commands.Users;
using Trans.Infrastructure.Repositories;
using Trans.Infrastructure.Services;
using Trans.Infrastructure.Settings;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationManager configuration = builder.Configuration;

//configuration.AddJsonFile("appsettings.dev.json"); //development json

//Serilog configuration
builder.Host.UseSerilog((context, services, configuration) => configuration
         .ReadFrom.Configuration(context.Configuration)
         .ReadFrom.Services(services)
         .Enrich.FromLogContext()
         .WriteTo.File("information.log", rollingInterval: RollingInterval.Day));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standar Authorization header using the Bearer scheme",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();

});

var jwtSettings = new JwtSettings();
var dataInitializer = new DataInitializerSettings();
var emailHost = new EmailHostSettings();

builder.Services.AddMvc().AddJsonOptions(x => x.JsonSerializerOptions.WriteIndented = true);

builder.Configuration.GetSection("JwtSettings").Bind(jwtSettings);
builder.Configuration.GetSection("DataInitializer").Bind(dataInitializer);
builder.Configuration.GetSection("EmailSender").Bind(emailHost);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICommandHandler<CreateUser>, CreateUserHandler>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICompanyDriverService, CompanyDriverService>();
builder.Services.AddScoped<ICompanyOrderService, CompanyOrderService>();
builder.Services.AddScoped<ICompanyVehicleService, CompanyVehicleService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPasswordEncrypter, PasswordEncrypter>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IDataInitializer, DataInitializer>();


builder.Services.AddScoped<ErrorHandlerMiddleware>();
builder.Services.AddSingleton(AutoMapperConfig.Initialize());
builder.Services.AddSingleton(jwtSettings);
builder.Services.AddSingleton(emailHost);
builder.Services.AddDbContext<TransContext>(options =>
{
    //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
    options.UseSqlServer(connectionString);
    options.UseSqlServer(b => b.MigrationsAssembly("Trans.Api"));
});

//Autofac ICommandDispatcher
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
    });

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseSwagger();
app.UseSwaggerUI(op =>
{
    op.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger V1");
    op.RoutePrefix = string.Empty;

});


app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

//DataInitializer (if database is empty)
/*if (dataInitializer.DataSeed)
{
    var dataInitialize = app.Services.CreateScope();
    dataInitialize.ServiceProvider.GetRequiredService<IDataInitializer>().SeedAsync();
}*/

app.Run();



public partial class Program { }