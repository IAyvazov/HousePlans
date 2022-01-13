using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using HousePlans.Data;
using HousePlans.Data.Seeding;
using HousePlans.Services.House;
using HousePlans.Services.Instalation;
using HousePlans.Services.Plan;
using HousePlans.Areas.Administration.Services.Floor;
using HousePlans.Areas.Administration.Services.House;
using HousePlans.Areas.Administration.Services.Instalation;
using HousePlans.Areas.Administration.Services.Material;
using HousePlans.Areas.Administration.Services.Photo;
using HousePlans.Areas.Administration.Services.Plan;
using HousePlans.Areas.Administration.Services.Room;
using HousePlans.Services.Material;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IPlanAdministrationService, PlanAdministrationService>();
builder.Services.AddTransient<IRoomAdministrationService, RoomAdministrationService>();
builder.Services.AddTransient<IFloorAdministrationService, FloorAdministrationService>();
builder.Services.AddTransient<IHouseAdministrationService, HouseAdministrationService>();
builder.Services.AddTransient<IInstalationAdministrationService, InstalationAdministrationService>();
builder.Services.AddTransient<IPhotoAdministrationService, PhotoAdministrationService>();
builder.Services.AddTransient<IMateialAdministrationService, MaterialAdministrationService>();

builder.Services.AddTransient<IPlanService, PlanService>();
builder.Services.AddTransient<IHouseService, HouseService>();
builder.Services.AddTransient<IInstalationService, InstalationService>();
builder.Services.AddTransient<IMaterialService, MaterialService>();

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
    new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
}

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "areaRoute",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapRazorPages();
});

app.Run();
