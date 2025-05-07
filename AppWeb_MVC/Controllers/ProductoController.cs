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
            var usuarios = await _context.Usuarios.ToListAsync();
            return View(usuarios);  // Pasa la lista de usuarios a la vista
        }

        // GET: /Usuario/Crear (Muestra el formulario vacío)
        public IActionResult Crear()
        {
           return View();
        }
        #region
        // GET: /Usuario/ObtenerTodos (Para DataTables)
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return Json(new { data = usuarios });
        }

        #endregion

        // POST: /Usuario/Crear (Recibe los datos del formulario)
         #region
          
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                // Verificar si ya existe un usuario con el mismo número de documento
                bool existeDocumento = await _context.Usuarios
                    .AnyAsync(u => u.NumeroDocumento == usuario.NumeroDocumento);

                if (existeDocumento)
                {
                    // Agregar error al modelo
                    ModelState.AddModelError("NumeroDocumento", "Ya existe un usuario con este número de documento.");
                    return View(usuario); // Devuelve a la vista con el error mostrado
                }

                usuario.FechaCreacion = DateTime.Now;
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(usuario);
        }


        [HttpDelete]
        public async Task<IActionResult> Eliminar(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return Json(new { success = false, message = "Error al eliminar" });
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Usuario eliminado exitosamente" });
        }


        #endregion

    }
}
