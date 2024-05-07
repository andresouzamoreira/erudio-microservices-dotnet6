using GeekShopping.Web.Services;
using GeekShopping.Web.Services.IServices;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IServiceProduct, ProductService>(

    c => c.BaseAddress = new Uri(builder.Configuration[("ServicesUrls:ProductAPI")])
);  

//builder.Services.AddScoped<IServiceProduct>();
// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddMvc();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
