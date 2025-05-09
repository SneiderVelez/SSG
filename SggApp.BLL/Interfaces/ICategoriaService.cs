using SggApp.DAL.Entidades;

namespace SggApp.BLL.Interfaces
{
    /// <summary>
    /// Interfaz que define las operaciones para el servicio de categorías
    /// </summary>
    public interface ICategoriaService
    {
        /// <summary>
        /// Obtiene todas las categorías registradas
        /// </summary>
        /// <returns>Lista de categorías</returns>
        Task<IEnumerable<Categorias>> GetAllAsync();

        /// <summary>
        /// Obtiene una categoría por su identificador
        /// </summary>
        /// <param name="id">Identificador de la categoría</param>
        /// <returns>Categoría encontrada o null si no existe</returns>
        Task<Categorias> GetByIdAsync(int id);

        /// <summary>
        /// Crea una nueva categoría
        /// </summary>
        /// <param name="categoria">Datos de la categoría a crear</param>
        /// <returns>Categoría creada</returns>
        Task<Categorias> CreateAsync(Categorias categoria);

        /// <summary>
        /// Actualiza los datos de una categoría existente
        /// </summary>
        /// <param name="id">Identificador de la categoría</param>
        /// <param name="categoria">Nuevos datos de la categoría</param>
        /// <returns>True si se actualizó correctamente, False en caso contrario</returns>
        Task<bool> UpdateAsync(int id, Categorias categoria);

        /// <summary>
        /// Elimina una categoría existente
        /// </summary>
        /// <param name="id">Identificador de la categoría a eliminar</param>
        /// <returns>True si se eliminó correctamente, False en caso contrario</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Verifica si existe una categoría con el nombre especificado
        /// </summary>
        /// <param name="nombre">Nombre de la categoría a buscar</param>
        /// <returns>True si existe, False en caso contrario</returns>
        Task<bool> ExistsByNombreAsync(string nombre);
    }
}