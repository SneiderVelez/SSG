using Microsoft.EntityFrameworkCore;
using SggApp.DAL.Entidades;

namespace SggApp.DAL.Repositorios
{
    public class CategoriaRepository : GenericRepository<Categorias>
    {
        private readonly ApplicationDbContext _context;

        public CategoriaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // Método específico: Obtener categorías con sus gastos asociados
        public async Task<IEnumerable<Categorias>> GetWithGastosAsync()
        {
            return await _context.Categorias
                .Include(c => c.Gastos)
                .ToListAsync();
        }

        // Método específico: Obtener categorías con sus presupuestos asociados
        public async Task<IEnumerable<Categorias>> GetWithPresupuestosAsync()
        {
            return await _context.Categorias
                .Include(c => c.Presupuestos)
                .ToListAsync();
        }

        // Método específico: Verificar si existe una categoría con el mismo nombre
        public async Task<bool> ExistsByNombreAsync(string nombre)
        {
            return await _context.Categorias.AnyAsync(c => c.Nombre == nombre);
        }
    }
}