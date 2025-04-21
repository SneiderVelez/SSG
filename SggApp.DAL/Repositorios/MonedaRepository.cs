using Microsoft.EntityFrameworkCore;
using SggApp.DAL.Entidades;

namespace SggApp.DAL.Repositorios
{
    public class MonedaRepository : GenericRepository<Monedas>
    {
        private readonly ApplicationDbContext _context;

        public MonedaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // Método específico: Obtener moneda por código
        public async Task<Monedas> GetByCodigoAsync(string codigo)
        {
            return await _context.Monedas.FirstOrDefaultAsync(m => m.Codigo == codigo);
        }

        // Método específico: Verificar si existe una moneda con el mismo código
        public async Task<bool> ExistsByCodigoAsync(string codigo)
        {
            return await _context.Monedas.AnyAsync(m => m.Codigo == codigo);
        }

        // Método específico: Obtener monedas con sus gastos asociados
        public async Task<IEnumerable<Monedas>> GetWithGastosAsync()
        {
            return await _context.Monedas
                .Include(m => m.Gastos)
                .ToListAsync();
        }

        // Método específico: Obtener monedas con sus presupuestos asociados
        public async Task<IEnumerable<Monedas>> GetWithPresupuestosAsync()
        {
            return await _context.Monedas
                .Include(m => m.Presupuestos)
                .ToListAsync();
        }
    }
}