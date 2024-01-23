using API_HotelManagement.Data.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using System.Security.Claims;
using Serilog;
using API_HotelManagement.Business.Services.Orders;
using API_HotelManagement.Business.Services.Rooms;
using API_HotelManagement.Business.Services.Services;
using API_HotelManagement.Business.Services.Auths;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

Environment.SetEnvironmentVariable("ASPNETCORE_APIURL", builder.Configuration.GetSection("URLs").GetSection("APIUrl").Value);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

/***
 * Xử lý Swagger hiển thị cho view
 */
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API_HotelManagement",
        Description = "Dự án thiết kế hotel ma....",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Tran Van Hoang",
            Email = "vanhoangtran241199@gmail.com",
            Url = new Uri("https://vanhoangtran241199@gmail.com"),
        },
        License = new OpenApiLicense
        {
            Name = "License",
            Url = new Uri("https://example.com/license")
        }
    });

    options.SwaggerDoc("Auths", new OpenApiInfo
    {
        Version = "v1",
        Title = "API_HotelManagement - Auths",
        
        // ...
    });

    options.SwaggerDoc("Users", new OpenApiInfo
    {
        Version = "v1",
        Title = "API_HotelManagement - User",
        // ...
    });

    // Tạo note ghi chú cho API từ file XML
    var filename = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
    var filepath = Path.Combine(AppContext.BaseDirectory, filename);
    options.IncludeXmlComments(filepath);

    // Thêm định nghĩa bảo mật cho Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer {token}'",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        //Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    // Thêm yêu cầu bảo mật cho Swagger
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

/***
 * Xử dụng cho log xem lỗi 
 * Log.Information($"User '{user.Id}' successfully authenticated.");
 * Log.Warning($"Authentication failed: User not found for email '{email}'.");
 * Log.Error(ex, string.Empty);
 */
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() // nếu không muốn hiển thị console comment lại dòng này
    .WriteTo.File(Path.Combine("Logs", "log.txt"), rollingInterval: RollingInterval.Day)
    .CreateLogger();


/***
 * Add Serviecs to businice
 * 
 */
builder.Services.AddScoped<IAuthHandler, AuthHandler>();
builder.Services.AddScoped<IOrderHandler, OrderHandler>();
builder.Services.AddScoped<IRoomHandler, RoomHandler>();
builder.Services.AddScoped<IServiceHanlder, ServiceHanlder>();
builder.Services.AddScoped<HtDbContext>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

/***
 * Authentcatin JWT token
 * 
 */
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            RoleClaimType = ClaimTypes.Role,
            //ClockSkew = TimeSpan.Zero,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

/***
 * Connection DB Sql Server
 */
//builder.Services.AddDbContext<HtDbContext>(options =>
//    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

/***
 * AutoMapper
 */
builder.Services.AddAutoMapper(typeof(UserAutoMapper));
builder.Services.AddAutoMapper(typeof(OrderAutoMapper));
builder.Services.AddAutoMapper(typeof(RoomAutoMapper));
builder.Services.AddAutoMapper(typeof(ServiceAutoMapper));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API_HotelManagement_v1");
    });
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
