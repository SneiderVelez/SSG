using Microsoft.EntityFrameworkCore;
using SggApp.DAL.Entidades;

namespace SggApp.DAL.Repositorios
{
    public class PresupuestoRepository : GenericRepository<Presupuestos>
    {
        private readonly ApplicationDbContext _context;

        public PresupuestoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // Método específico: Obtener presupuestos por usuario
        public async Task<IEnumerable<Presupuestos>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _context.Presupuestos
                .Where(p => p.UsuarioId == usuarioId)
                .Include(p => p.Categoria)
                .Include(p => p.Moneda)
                .ToListAsync();
        }

        // Método específico: Obtener presupuestos por categoría
        public async Task<IEnumerable<Presupuestos>> GetByCategoriaIdAsync(int categoriaId)
        {
            return await _context.Presupuestos
                .Where(p => p.CategoriaId == categoriaId)
                .Include(p => p.Usuario)
                .Include(p => p.Moneda)
                .ToListAsync();
        }

        // Método específico: Obtener presupuestos activos (fecha actual entre inicio y fin)
        public async Task<IEnumerable<Presupuestos>> GetActivosAsync()
        {
            var fechaActual = DateTime.Now;
            return await _context.Presupuestos
                .Where(p => p.FechaInicio <= fechaActual && p.FechaFin >= fechaActual)
                .Include(p => p.Usuario)
                .Include(p => p.Categoria)
                .Include(p => p.Moneda)
                .ToListAsync();
        }

        // Método específico: Obtener presupuestos con todas sus relaciones
        public async Task<IEnumerable<Presupuestos>> GetAllWithDetailsAsync()
        {
            return await _context.Presupuestos
                .Include(p => p.Usuario)
                .Include(p => p.Categoria)
                .Include(p => p.Moneda)
                .ToListAsync();
        }
    }
}