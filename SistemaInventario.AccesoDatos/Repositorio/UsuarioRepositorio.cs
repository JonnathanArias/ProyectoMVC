using Microsoft.EntityFrameworkCore;
using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Interfaces;
using SistemaInventario.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SistemaInventario.AccesoDatos.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> ObtenerUsuarioPorIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task CrearUsuarioAsync(Usuario usuario)
        {
            usuario.ContraseñaHash = BCrypt.Net.BCrypt.HashPassword(usuario.ContraseñaHash); // Encriptar la contraseña
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarUsuarioAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarUsuarioAsync(int id)
        {
            var usuario = await ObtenerUsuarioPorIdAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Usuario>> ObtenerTodosUsuariosAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }
    }
}

