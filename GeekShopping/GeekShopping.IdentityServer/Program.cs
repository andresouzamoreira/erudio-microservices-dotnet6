using GeekShopping.IdentityServer.Configuration;
using GeekShopping.IdentityServer.Initializer;
using GeekShopping.IdentityServer.Model;
using GeekShopping.IdentityServer.Model.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
// Add services to the container.
builder.Services.AddControllersWithViews();

var connection = builder.Configuration["ConnectionString:SqlServer"];
builder.Services.AddDbContext<SqlServerContext>(options => options.UseSqlServer(connection));

	builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
		.AddEntityFrameworkStores<SqlServerContext>()
		.AddDefaultTokenProviders();



var build = builder.Services.AddIdentityServer(options =>
	{
		options.Events.RaiseErrorEvents = true;
		options.Events.RaiseInformationEvents = true;
		options.Events.RaiseFailureEvents = true;
		options.Events.RaiseSuccessEvents = true;
		options.EmitStaticAudienceClaim = true;

	}).AddInMemoryIdentityResources(
			IdentityConfiguration.IdentityResource)
				.AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
				.AddInMemoryClients(IdentityConfiguration.Clients)
				.AddAspNetIdentity<ApplicationUser>();



build.AddDeveloperSigningCredential();


builder.Services.AddControllersWithViews();


var app = builder.Build();


var initializer = app.Services.CreateScope().ServiceProvider.GetService<IDbInitializer>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();

app.UseAuthorization();

initializer.Initialize();	

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
