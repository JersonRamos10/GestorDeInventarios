using InventarioProductos.Data;
using InventarioProductos.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Globalization;

namespace InventarioProductos
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuración de la cadena de conexion
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();



            // Configuración de la identidad (Identity) para la autenticacion y autorizacion
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
                options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddRazorPages();

            builder.Services.AddSingleton<IEmailSender, NullEmailSender>();

            var cultureInfo = new CultureInfo("en-US"); // Cambia a "es-ES" para español
            cultureInfo.NumberFormat.CurrencySymbol = "$"; // Cambia el símbolo de moneda si es necesario
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            // Habilitar controladores y vistas
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Llamada del metodo creacion de roles
           await CrearRoles(app);  // Usar .Wait() para esperar a que los roles se creen antes de que arranque la app

            // Configuración del pipeline de la aplicacion
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

            // Agregar autenticacion y autorizacion al pipeline
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
        //Metodo para crear roles
        private static async Task CrearRoles(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var servicioRoles = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var servicioUsuario = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                // Verificacion de existencia de roles y creacion de roles
                if (await servicioRoles.RoleExistsAsync("Admin") == false)
                {
                    await servicioRoles.CreateAsync(new IdentityRole("Admin"));
                 
                }

                if (await servicioRoles.RoleExistsAsync("Usuario") == false)
                {
                    await servicioRoles.CreateAsync(new IdentityRole("Usuario"));
                   
                }

                
            }
        }


    }
}
