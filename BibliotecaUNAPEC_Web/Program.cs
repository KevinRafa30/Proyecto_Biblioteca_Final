using BibliotecaUNAPEC_Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BibliotecaUNAPEC_Web.Data;

var builder = WebApplication.CreateBuilder(args);


//Configuracion de la base de datos y el contexto de identidad
builder.Services.AddDbContext<BibliotecaUnapecContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
?? "Server=(localdb)\\MSSQLLocalDB;Database=BibliotecaUNAPEC;Trusted_Connection=True;"));

//Configuración de Identity


builder.Services.AddDefaultIdentity<IdentityUser>(options => {
    options.SignIn.RequireConfirmedAccount = false; 
    options.Password.RequireDigit = false;          
    options.Password.RequiredLength = 6;
})

.AddRoles<IdentityRole>() // Agrega soporte para roles
.AddEntityFrameworkStores<BibliotecaUnapecContext>();


// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthentication();
app.UseAuthorization();




app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//Mapeo de paginas de razor para usar identity (login, registro)
app.MapRazorPages();



//Manejo de roles con seeding
// --- BLOQUE DE MANEJO DE ROLES ---
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    //Definicion de roles
    string[] roleNames = { "Admin", "Empleado", "Lector" };

    foreach (var roleName in roleNames)
    {
        // Si el rol no existe en la tabla AspNetRoles
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    // Administrador por defecto 
    var adminEmail = "admin@biblioteca.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        var user = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
        await userManager.CreateAsync(user, "Admin123!"); // Clave segura para el administrador
        await userManager.AddToRoleAsync(user, "Admin"); // Usuario administrador agregado al rol Admin
    }
}


app.Run();

