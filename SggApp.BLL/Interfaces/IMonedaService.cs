using SggApp.DAL.Entidades;

namespace SggApp.BLL.Interfaces
{
    /// <summary>
    /// Interfaz que define las operaciones para el servicio de monedas
    /// </summary>
    public interface IMonedaService
    {
        /// <summary>
        /// Obtiene todas las monedas registradas
        /// </summary>
        /// <returns>Lista de monedas</returns>
        Task<IEnumerable<Monedas>> GetAllAsync();

        /// <summary>
        /// Obtiene una moneda por su identificador
        /// </summary>
        /// <param name="id">Identificador de la moneda</param>
        /// <returns>Moneda encontrada o null si no existe</returns>
        Task<Monedas> GetByIdAsync(int id);

        /// <summary>
        /// Obtiene una moneda por su código ISO
        /// </summary>
        /// <param name="codigo">Código ISO de la moneda</param>
        /// <returns>Moneda encontrada o null si no existe</returns>
        Task<Monedas> GetByCodigoAsync(string codigo);

        /// <summary>
        /// Crea una nueva moneda
        /// </summary>
        /// <param name="moneda">Datos de la moneda a crear</param>
        /// <returns>Moneda creada</returns>
        Task<Monedas> CreateAsync(Monedas moneda);

        /// <summary>
        /// Actualiza los datos de una moneda existente
        /// </summary>
        /// <param name="id">Identificador de la moneda</param>
        /// <param name="moneda">Nuevos datos de la moneda</param>
        /// <returns>True si se actualizó correctamente, False en caso contrario</returns>
        Task<bool> UpdateAsync(int id, Monedas moneda);

        /// <summary>
        /// Elimina una moneda existente
        /// </summary>
        /// <param name="id">Identificador de la moneda a eliminar</param>
        /// <returns>True si se eliminó correctamente, False en caso contrario</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Verifica si existe una moneda con el código ISO especificado
        /// </summary>
        /// <param name="codigo">Código ISO de la moneda a buscar</param>
        /// <returns>True si existe, False en caso contrario</returns>
        Task<bool> ExistsByCodigoAsync(string codigo);
    }
}