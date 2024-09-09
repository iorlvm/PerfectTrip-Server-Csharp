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

// 資料庫連線設定
builder.Services.AddDbContext<PerfectTripDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("PerfectTrip.Data")
     ));

// redis連線設定
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

// 配置 JWT 認證服務
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
            // 執行預設的 Token 驗證邏輯
            var claimsPrincipal = context.Principal;
            if (claimsPrincipal == null || !claimsPrincipal.Identity.IsAuthenticated)
            {
                // 預設的驗證沒通過, 表示token被偽造了
                context.Fail("Invalid token.");
                return;
            }

            // 讀取 iat (發行時間)
            var issuedAtClaim = context.Principal.Claims.First(c => c.Type == JwtRegisteredClaimNames.Iat).Value;
            var issuedAtUnixTime = long.Parse(issuedAtClaim);

            // 讀取 exp (過期時間)
            var expirationClaim = context.Principal.Claims.First(c => c.Type == JwtRegisteredClaimNames.Exp).Value;
            var expirationUnixTime = long.Parse(expirationClaim);

            // 獲取當前時間
            var currentUnixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            // 檢查 token 是否過期
            if (currentUnixTime >= expirationUnixTime)
            {
                context.Fail("Token has expired.");
                return;
            }


            // 獲取使用者的 ID
            var userId = context.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // 使用自訂的JWT驗證token的版本號 (發行時間), 其實還可以考慮導入機器碼進行附加驗證
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

// 配置授權
builder.Services.AddAuthorization();

// 註冊服務
builder.Services.AddScoped<IRedisService, RedisService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IAdminService, AdminService>();

// 註冊存儲庫
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();

// 自動映射依賴
builder.Services.AddAutoMapper(typeof(MappingProfile));

// 添加所有的控制器
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
});
// 將序列化工具配置為一個服務
builder.Services.AddSingleton(provider =>
{
    var mvcJsonOptions = provider.GetRequiredService<IOptions<MvcNewtonsoftJsonOptions>>();
    return mvcJsonOptions.Value.SerializerSettings;
});
// 註冊 JsonHelper
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

// 跨域配置
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllHeaders",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader(); // 允許 Authorization
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 使用認證中間件
app.UseRouting();
app.UseCors("AllowAllHeaders");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
