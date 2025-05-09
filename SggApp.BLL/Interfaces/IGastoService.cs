using SggApp.DAL.Entidades;

namespace SggApp.BLL.Interfaces
{
    /// <summary>
    /// Interfaz que define las operaciones para el servicio de gastos
    /// </summary>
    public interface IGastoService
    {
        /// <summary>
        /// Obtiene todos los gastos registrados
        /// </summary>
        /// <returns>Lista de gastos</returns>
        Task<IEnumerable<Gastos>> GetAllAsync();

        /// <summary>
        /// Obtiene un gasto por su identificador
        /// </summary>
        /// <param name="id">Identificador del gasto</param>
        /// <returns>Gasto encontrado o null si no existe</returns>
        Task<Gastos> GetByIdAsync(int id);

        /// <summary>
        /// Obtiene los gastos de un usuario específico
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario</param>
        /// <returns>Lista de gastos del usuario</returns>
        Task<IEnumerable<Gastos>> GetByUsuarioIdAsync(int usuarioId);

        /// <summary>
        /// Obtiene los gastos por categoría
        /// </summary>
        /// <param name="categoriaId">Identificador de la categoría</param>
        /// <returns>Lista de gastos de la categoría</returns>
        Task<IEnumerable<Gastos>> GetByCategoriaIdAsync(int categoriaId);

        /// <summary>
        /// Crea un nuevo gasto
        /// </summary>
        /// <param name="gasto">Datos del gasto a crear</param>
        /// <returns>Gasto creado</returns>
        Task<Gastos> CreateAsync(Gastos gasto);

        /// <summary>
        /// Actualiza los datos de un gasto existente
        /// </summary>
        /// <param name="id">Identificador del gasto</param>
        /// <param name="gasto">Nuevos datos del gasto</param>
        /// <returns>True si se actualizó correctamente, False en caso contrario</returns>
        Task<bool> UpdateAsync(int id, Gastos gasto);

        /// <summary>
        /// Elimina un gasto existente
        /// </summary>
        /// <param name="id">Identificador del gasto a eliminar</param>
        /// <returns>True si se eliminó correctamente, False en caso contrario</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Obtiene los gastos realizados en un período específico
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio del período</param>
        /// <param name="fechaFin">Fecha de fin del período</param>
        /// <returns>Lista de gastos en el período</returns>
        Task<IEnumerable<Gastos>> GetByPeriodoAsync(DateTime fechaInicio, DateTime fechaFin);
    }
}