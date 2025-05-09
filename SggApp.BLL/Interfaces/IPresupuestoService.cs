using SggApp.DAL.Entidades;

namespace SggApp.BLL.Interfaces
{
    /// <summary>
    /// Interfaz que define las operaciones para el servicio de presupuestos
    /// </summary>
    public interface IPresupuestoService
    {
        /// <summary>
        /// Obtiene todos los presupuestos registrados
        /// </summary>
        /// <returns>Lista de presupuestos</returns>
        Task<IEnumerable<Presupuestos>> GetAllAsync();

        /// <summary>
        /// Obtiene un presupuesto por su identificador
        /// </summary>
        /// <param name="id">Identificador del presupuesto</param>
        /// <returns>Presupuesto encontrado o null si no existe</returns>
        Task<Presupuestos> GetByIdAsync(int id);

        /// <summary>
        /// Obtiene los presupuestos de un usuario específico
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario</param>
        /// <returns>Lista de presupuestos del usuario</returns>
        Task<IEnumerable<Presupuestos>> GetByUsuarioIdAsync(int usuarioId);

        /// <summary>
        /// Obtiene los presupuestos por categoría
        /// </summary>
        /// <param name="categoriaId">Identificador de la categoría</param>
        /// <returns>Lista de presupuestos de la categoría</returns>
        Task<IEnumerable<Presupuestos>> GetByCategoriaIdAsync(int categoriaId);

        /// <summary>
        /// Crea un nuevo presupuesto
        /// </summary>
        /// <param name="presupuesto">Datos del presupuesto a crear</param>
        /// <returns>Presupuesto creado</returns>
        Task<Presupuestos> CreateAsync(Presupuestos presupuesto);

        /// <summary>
        /// Actualiza los datos de un presupuesto existente
        /// </summary>
        /// <param name="id">Identificador del presupuesto</param>
        /// <param name="presupuesto">Nuevos datos del presupuesto</param>
        /// <returns>True si se actualizó correctamente, False en caso contrario</returns>
        Task<bool> UpdateAsync(int id, Presupuestos presupuesto);

        /// <summary>
        /// Elimina un presupuesto existente
        /// </summary>
        /// <param name="id">Identificador del presupuesto a eliminar</param>
        /// <returns>True si se eliminó correctamente, False en caso contrario</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Obtiene los presupuestos vigentes para un período específico
        /// </summary>
        /// <param name="fecha">Fecha para la cual verificar presupuestos vigentes</param>
        /// <returns>Lista de presupuestos vigentes</returns>
        Task<IEnumerable<Presupuestos>> GetVigentesByFechaAsync(DateTime fecha);

        /// <summary>
        /// Verifica si un presupuesto ha superado su límite considerando los gastos asociados
        /// </summary>
        /// <param name="presupuestoId">Identificador del presupuesto</param>
        /// <returns>True si el presupuesto está excedido, False en caso contrario</returns>
        Task<bool> VerificarLimiteExcedidoAsync(int presupuestoId);
    }
}