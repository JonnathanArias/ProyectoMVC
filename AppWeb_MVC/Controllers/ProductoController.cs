using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.Modelos;

namespace AppWeb_MVC.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ApplicationDbContext _context;

        //inyectamos el DbContext que viene desde program.cs 
        public UsuarioController(ApplicationDbContext context) 
        {
            _context = context;
        }

        //vamos a traer los datos de la BD de usuarios de forma asincrona y tambien que envie los datos a la vista Index 
        //enlista todos los usuarios!!
        public async Task<IActionResult> Index() 
        {
            var usuarios = _context.Usuarios.ToListAsync();
            return View(usuarios);//pasa la lista de usuarios a la vista
        }

        // GET: /Usuario/Crear (Muestra el formulario vacío)
        public IActionResult Crear()
        {
           return View();
      }

        // GET: /Usuario/ObtenerTodos (Para DataTables)
        // POST: /Usuario/Crear (Recibe los datos del formulario)
        #region
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.FechaCreacion = DateTime.Now; // Esto asigna la fecha de creación

                _context.Usuarios.Add(usuario);  // Agrega el nuevo usuario a la base de datos
                await _context.SaveChangesAsync();  // Guarda los cambios

                return RedirectToAction(nameof(Index));  // Redirige a la vista Index después de guardar
            }

            return View(usuario);  // Si el modelo no es válido, retorna a la vista con los datos del usuario y los errores
        }


        #endregion
    }
}
