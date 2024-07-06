using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using fyp.Data;
using fyp.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using Stripe;
using fyp.Services;
using EmailService = fyp.Services.EmailSender;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders ();
builder.Services.AddRazorPages();


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

builder.Services.AddHttpClient<GoldPriceService>();

builder.Services.AddHttpClient("GoldAPI", client =>
{
    client.BaseAddress = new Uri("https://www.goldapi.io/api/");
    client.DefaultRequestHeaders.Add("x-access-token", "goldapi-3phepsly1epmz2-io");
}
    );
builder.Services.AddScoped<GoldPriceService>();
builder.Services.AddHttpClient();

builder.Services.AddSingleton<EmailService>();
builder.Services.AddSingleton<IEmailSender, fyp.Utility.EmailSender>();

builder.Services.AddTransient<IEmailSender, fyp.Utility.EmailSender>();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


/*
builder.Services.AddAuthentication().AddFacebook(opt =>
{
    opt.ClientId = "1160668128291475";
    opt.ClientSecret = "499f106ba3b1c893cc9673480316fac8";
});
*/
app.UseHttpsRedirection();
app.UseStaticFiles();
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
//StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
