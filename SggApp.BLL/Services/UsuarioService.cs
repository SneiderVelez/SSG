using SggApp.BLL.Interfaces;
using SggApp.DAL.Entidades;
using SggApp.DAL.Repositorios;

namespace SggApp.BLL.Services
{
    /// <summary>
    /// Implementación del servicio para la gestión de usuarios
    /// </summary>
    public class UsuarioService : IUsuarioService
    {
        private readonly UsuarioRepository _usuarioRepository;
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor que inicializa el repositorio de usuarios
        /// </summary>
        /// <param name="context">Contexto de base de datos</param>
        public UsuarioService(ApplicationDbContext context)
        {
            _context = context;
            _usuarioRepository = new UsuarioRepository(context);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Usuarios>> GetAllAsync()
        {
            return await _usuarioRepository.GetAllAsync();
        }

        /// <inheritdoc />
        public async Task<Usuarios> GetByIdAsync(int id)
        {
            return await _usuarioRepository.GetByIdAsync(id);
        }

        /// <inheritdoc />
        public async Task<Usuarios> GetByEmailAsync(string email)
        {
            return await _usuarioRepository.GetByEmailAsync(email);
        }

        /// <inheritdoc />
        public async Task<Usuarios> CreateAsync(Usuarios usuario)
        {
            // Validar que el correo electrónico no esté ya registrado
            if (await EmailExistsAsync(usuario.Email))
            {
                throw new InvalidOperationException($"El correo electrónico {usuario.Email} ya está registrado");
            }

            // Establecer la fecha de registro como la fecha actual
            usuario.FechaRegistro = DateTime.Now;

            // Agregar el usuario al repositorio
            await _usuarioRepository.AddAsync(usuario);

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            return usuario;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateAsync(int id, Usuarios usuario)
        {
            // Verificar que el usuario existe
            var usuarioExistente = await _usuarioRepository.GetByIdAsync(id);
            if (usuarioExistente == null)
            {
                return false;
            }

            // Verificar que si se cambia el correo, no exista otro usuario con ese correo
            if (usuario.Email != usuarioExistente.Email && await EmailExistsAsync(usuario.Email))
            {
                throw new InvalidOperationException($"El correo electrónico {usuario.Email} ya está registrado por otro usuario");
            }

            // Actualizar las propiedades del usuario
            usuarioExistente.Nombre = usuario.Nombre;
            usuarioExistente.Email = usuario.Email;

            // Solo actualizar el hash de contraseña si se proporciona uno nuevo
            if (!string.IsNullOrEmpty(usuario.PasswordHash))
            {
                usuarioExistente.PasswordHash = usuario.PasswordHash;
            }

            // Actualizar el usuario en el repositorio
            _usuarioRepository.Update(usuarioExistente);

            // Guardar los cambios
            await _context.SaveChangesAsync();

            return true;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
        {
            // Verificar que el usuario existe
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
            {
                return false;
            }

            // Eliminar el usuario
            _usuarioRepository.Delete(usuario);

            // Guardar los cambios
            await _context.SaveChangesAsync();

            return true;
        }

        /// <inheritdoc />
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _usuarioRepository.EmailExistsAsync(email);
        }
    }
}