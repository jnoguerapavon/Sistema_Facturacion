using Microsoft.EntityFrameworkCore;
using SistemaVentaAngular.Models;
using SistemaVentaAngular.Repository.Contratos;
using System.Linq;
using System.Linq.Expressions;

namespace SistemaVentaAngular.Repository.Implementacion
{
    public class CategoriaRepositorio : ICategoriaRepositorio
    {
        private readonly DBVentaAngularContext _dbContext;

        public CategoriaRepositorio(DBVentaAngularContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Categoria> Crear(Categoria entidad)
        {
            try
            {
                _dbContext.Set<Categoria>().Add(entidad);
                await _dbContext.SaveChangesAsync();
                return entidad;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(Categoria entidad)
        {
            try
            {
                _dbContext.Update(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(Categoria entidad)
        {
            try
            {
                _dbContext.Remove(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Categoria>> Lista()
        {
            try
            {
                return await _dbContext.Categoria.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Categoria> Obtener(Expression<Func<Categoria, bool>> filtro = null)
        {
            try
            {
                return await _dbContext.Categoria.Where(filtro).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
