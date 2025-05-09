using Microsoft.EntityFrameworkCore;
using SggApp.BLL.Interfaces;
using SggApp.DAL.Entidades;
using SggApp.DAL.Repositorios;

namespace SggApp.BLL.Services
{
    /// <summary>
    /// Implementación del servicio para la gestión de monedas
    /// </summary>
    public class MonedaService : IMonedaService
    {
        private readonly MonedaRepository _monedaRepository;
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor que inicializa el repositorio de monedas
        /// </summary>
        /// <param name="context">Contexto de base de datos</param>
        public MonedaService(ApplicationDbContext context)
        {
            _context = context;
            _monedaRepository = new MonedaRepository(context);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Monedas>> GetAllAsync()
        {
            return await _monedaRepository.GetAllAsync();
        }

        /// <inheritdoc />
        public async Task<Monedas> GetByIdAsync(int id)
        {
            return await _monedaRepository.GetByIdAsync(id);
        }

        /// <inheritdoc />
        public async Task<Monedas> GetByCodigoAsync(string codigo)
        {
            return await _monedaRepository.GetByCodigoAsync(codigo);
        }

        /// <inheritdoc />
        public async Task<Monedas> CreateAsync(Monedas moneda)
        {
            // Validar que no exista una moneda con el mismo código
            if (await ExistsByCodigoAsync(moneda.Codigo))
            {
                throw new InvalidOperationException($"Ya existe una moneda con el código '{moneda.Codigo}'");
            }

            // Normalizar el código (asegurarse que esté en mayúsculas)
            moneda.Codigo = moneda.Codigo.ToUpper();

            // Agregar la moneda al repositorio
            await _monedaRepository.AddAsync(moneda);

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            return moneda;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateAsync(int id, Monedas moneda)
        {
            // Verificar que la moneda existe
            var monedaExistente = await _monedaRepository.GetByIdAsync(id);
            if (monedaExistente == null)
            {
                return false;
            }

            // Normalizar el código (asegurarse que esté en mayúsculas)
            moneda.Codigo = moneda.Codigo.ToUpper();

            // Verificar que no exista otra moneda con el mismo código
            if (moneda.Codigo != monedaExistente.Codigo && await ExistsByCodigoAsync(moneda.Codigo))
            {
                throw new InvalidOperationException($"Ya existe otra moneda con el código '{moneda.Codigo}'");
            }

            // Actualizar las propiedades de la moneda
            monedaExistente.Codigo = moneda.Codigo;
            monedaExistente.Nombre = moneda.Nombre;
            monedaExistente.Simbolo = moneda.Simbolo;

            // Actualizar la moneda en el repositorio
            _monedaRepository.Update(monedaExistente);

            // Guardar los cambios
            await _context.SaveChangesAsync();

            return true;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
        {
            // Verificar que la moneda existe
            var moneda = await _monedaRepository.GetByIdAsync(id);
            if (moneda == null)
            {
                return false;
            }

            // Verificar que la moneda no tenga gastos ni presupuestos asociados
            var tieneGastos = await _context.Set<Gastos>().AnyAsync(g => g.MonedaId == id);
            var tienePresupuestos = await _context.Set<Presupuestos>().AnyAsync(p => p.MonedaId == id);

            if (tieneGastos || tienePresupuestos)
            {
                throw new InvalidOperationException("No se puede eliminar la moneda porque tiene gastos o presupuestos asociados");
            }

            // Eliminar la moneda
            _monedaRepository.Delete(moneda);

            // Guardar los cambios
            await _context.SaveChangesAsync();

            return true;
        }

        /// <inheritdoc />
        public async Task<bool> ExistsByCodigoAsync(string codigo)
        {
            return await _monedaRepository.ExistsByCodigoAsync(codigo);
        }
    }
}