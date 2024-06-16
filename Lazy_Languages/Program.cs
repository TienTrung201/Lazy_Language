using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NTTRUNG_Lazy_Languages.Middleware;
using NTTRUNG_Lazy_Languages_Application.Dtos.Entity;
using NTTRUNG_Lazy_Languages_Application.Interface.Service;
using NTTRUNG_Lazy_Languages_Application.Service;
using NTTRUNG_Lazy_Languages_Domain.Interface.Repository;
using NTTRUNG_Lazy_Languages_Domain;
using NTTRUNG_Lazy_Languages_Domain.Interface.UnitOfWork;
using NTTRUNG_Lazy_Languages_Infastructurce.Repository;
using NTTRUNG_Lazy_Languages_Infastructurce.Repository.UnitOfWork;
using NTTRUNG_Lazy_Languages_Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container.
// Exception validate input
builder.Services.AddControllers().ConfigureApiBehaviorOptions(option =>
{
    option.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState.Values.Reverse().SelectMany(x => x.Errors);
        string errorMessage = string.Join(", ", errors.Select(x => x.ErrorMessage));
        return new BadRequestObjectResult(new BaseException()
        {
            ErrorCode = 400,
            UserMessage = errorMessage,
            DevMessage = errorMessage,
            TraceId = context.HttpContext.TraceIdentifier,
            MoreInfo = "",
            Errors = errors
        });

    };
}).AddJsonOptions(option =>
{
    option.JsonSerializerOptions.PropertyNamingPolicy = null;
    //option.JsonSerializerOptions.Converters.Add(new LocalTimeZoneConverter());
});
// Get ConnectionString
var connectionstring = builder.Configuration.GetConnectionString("BaseAPI");

// Add DI
builder.Services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(connectionstring));
builder.Services.AddScoped<IAuthService, AuthService>();
// Add auto mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
//
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
// Add Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin();
        policyBuilder.AllowAnyMethod();
        policyBuilder.AllowAnyHeader();
    });
});
//localization 
var localizationOptions = new RequestLocalizationOptions();
localizationOptions.SetDefaultCulture("en-US");


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
        };
    });
builder.Services.AddAuthorization();
var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseMiddleware<LocalizationMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<JwtMiddleware>();
// Sử dụng middleware xác thực
app.UseAuthentication();

// Sử dụng middleware ủy quyền
app.UseAuthorization();

app.MapControllers();               
app.Run();
