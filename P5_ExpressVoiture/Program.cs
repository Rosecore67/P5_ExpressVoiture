using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using P5_ExpressVoiture.Data;
using P5_ExpressVoiture.Models;
using P5_ExpressVoiture.Models.Entities;
using P5_ExpressVoiture.Models.Interfaces.IRepositories;
using P5_ExpressVoiture.Models.Interfaces.IServices;
using P5_ExpressVoiture.Models.Repositories;
using P5_ExpressVoiture.Models.Services;
using P5_ExpressVoiture.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Injection des dépendances.
builder.Services.AddScoped<IVoitureRepository, VoitureRepository>();
builder.Services.AddScoped<IVoitureService, VoitureService>();

builder.Services.AddScoped<IMarqueRepository, MarqueRepository>();
builder.Services.AddScoped<IMarqueService, MarqueService>();

builder.Services.AddScoped<IModeleRepository, ModeleRepository>();
builder.Services.AddScoped<IModeleService, ModeleService>();

builder.Services.AddScoped<IReparationRepository, ReparationRepository>();
builder.Services.AddScoped<IReparationService, ReparationService>();

builder.Services.AddScoped<ITypeReparationRepository, TypeReparationRepository>();
builder.Services.AddScoped<ITypeReparationService, TypeReparationService>();

builder.Services.AddScoped<IFinanceRepository, FinanceRepository>();
builder.Services.AddScoped<IFinanceService, FinanceService>();

builder.Services.AddScoped<IUtilisateurRepository, UtilisateurRepository>();
builder.Services.AddScoped<IUtilisateurService, UtilisateurService>();

builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();

builder.Services.AddScoped<CalculePrix>();

builder.Services.AddIdentity<Utilisateur, UserRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders()
    .AddUserManager<UserManager<Utilisateur>>()
    .AddRoleManager<RoleManager<UserRole>>();

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
{

});

builder.Services.Configure<IdentityOptions>(options =>
{
    // Sign in settings.
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;

    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<UserRole>>();
    var userManager = services.GetRequiredService<UserManager<Utilisateur>>();
    await SeedRolesAndAdminAsync(roleManager, userManager);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Voiture}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

async Task SeedRolesAndAdminAsync(RoleManager<UserRole> roleManager, UserManager<Utilisateur> userManager)
{
    var roleNames = new[] { "Admin", "Visiteur" };
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            var role = new UserRole { Name = roleName, NomRole = roleName };
            await roleManager.CreateAsync(role);
        }
    }

    var adminEmail = "admin@example.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new Utilisateur { UserName = adminEmail, Email = adminEmail, NomUtilisateur = "Admin" };
        var result = await userManager.CreateAsync(adminUser, "Admin@1234");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}