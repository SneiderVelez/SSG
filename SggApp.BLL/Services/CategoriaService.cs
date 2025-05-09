using Microsoft.EntityFrameworkCore;
using SggApp.BLL.Interfaces;
using SggApp.DAL.Entidades;
using SggApp.DAL.Repositorios;

namespace SggApp.BLL.Services
{
    /// <summary>
    /// Implementación del servicio para la gestión de categorías
    /// </summary>
    public class CategoriaService : ICategoriaService
    {
        private readonly CategoriaRepository _categoriaRepository;
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor que inicializa el repositorio de categorías
        /// </summary>
        /// <param name="context">Contexto de base de datos</param>
        public CategoriaService(ApplicationDbContext context)
        {
            _context = context;
            _categoriaRepository = new CategoriaRepository(context);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Categorias>> GetAllAsync()
        {
            return await _categoriaRepository.GetAllAsync();
        }

        /// <inheritdoc />
        public async Task<Categorias> GetByIdAsync(int id)
        {
            return await _categoriaRepository.GetByIdAsync(id);
        }

        /// <inheritdoc />
        public async Task<Categorias> CreateAsync(Categorias categoria)
        {
            // Validar que no exista una categoría con el mismo nombre
            if (await ExistsByNombreAsync(categoria.Nombre))
            {
                throw new InvalidOperationException($"Ya existe una categoría con el nombre '{categoria.Nombre}'");
            }

            // Agregar la categoría al repositorio
            await _categoriaRepository.AddAsync(categoria);

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            return categoria;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateAsync(int id, Categorias categoria)
        {
            // Verificar que la categoría existe
            var categoriaExistente = await _categoriaRepository.GetByIdAsync(id);
            if (categoriaExistente == null)
            {
                return false;
            }

            // Verificar que no exista otra categoría con el mismo nombre
            if (categoria.Nombre != categoriaExistente.Nombre && await ExistsByNombreAsync(categoria.Nombre))
            {
                throw new InvalidOperationException($"Ya existe otra categoría con el nombre '{categoria.Nombre}'");
            }

            // Actualizar las propiedades de la categoría
            categoriaExistente.Nombre = categoria.Nombre;
            categoriaExistente.Descripcion = categoria.Descripcion;

            // Actualizar la categoría en el repositorio
            _categoriaRepository.Update(categoriaExistente);

            // Guardar los cambios
            await _context.SaveChangesAsync();

            return true;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
        {
            // Verificar que la categoría existe
            var categoria = await _categoriaRepository.GetByIdAsync(id);
            if (categoria == null)
            {
                return false;
            }

            // Verificar que la categoría no tenga gastos ni presupuestos asociados
            var tieneGastos = await _context.Set<Gastos>().AnyAsync(g => g.CategoriaId == id);
            var tienePresupuestos = await _context.Set<Presupuestos>().AnyAsync(p => p.CategoriaId == id);

            if (tieneGastos || tienePresupuestos)
            {
                throw new InvalidOperationException("No se puede eliminar la categoría porque tiene gastos o presupuestos asociados");
            }

            // Eliminar la categoría
            _categoriaRepository.Delete(categoria);

            // Guardar los cambios
            await _context.SaveChangesAsync();

            return true;
        }

        /// <inheritdoc />
        public async Task<bool> ExistsByNombreAsync(string nombre)
        {
            // Verificar si existe una categoría con el nombre especificado
            return await _categoriaRepository.ExistsByNombreAsync(nombre);
        }
    }
}