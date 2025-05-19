using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.Modelos;

namespace AppWeb_MVC.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UsuarioController> _logger;

        //inyectamos el DbContext que viene desde program.cs 
        public UsuarioController(ApplicationDbContext context, ILogger<UsuarioController> logger)
        {
            _context = context;
            _logger = logger;
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
            return View(new Usuario());
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

                // Verificar la creacion del usuario no sea una fecha futura
                DateTime fechaCreacion = usuario.FechaCreacion;

                if (fechaCreacion > DateTime.Now)
                {
                    throw new ArgumentException("La fecha de creación no puede ser en el futuro.");
                }

                usuario.FechaCreacion = DateTime.Now;
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(usuario);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);
                if (usuario == null)
                {
                    TempData["Error"] = "Usuario no encontrado";
                    return RedirectToAction(nameof(Index));
                }

                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();

                TempData["Success"] = "¡Usuario eliminado correctamente!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }



        }
        //validar el de editar 


        // GET: Usuario/Editar/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
                return NotFound();

            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        // POST: Usuario/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Usuario usuario)
        {
            if (id != usuario.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                // Debug: Verifica qué errores hay en ModelState
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    _logger.LogWarning($"Error de validación: {error.ErrorMessage}");
                }
                return View(usuario);
            }

            try
            {
                var usuarioExistente = await _context.Usuarios.FindAsync(id);
                if (usuarioExistente == null)
                    return NotFound();

                // Validación de documento único
                if (await _context.Usuarios
                    .AnyAsync(u => u.Id != usuario.Id && u.NumeroDocumento == usuario.NumeroDocumento))
                {
                    ModelState.AddModelError("NumeroDocumento", "Documento ya registrado");
                    return View(usuario);
                }

                // Actualiza solo las propiedades necesarias
                usuarioExistente.Nombre = usuario.Nombre;
                usuarioExistente.NumeroDocumento = usuario.NumeroDocumento;

                // ... otras propiedades

                await _context.SaveChangesAsync();

                TempData["Success"] = $"Usuario {usuario.Nombre} actualizado";
                return RedirectToAction(nameof(Index));

            }


            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar usuario");
                TempData["Error"] = "Error inesperado al actualizar usuario";
                return View(usuario);
            }
        }



        #endregion

    }
}