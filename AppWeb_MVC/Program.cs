using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SistemaInventario.AccesoDatos.Repositorios; // Correcto, asegúrate de que este espacio de nombres esté bien.
using SistemaInventario.AccesoDatos.Interfaces;


public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Obtener la cadena de conexión desde appsettings.json
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        // Registrar el ApplicationDbContext en el contenedor de dependencias
        builder.Services.AddDbContext<SistemaInventario.AccesoDatos.Data.ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Registrar los repositorios en el contenedor de dependencias
        builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

        // Agregar la página de excepción para la base de datos en modo desarrollo
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        // Agregar servicios MVC con Razor Runtime Compilation (para desarrollo)
        builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

        // Construir la aplicación
        var app = builder.Build();

        // Configuración del pipeline de la aplicación
        app.UseHttpsRedirection(); // Redirige todas las peticiones HTTP a HTTPS
        app.UseStaticFiles(); // Permite servir archivos estáticos (CSS, JS, imágenes, etc.)
        app.UseRouting(); // Habilita el enrutamiento
        app.UseAuthorization(); // Habilita la autorización (control de acceso)

        // Definir las rutas del controlador
        app.MapControllerRoute(
            name: "default", // Ruta por defecto
            pattern: "{controller=Home}/{action=Index}/{id?}"); // Parámetros de ruta

        app.Run(); // Ejecuta la aplicación
    }
}





