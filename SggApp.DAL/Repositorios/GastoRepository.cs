using Microsoft.EntityFrameworkCore;
using SggApp.DAL.Entidades;

namespace SggApp.DAL.Repositorios
{
    public class GastoRepository : GenericRepository<Gastos>
    {
        private readonly ApplicationDbContext _context;

        public GastoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // Método específico: Obtener gastos por usuario con detalles
        public async Task<IEnumerable<Gastos>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _context.Gastos
                .Where(g => g.UsuarioId == usuarioId)
                .Include(g => g.Categoria)
                .Include(g => g.Moneda)
                .OrderByDescending(g => g.Fecha)
                .ToListAsync();
        }

        // Método específico: Obtener gastos por categoría
        public async Task<IEnumerable<Gastos>> GetByCategoriaIdAsync(int categoriaId)
        {
            return await _context.Gastos
                .Where(g => g.CategoriaId == categoriaId)
                .Include(g => g.Usuario)
                .Include(g => g.Moneda)
                .OrderByDescending(g => g.Fecha)
                .ToListAsync();
        }

        // Método específico: Obtener gastos por rango de fechas
        public async Task<IEnumerable<Gastos>> GetByRangoFechasAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _context.Gastos
                .Where(g => g.Fecha >= fechaInicio && g.Fecha <= fechaFin)
                .Include(g => g.Usuario)
                .Include(g => g.Categoria)
                .Include(g => g.Moneda)
                .OrderByDescending(g => g.Fecha)
                .ToListAsync();
        }

        // Método específico: Obtener gastos por usuario y rango de fechas
        public async Task<IEnumerable<Gastos>> GetByUsuarioYFechasAsync(int usuarioId, DateTime fechaInicio, DateTime fechaFin)
        {
            return await _context.Gastos
                .Where(g => g.UsuarioId == usuarioId && g.Fecha >= fechaInicio && g.Fecha <= fechaFin)
                .Include(g => g.Categoria)
                .Include(g => g.Moneda)
                .OrderByDescending(g => g.Fecha)
                .ToListAsync();
        }

        // Método específico: Obtener gastos con todos sus detalles
        public async Task<IEnumerable<Gastos>> GetAllWithDetailsAsync()
        {
            return await _context.Gastos
                .Include(g => g.Usuario)
                .Include(g => g.Categoria)
                .Include(g => g.Moneda)
                .OrderByDescending(g => g.Fecha)
                .ToListAsync();
        }

        // Método específico: Obtener suma total de gastos por usuario
        public async Task<decimal> GetTotalGastosByUsuarioAsync(int usuarioId)
        {
            return await _context.Gastos
                .Where(g => g.UsuarioId == usuarioId)
                .SumAsync(g => g.Monto);
        }

        // Método específico: Obtener suma total de gastos por categoría
        public async Task<decimal> GetTotalGastosByCategoriaAsync(int categoriaId)
        {
            return await _context.Gastos
                .Where(g => g.CategoriaId == categoriaId)
                .SumAsync(g => g.Monto);
        }
    }
}