using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using ShopBridge.Data;
using ShopBridge.Domain.Services;
using ShopBridge.Domain.Services.Interfaces;
using ShopBridge.Data.Repository;
using ShopBridge.Application;
using Microsoft.OpenApi.Models;
using ShopBridge.Application.Services.Interfaces;
using ShopBridge.Application.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();

builder.Services
    .AddDbContext<Shopbridge_Context>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("Shopbridge_Context"));
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shopbridge_base", Version = "v1" });
});

builder.Services.AddLogging(options =>
{
    options.AddDebug();
    options.AddConsole();
});

builder.Services.AddTransient<IRepository, Repository>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ITagService, TagService>();
builder.Services.AddTransient<IImageUploadService, ImageUploadService>();

builder.Services.AddAutoMapper(option => option.AddProfile<AutoMapping>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler("/api/Error");

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
