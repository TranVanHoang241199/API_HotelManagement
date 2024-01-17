using API_HotelManagement.Business.Services.Test;
using API_HotelManagement.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo // là một đối tượng mô tả thông tin về API như Version, Title, Description, TermsOfService, Contact, License.
    {
        Version = "v1",
        Title = "API_HotelManagement",
        Description = "(Mô tả dự án) Đây là dự án ..... ",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Tran Van Hoang",
            Email = "vanhoangtran241199@gmail.com",
            Url = new Uri("https://example.com"),
        },
        License = new OpenApiLicense
        {
            Name = "License",
            Url = new Uri("https://example.com/license")
        }
    });

    // tạo note ghi chú cho api
    var filename = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
    var filepath = Path.Combine(AppContext.BaseDirectory, filename);
    options.IncludeXmlComments(filepath);
});

// Add Serviecs to businice
// Admin
builder.Services.AddScoped<ITestHandler, TestHandler>();
builder.Services.AddScoped<HtDbContext>();


//// Connection DB Sql Server
//builder.Services.AddDbContext<HtDbContext>(options =>
//    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

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
