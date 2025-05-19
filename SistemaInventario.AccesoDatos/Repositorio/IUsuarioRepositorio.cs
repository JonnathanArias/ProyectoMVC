using SistemaInventario.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario> ObtenerUsuarioPorIdAsync(int id);
        Task CrearUsuarioAsync(Usuario usuario);
        Task ActualizarUsuarioAsync(Usuario usuario);
        Task EliminarUsuarioAsync(int id);
        Task<IEnumerable<Usuario>> ObtenerTodosUsuariosAsync();
    }
}

