using SistemaVentaAngular.Models;
using System.Linq.Expressions;

namespace SistemaVentaAngular.Repository.Contratos
{
    public interface ICategoriaRepositorio
    {
        Task<List<Categoria>> Lista();

        Task<Categoria> Obtener(Expression<Func<Categoria, bool>> filtro = null);
        Task<Categoria> Crear(Categoria entidad);
        Task<bool> Editar(Categoria entidad);
        Task<bool> Eliminar(Categoria entidad);
    }
}
