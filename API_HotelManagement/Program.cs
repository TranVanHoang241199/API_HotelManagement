﻿using API_HotelManagement.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using System.Security.Claims;
using Serilog;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Controllers;
using API_HotelManagement.Business;

var builder = WebApplication.CreateBuilder(args);

Environment.SetEnvironmentVariable("ASPNETCORE_APIURL", builder.Configuration.GetSection("URLs").GetSection("APIUrl").Value);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Swagger
/***
 * Xử lý Swagger hiển thị cho view
 */

//builder.Services.AddApiVersioning(options =>
//{
//    options.DefaultApiVersion = new ApiVersion(1, 0);
//    options.AssumeDefaultVersionWhenUnspecified = true;
//    options.ReportApiVersions = true;
//});

//builder.Services.AddApiVersioning(options =>
//{
//    // Specify the default API version
//    options.DefaultApiVersion = new ApiVersion(1, 0);

//    // Specify supported API versions
//    options.AssumeDefaultVersionWhenUnspecified = true;
//    options.ReportApiVersions = true;

//    // Specify versioning scheme (e.g., using query string parameter "api-version")
//    options.ApiVersionReader = new QueryStringApiVersionReader("api-version");
//});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API_HotelManagement",
        Description = "Kho lưu trữ này chứa mã nguồn của hệ thống quản lý khách sạn được xây dựng bằng API ASP.NET Core 7. Dự án được thiết kế để hỗ trợ các ứng dụng di động và web với phiên bản mới nhất của .NET Core framework.",
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

    //options.SwaggerDoc("Auths", new OpenApiInfo
    //{
    //    Version = "v1",
    //    Title = "API_HotelManagement - Auths",

    //    // ...
    //});

    //options.SwaggerDoc("Users", new OpenApiInfo
    //{
    //    Version = "v1",
    //    Title = "API_HotelManagement - User",
    //    // ...
    //});

    // Group name
    // https://rimdev.io/swagger-grouping-with-controller-name-fallback-using-swashbuckle-aspnetcore/
    options.TagActionsBy(api =>
    {
        if (api.GroupName != null)
        {
            return new[] { api.GroupName };
        }

        var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
        if (controllerActionDescriptor != null)
        {
            return new[] { controllerActionDescriptor.ControllerName };
        }

        throw new InvalidOperationException("Unable to determine tag for endpoint.");
    });

    options.DocInclusionPredicate((name, api) => true);

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

// Đăng ký ràng buộc cho tham số đường dẫn 'api-version'

builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap.Add("apiVersion", typeof(ApiVersionRouteConstraint));
});

#endregion Swagger

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
builder.Services.AddScoped<ICategoryRoomHandler, CategoryRoomHandler>();
builder.Services.AddScoped<ICategoryServiceHandler, CategoryServiceHandler>();
builder.Services.AddScoped<IRoomHandler, RoomHandler>();
builder.Services.AddScoped<IServiceHanlder, ServiceHanlder>();
builder.Services.AddScoped<ICustomerHandler, CustomerHandler>();
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
            ValidIssuer = builder.Configuration["Authentication:Jwt:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:Jwt:Key"]))
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

#region Swagger View
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API_HotelManagement_v1");
    });
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API_HotelManagement_v1");
    options.RoutePrefix = string.Empty; // chỉnh đường linh swagger 
});
#endregion Swagger View

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
