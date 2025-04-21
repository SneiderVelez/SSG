using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SggApp.DAL.Repositorios
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        // Obtener todos los registros
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // Obtener un registro por ID
        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        // Obtener registros con filtros (opcional)
        public virtual async Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        // Agregar un nuevo registro
        public virtual async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        // Actualizar un registro existente
        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        // Eliminar un registro
        public virtual void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        // Verificar si existe un registro según una condición
        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        // Contar registros según una condición (opcional)
        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null
                ? await _dbSet.CountAsync()
                : await _dbSet.CountAsync(predicate);
        }
    }
}