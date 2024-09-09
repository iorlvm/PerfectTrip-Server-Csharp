using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PerfectTrip.Application.Mappings;
using PerfectTrip.Application.Services.Jwt.Implement;
using PerfectTrip.Application.Services.Jwt.Interface;
using PerfectTrip.Application.Services.Member.Implement;
using PerfectTrip.Application.Services.Member.Interface;
using PerfectTrip.Application.Services.Redis.Implement;
using PerfectTrip.Application.Services.Redis.Interface;
using PerfectTrip.Common.Utils;
using PerfectTrip.Data;
using PerfectTrip.Data.Repositories.Member.Implement;
using PerfectTrip.Data.Repositories.Member.Interface;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ��Ʈw�s�u�]�w
builder.Services.AddDbContext<PerfectTripDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("PerfectTrip.Data")
     ));

// redis�s�u�]�w
var redisConfig = builder.Configuration.GetSection("Redis");
builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect(
        new ConfigurationOptions()
        {
            EndPoints = { { redisConfig["Hostname"], int.Parse(redisConfig["Port"]) } },
            Password = redisConfig["Password"],
            DefaultDatabase = int.Parse(redisConfig["Database"])
        }
    )
);

// �t�m JWT �{�ҪA��
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = async context =>
        {
            // ����w�]�� Token �����޿�
            var claimsPrincipal = context.Principal;
            if (claimsPrincipal == null || !claimsPrincipal.Identity.IsAuthenticated)
            {
                // �w�]�����ҨS�q�L, ���token�Q���y�F
                context.Fail("Invalid token.");
                return;
            }

            // Ū�� iat (�o��ɶ�)
            var issuedAtClaim = context.Principal.Claims.First(c => c.Type == JwtRegisteredClaimNames.Iat).Value;
            var issuedAtUnixTime = long.Parse(issuedAtClaim);

            // Ū�� exp (�L���ɶ�)
            var expirationClaim = context.Principal.Claims.First(c => c.Type == JwtRegisteredClaimNames.Exp).Value;
            var expirationUnixTime = long.Parse(expirationClaim);

            // �����e�ɶ�
            var currentUnixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            // �ˬd token �O�_�L��
            if (currentUnixTime >= expirationUnixTime)
            {
                context.Fail("Token has expired.");
                return;
            }


            // ����ϥΪ̪� ID
            var userId = context.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // �ϥΦۭq��JWT����token�������� (�o��ɶ�), ����٥i�H�Ҽ{�ɤJ�����X�i����[����
            var jwtService = context.HttpContext.RequestServices.GetRequiredService<IJwtService>();
            if (!await jwtService.IsTokenValidAsync(userId, issuedAtUnixTime))
            {
                context.Fail("Invalid token.");
            }
        }
    };

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"])),
    };
});

// �t�m���v
builder.Services.AddAuthorization();

// ���U�A��
builder.Services.AddScoped<IRedisService, RedisService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IAdminService, AdminService>();

// ���U�s�x�w
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();

// �۰ʬM�g�̿�
builder.Services.AddAutoMapper(typeof(MappingProfile));

// �K�[�Ҧ������
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
});
// �N�ǦC�Ƥu��t�m���@�ӪA��
builder.Services.AddSingleton(provider =>
{
    var mvcJsonOptions = provider.GetRequiredService<IOptions<MvcNewtonsoftJsonOptions>>();
    return mvcJsonOptions.Value.SerializerSettings;
});
// ���U JsonHelper
builder.Services.AddSingleton<JsonHelper>(provider =>
{
    var settings = provider.GetRequiredService<JsonSerializerSettings>();
    return new JsonHelper(settings);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT token into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
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

// ���t�m
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllHeaders",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader(); // ���\ Authorization
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// �ϥλ{�Ҥ�����
app.UseRouting();
app.UseCors("AllowAllHeaders");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
