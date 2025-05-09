using Microsoft.EntityFrameworkCore;
using SggApp.BLL.Interfaces;
using SggApp.DAL.Entidades;
using SggApp.DAL.Repositorios;

namespace SggApp.BLL.Services
{
    /// <summary>
    /// Implementación del servicio para la gestión de presupuestos
    /// </summary>
    public class PresupuestoService : IPresupuestoService
    {
        private readonly PresupuestoRepository _presupuestoRepository;
        private readonly GastoRepository _gastoRepository;
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor que inicializa los repositorios necesarios
        /// </summary>
        /// <param name="context">Contexto de base de datos</param>
        public PresupuestoService(ApplicationDbContext context)
        {
            _context = context;
            _presupuestoRepository = new PresupuestoRepository(context);
            _gastoRepository = new GastoRepository(context);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Presupuestos>> GetAllAsync()
        {
            return await _presupuestoRepository.GetAllAsync();
        }

        /// <inheritdoc />
        public async Task<Presupuestos> GetByIdAsync(int id)
        {
            return await _presupuestoRepository.GetByIdAsync(id);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Presupuestos>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _presupuestoRepository.GetByUsuarioIdAsync(usuarioId);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Presupuestos>> GetByCategoriaIdAsync(int categoriaId)
        {
            return await _presupuestoRepository.GetByCategoriaIdAsync(categoriaId);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Presupuestos>> GetVigentesByFechaAsync(DateTime fecha)
        {
            // Usar el método GetActivosAsync() y luego filtrar si es necesario
            var presupuestosActivos = await _presupuestoRepository.GetActivosAsync();
            return presupuestosActivos.Where(p => p.FechaInicio <= fecha && p.FechaFin >= fecha).ToList();
        }

        /// <inheritdoc />
        public async Task<Presupuestos> CreateAsync(Presupuestos presupuesto)
        {
            // Validar que el usuario existe
            var usuarioExiste = await _context.Set<Usuarios>().AnyAsync(u => u.Id == presupuesto.UsuarioId);
            if (!usuarioExiste)
            {
                throw new InvalidOperationException($"El usuario con ID {presupuesto.UsuarioId} no existe");
            }

            // Validar que la categoría existe
            var categoriaExiste = await _context.Set<Categorias>().AnyAsync(c => c.Id == presupuesto.CategoriaId);
            if (!categoriaExiste)
            {
                throw new InvalidOperationException($"La categoría con ID {presupuesto.CategoriaId} no existe");
            }

            // Validar que la moneda existe
            var monedaExiste = await _context.Set<Monedas>().AnyAsync(m => m.Id == presupuesto.MonedaId);
            if (!monedaExiste)
            {
                throw new InvalidOperationException($"La moneda con ID {presupuesto.MonedaId} no existe");
            }

            // Validar que el monto sea mayor que cero
            if (presupuesto.Limite <= 0)
            {
                throw new InvalidOperationException("El monto límite debe ser mayor que cero");
            }

            // Validar que la fecha de fin sea posterior a la fecha de inicio
            if (presupuesto.FechaFin <= presupuesto.FechaInicio)
            {
                throw new InvalidOperationException("La fecha de fin debe ser posterior a la fecha de inicio");
            }

            // Agregar el presupuesto al repositorio
            await _presupuestoRepository.AddAsync(presupuesto);

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            return presupuesto;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateAsync(int id, Presupuestos presupuesto)
        {
            // Verificar que el presupuesto existe
            var presupuestoExistente = await _presupuestoRepository.GetByIdAsync(id);
            if (presupuestoExistente == null)
            {
                return false;
            }

            // Validar que la categoría existe
            if (presupuesto.CategoriaId != presupuestoExistente.CategoriaId)
            {
                var categoriaExiste = await _context.Set<Categorias>().AnyAsync(c => c.Id == presupuesto.CategoriaId);
                if (!categoriaExiste)
                {
                    throw new InvalidOperationException($"La categoría con ID {presupuesto.CategoriaId} no existe");
                }
            }

            // Validar que la moneda existe
            if (presupuesto.MonedaId != presupuestoExistente.MonedaId)
            {
                var monedaExiste = await _context.Set<Monedas>().AnyAsync(m => m.Id == presupuesto.MonedaId);
                if (!monedaExiste)
                {
                    throw new InvalidOperationException($"La moneda con ID {presupuesto.MonedaId} no existe");
                }
            }

            // Validar que el monto sea mayor que cero
            if (presupuesto.Limite <= 0)
            {
                throw new InvalidOperationException("El monto límite debe ser mayor que cero");
            }

            // Validar que la fecha de fin sea posterior a la fecha de inicio
            if (presupuesto.FechaFin <= presupuesto.FechaInicio)
            {
                throw new InvalidOperationException("La fecha de fin debe ser posterior a la fecha de inicio");
            }

            // Actualizar las propiedades del presupuesto
            presupuestoExistente.CategoriaId = presupuesto.CategoriaId;
            presupuestoExistente.MonedaId = presupuesto.MonedaId;
            presupuestoExistente.Limite = presupuesto.Limite;
            presupuestoExistente.FechaInicio = presupuesto.FechaInicio;
            presupuestoExistente.FechaFin = presupuesto.FechaFin;

            // Actualizar el presupuesto en el repositorio
            _presupuestoRepository.Update(presupuestoExistente);

            // Guardar los cambios
            await _context.SaveChangesAsync();

            return true;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
        {
            // Verificar que el presupuesto existe
            var presupuesto = await _presupuestoRepository.GetByIdAsync(id);
            if (presupuesto == null)
            {
                return false;
            }

            // Eliminar el presupuesto
            _presupuestoRepository.Delete(presupuesto);

            // Guardar los cambios
            await _context.SaveChangesAsync();

            return true;
        }

        /// <inheritdoc />
        public async Task<bool> VerificarLimiteExcedidoAsync(int presupuestoId)
        {
            // Obtener el presupuesto
            var presupuesto = await _presupuestoRepository.GetByIdAsync(presupuestoId);
            if (presupuesto == null)
            {
                throw new InvalidOperationException($"El presupuesto con ID {presupuestoId} no existe");
            }

            // Obtener los gastos relacionados con la categoría y en el período del presupuesto
            var gastos = await _gastoRepository.GetByUsuarioYFechasAsync(
                presupuesto.UsuarioId,
                presupuesto.FechaInicio,
                presupuesto.FechaFin);

            // Filtrar por categoría
            var gastosFiltrados = gastos.Where(g => g.CategoriaId == presupuesto.CategoriaId).ToList();

            // Calcular la suma de los gastos
            decimal totalGastos = gastosFiltrados.Sum(g => g.Monto);

            // Verificar si se excede el límite
            return totalGastos > presupuesto.Limite;
        }
    }
}