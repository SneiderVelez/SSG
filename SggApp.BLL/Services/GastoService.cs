using Microsoft.EntityFrameworkCore;
using SggApp.BLL.Interfaces;
using SggApp.DAL.Entidades;
using SggApp.DAL.Repositorios;

namespace SggApp.BLL.Services
{
    /// <summary>
    /// Implementación del servicio para la gestión de gastos
    /// </summary>
    public class GastoService : IGastoService
    {
        private readonly GastoRepository _gastoRepository;
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor que inicializa el repositorio de gastos
        /// </summary>
        /// <param name="context">Contexto de base de datos</param>
        public GastoService(ApplicationDbContext context)
        {
            _context = context;
            _gastoRepository = new GastoRepository(context);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Gastos>> GetAllAsync()
        {
            return await _gastoRepository.GetAllAsync();
        }

        /// <inheritdoc />
        public async Task<Gastos> GetByIdAsync(int id)
        {
            return await _gastoRepository.GetByIdAsync(id);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Gastos>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _gastoRepository.GetByUsuarioIdAsync(usuarioId);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Gastos>> GetByCategoriaIdAsync(int categoriaId)
        {
            return await _gastoRepository.GetByCategoriaIdAsync(categoriaId);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Gastos>> GetByPeriodoAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _gastoRepository.GetByRangoFechasAsync(fechaInicio, fechaFin);
        }

        /// <inheritdoc />
        public async Task<Gastos> CreateAsync(Gastos gasto)
        {
            // Validar que el usuario existe
            var usuarioExiste = await _context.Set<Usuarios>().AnyAsync(u => u.Id == gasto.UsuarioId);
            if (!usuarioExiste)
            {
                throw new InvalidOperationException($"El usuario con ID {gasto.UsuarioId} no existe");
            }

            // Validar que la categoría existe
            var categoriaExiste = await _context.Set<Categorias>().AnyAsync(c => c.Id == gasto.CategoriaId);
            if (!categoriaExiste)
            {
                throw new InvalidOperationException($"La categoría con ID {gasto.CategoriaId} no existe");
            }

            // Validar que la moneda existe
            var monedaExiste = await _context.Set<Monedas>().AnyAsync(m => m.Id == gasto.MonedaId);
            if (!monedaExiste)
            {
                throw new InvalidOperationException($"La moneda con ID {gasto.MonedaId} no existe");
            }

            // Validar que el monto sea mayor que cero
            if (gasto.Monto <= 0)
            {
                throw new InvalidOperationException("El monto del gasto debe ser mayor que cero");
            }

            // Establecer la fecha de creación si no se proporciona
            if (gasto.Fecha == default)
            {
                gasto.Fecha = DateTime.Now;
            }

            // Agregar el gasto al repositorio
            await _gastoRepository.AddAsync(gasto);

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            return gasto;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateAsync(int id, Gastos gasto)
        {
            // Verificar que el gasto existe
            var gastoExistente = await _gastoRepository.GetByIdAsync(id);
            if (gastoExistente == null)
            {
                return false;
            }

            // Validar que la categoría existe
            if (gasto.CategoriaId != gastoExistente.CategoriaId)
            {
                var categoriaExiste = await _context.Set<Categorias>().AnyAsync(c => c.Id == gasto.CategoriaId);
                if (!categoriaExiste)
                {
                    throw new InvalidOperationException($"La categoría con ID {gasto.CategoriaId} no existe");
                }
            }

            // Validar que la moneda existe
            if (gasto.MonedaId != gastoExistente.MonedaId)
            {
                var monedaExiste = await _context.Set<Monedas>().AnyAsync(m => m.Id == gasto.MonedaId);
                if (!monedaExiste)
                {
                    throw new InvalidOperationException($"La moneda con ID {gasto.MonedaId} no existe");
                }
            }

            // Validar que el monto sea mayor que cero
            if (gasto.Monto <= 0)
            {
                throw new InvalidOperationException("El monto del gasto debe ser mayor que cero");
            }

            // Actualizar las propiedades del gasto
            gastoExistente.CategoriaId = gasto.CategoriaId;
            gastoExistente.MonedaId = gasto.MonedaId;
            gastoExistente.Descripcion = gasto.Descripcion;
            gastoExistente.Monto = gasto.Monto;
            gastoExistente.Fecha = gasto.Fecha;

            // Actualizar el gasto en el repositorio
            _gastoRepository.Update(gastoExistente);

            // Guardar los cambios
            await _context.SaveChangesAsync();

            return true;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
        {
            // Verificar que el gasto existe
            var gasto = await _gastoRepository.GetByIdAsync(id);
            if (gasto == null)
            {
                return false;
            }

            // Eliminar el gasto
            _gastoRepository.Delete(gasto);

            // Guardar los cambios
            await _context.SaveChangesAsync();

            return true;
        }
    }
}