using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using HousePlans.Data;
using HousePlans.Data.Seeding;

using HousePlans.Services.House;
using HousePlans.Services.Instalation;
using HousePlans.Services.Plan;
using HousePlans.Services.Material;

using HousePlans.Areas.Administration.Services.Floor;
using HousePlans.Areas.Administration.Services.House;
using HousePlans.Areas.Administration.Services.Instalation;
using HousePlans.Areas.Administration.Services.Material;
using HousePlans.Areas.Administration.Services.Photo;
using HousePlans.Areas.Administration.Services.Plan;
using HousePlans.Areas.Administration.Services.Room;
using HousePlans.Data.Email;
using HousePlans.Areas.Administration.Services.Email;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var emailConfig = builder.Configuration
    .GetSection("EmailConfiguration")
    .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);



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
builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddTransient<IPlanService, PlanService>();
builder.Services.AddTransient<IHouseService, HouseService>();
builder.Services.AddTransient<IInstalationService, InstalationService>();
builder.Services.AddTransient<IMaterialService, MaterialService>();

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var adminConfig = builder.Configuration
    .GetSection("Administrator")
    .Get<AdminConfiguration>();

    var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
    new ApplicationDbContextSeeder(adminConfig).SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
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
