using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PoshBay.Contracts;
using PoshBay.Data.Data;
using PoshBay.Data.Models;
using PoshBay.Repositories;
using PoshBay.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Define a variable to get check type of environment
var env = builder.Environment;


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (env.IsDevelopment())
{
    //Add the DbContext to the builder
    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
}

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

//Add services for IdentityContext
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>

                    { options.SignIn.RequireConfirmedAccount = false;
                        options.Password.RequireDigit = false;
                        options.Password.RequiredLength = 8;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireUppercase = false;
                    })
                .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAppUserRoles, AppUserRole>();

//Add the services to the builder for Cloudinary by mapping the CloudinaryConfig class to the appsettings.json file
builder.Services.Configure<CloudinaryConfig>(builder.Configuration.GetSection("Cloudinary"));
builder.Services.AddScoped<IImageService, ImageService>();


//Add email service to the builder for Mapping the EmailConfig class to the appsettings.json file MailConfiguration section
builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("MailConfiguration"));




// Add services to the container.
builder.Services.AddControllersWithViews();

//Add service for Razorpages
builder.Services.AddRazorPages();


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

//My custom method to generate default admin
SeedData();

app.UseAuthentication();
app.UseAuthorization(); 
app.MapRazorPages();

//My custom methods to ensure database creation in Production Environment
DatabaseEnsureCreated();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


void SeedData()
{
    using (var scope = app.Services.CreateAsyncScope())
    {
        var initializeRole = scope.ServiceProvider.GetRequiredService<IAppUserRoles>();
        initializeRole.Roles().Wait();  //Wait() method is added because Roles() is an async void method
    }
}

void DatabaseEnsureCreated()
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.EnsureCreated();
    }
}