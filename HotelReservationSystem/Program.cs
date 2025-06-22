using HotelReservationSystem.Data;
using HotelReservationSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 💾 Verilənlər bazası bağlantısı
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 🛡 Identity konfiqurasiyası — ApplicationUser istifadə edilir
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>() // Rol sistemi
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ⚠ Dev/prod fərqləndirilməsi
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

// 🔄 Default yönləndirmə: Rooms/Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Rooms}/{action=Index}/{id?}");

app.MapRazorPages();

// ✅ Admin və Rolların yaradılması (bir dəfəlik)
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    string[] roles = { "Admin", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    var adminEmail = "admin@example.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail
        };
        await userManager.CreateAsync(adminUser, "Admin123!");
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
}

app.Run();
