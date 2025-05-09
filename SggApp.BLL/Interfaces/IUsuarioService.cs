using SggApp.DAL.Entidades;

namespace SggApp.BLL.Interfaces
{
    /// <summary>
    /// Interfaz que define las operaciones para el servicio de usuarios
    /// </summary>
    public interface IUsuarioService
    {
        /// <summary>
        /// Obtiene todos los usuarios registrados
        /// </summary>
        /// <returns>Lista de usuarios</returns>
        Task<IEnumerable<Usuarios>> GetAllAsync();

        /// <summary>
        /// Obtiene un usuario por su identificador
        /// </summary>
        /// <param name="id">Identificador del usuario</param>
        /// <returns>Usuario encontrado o null si no existe</returns>
        Task<Usuarios> GetByIdAsync(int id);

        /// <summary>
        /// Obtiene un usuario por su correo electrónico
        /// </summary>
        /// <param name="email">Correo electrónico del usuario</param>
        /// <returns>Usuario encontrado o null si no existe</returns>
        Task<Usuarios> GetByEmailAsync(string email);

        /// <summary>
        /// Crea un nuevo usuario
        /// </summary>
        /// <param name="usuario">Datos del usuario a crear</param>
        /// <returns>Usuario creado</returns>
        Task<Usuarios> CreateAsync(Usuarios usuario);

        /// <summary>
        /// Actualiza los datos de un usuario existente
        /// </summary>
        /// <param name="id">Identificador del usuario</param>
        /// <param name="usuario">Nuevos datos del usuario</param>
        /// <returns>True si se actualizó correctamente, False en caso contrario</returns>
        Task<bool> UpdateAsync(int id, Usuarios usuario);

        /// <summary>
        /// Elimina un usuario existente
        /// </summary>
        /// <param name="id">Identificador del usuario a eliminar</param>
        /// <returns>True si se eliminó correctamente, False en caso contrario</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Verifica si un correo electrónico ya está registrado
        /// </summary>
        /// <param name="email">Correo electrónico a verificar</param>
        /// <returns>True si el correo ya existe, False en caso contrario</returns>
        Task<bool> EmailExistsAsync(string email);
    }
}