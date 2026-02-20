using Microsoft.EntityFrameworkCore;
using Biblioteca_Unapec.Models;
using Microsoft.AspNetCore.Identity;

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
