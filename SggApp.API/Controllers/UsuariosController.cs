using Microsoft.AspNetCore.Mvc;
using SggApp.BLL.Interfaces;
using SggApp.DAL.Entidades;

namespace SggApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Obtiene todos los usuarios
        /// </summary>
        /// <returns>Lista de usuarios</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuarios>>> GetAll()
        {
            var usuarios = await _usuarioService.GetAllAsync();
            return Ok(usuarios);
        }

        /// <summary>
        /// Obtiene un usuario por su identificador
        /// </summary>
        /// <param name="id">Identificador del usuario</param>
        /// <returns>Usuario encontrado</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuarios>> GetById(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound($"Usuario con ID {id} no encontrado");
            }
            return Ok(usuario);
        }

        /// <summary>
        /// Crea un nuevo usuario
        /// </summary>
        /// <param name="usuario">Datos del usuario a crear</param>
        /// <returns>Usuario creado</returns>
        [HttpPost]
        public async Task<ActionResult<Usuarios>> Create([FromBody] Usuarios usuario)
        {
            try
            {
                var usuarioCreado = await _usuarioService.CreateAsync(usuario);
                return CreatedAtAction(nameof(GetById), new { id = usuarioCreado.Id }, usuarioCreado);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Actualiza los datos de un usuario existente
        /// </summary>
        /// <param name="id">Identificador del usuario</param>
        /// <param name="usuario">Nuevos datos del usuario</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Usuarios usuario)
        {
            try
            {
                var resultado = await _usuarioService.UpdateAsync(id, usuario);
                if (!resultado)
                {
                    return NotFound($"Usuario con ID {id} no encontrado");
                }
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Elimina un usuario existente
        /// </summary>
        /// <param name="id">Identificador del usuario a eliminar</param>
        /// <returns>Resultado de la operación</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var resultado = await _usuarioService.DeleteAsync(id);
            if (!resultado)
            {
                return NotFound($"Usuario con ID {id} no encontrado");
            }
            return NoContent();
        }

        /// <summary>
        /// Obtiene un usuario por su correo electrónico
        /// </summary>
        /// <param name="email">Correo electrónico a buscar</param>
        /// <returns>Usuario encontrado</returns>
        [HttpGet("porEmail")]
        public async Task<ActionResult<Usuarios>> GetByEmail([FromQuery] string email)
        {
            var usuario = await _usuarioService.GetByEmailAsync(email);
            if (usuario == null)
            {
                return NotFound($"Usuario con email {email} no encontrado");
            }
            return Ok(usuario);
        }
    }
}