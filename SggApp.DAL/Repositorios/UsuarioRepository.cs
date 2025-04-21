using Microsoft.EntityFrameworkCore;
using SggApp.DAL.Entidades;

namespace SggApp.DAL.Repositorios
{
    public class UsuarioRepository : GenericRepository<Usuarios>
    {
        public UsuarioRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Método específico: Obtener un usuario por su correo electrónico
        public async Task<Usuarios> GetByEmailAsync(string email)
        {
            return await ((ApplicationDbContext)_context).Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        // Método específico: Verificar si un correo electrónico ya está registrado
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await ((ApplicationDbContext)_context).Usuarios.AnyAsync(u => u.Email == email);
        }

        // Método específico: Obtener todos los usuarios con sus gastos asociados
        public async Task<IEnumerable<Usuarios>> GetAllWithGastosAsync()
        {
            return await ((ApplicationDbContext)_context).Usuarios
                .Include(u => u.Gastos) // Incluir los gastos relacionados
                .ToListAsync();
        }
    }
}