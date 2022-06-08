using Microsoft.EntityFrameworkCore;
using PoshBay.Contracts;
using PoshBay.Data.Data;
using PoshBay.Data.Models;
using PoshBay.Repositories;
using PoshBay.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Add the DbContext to the builder
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();

//Add the services to the builder for Cloudinary by mapping the CloudinaryConfig class to the appsettings.json file
builder.Services.Configure<CloudinaryConfig>(builder.Configuration.GetSection("Cloudinary"));
builder.Services.AddScoped<IImageService, ImageService>();


//Add email service to the builder for Mapping the EmailConfig class to the appsettings.json file MailConfiguration section
builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("MailConfiguration"));



// Add services to the container.
builder.Services.AddControllersWithViews();

//Add service for Repositories


//add automapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
