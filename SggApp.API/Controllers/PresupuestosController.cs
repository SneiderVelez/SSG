using Microsoft.AspNetCore.Mvc;
using SggApp.BLL.Interfaces;
using SggApp.DAL.Entidades;

namespace SggApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresupuestosController : ControllerBase
    {
        private readonly IPresupuestoService _presupuestoService;

        public PresupuestosController(IPresupuestoService presupuestoService)
        {
            _presupuestoService = presupuestoService;
        }

        /// <summary>
        /// Obtiene todos los presupuestos
        /// </summary>
        /// <returns>Lista de presupuestos</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Presupuestos>>> GetAll()
        {
            var presupuestos = await _presupuestoService.GetAllAsync();
            return Ok(presupuestos);
        }

        /// <summary>
        /// Obtiene un presupuesto por su identificador
        /// </summary>
        /// <param name="id">Identificador del presupuesto</param>
        /// <returns>Presupuesto encontrado</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Presupuestos>> GetById(int id)
        {
            var presupuesto = await _presupuestoService.GetByIdAsync(id);
            if (presupuesto == null)
            {
                return NotFound($"Presupuesto con ID {id} no encontrado");
            }
            return Ok(presupuesto);
        }

        /// <summary>
        /// Obtiene los presupuestos de un usuario específico
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario</param>
        /// <returns>Lista de presupuestos del usuario</returns>
        [HttpGet("porUsuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<Presupuestos>>> GetByUsuarioId(int usuarioId)
        {
            var presupuestos = await _presupuestoService.GetByUsuarioIdAsync(usuarioId);
            return Ok(presupuestos);
        }

        /// <summary>
        /// Obtiene los presupuestos por categoría
        /// </summary>
        /// <param name="categoriaId">Identificador de la categoría</param>
        /// <returns>Lista de presupuestos de la categoría</returns>
        [HttpGet("porCategoria/{categoriaId}")]
        public async Task<ActionResult<IEnumerable<Presupuestos>>> GetByCategoriaId(int categoriaId)
        {
            var presupuestos = await _presupuestoService.GetByCategoriaIdAsync(categoriaId);
            return Ok(presupuestos);
        }

        /// <summary>
        /// Obtiene los presupuestos vigentes para una fecha específica
        /// </summary>
        /// <param name="fecha">Fecha para verificar vigencia</param>
        /// <returns>Lista de presupuestos vigentes</returns>
        [HttpGet("vigentes")]
        public async Task<ActionResult<IEnumerable<Presupuestos>>> GetVigentesByFecha([FromQuery] DateTime fecha)
        {
            var presupuestos = await _presupuestoService.GetVigentesByFechaAsync(fecha);
            return Ok(presupuestos);
        }

        /// <summary>
        /// Crea un nuevo presupuesto
        /// </summary>
        /// <param name="presupuesto">Datos del presupuesto a crear</param>
        /// <returns>Presupuesto creado</returns>
        [HttpPost]
        public async Task<ActionResult<Presupuestos>> Create([FromBody] Presupuestos presupuesto)
        {
            try
            {
                var presupuestoCreado = await _presupuestoService.CreateAsync(presupuesto);
                return CreatedAtAction(nameof(GetById), new { id = presupuestoCreado.Id }, presupuestoCreado);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Actualiza los datos de un presupuesto existente
        /// </summary>
        /// <param name="id">Identificador del presupuesto</param>
        /// <param name="presupuesto">Nuevos datos del presupuesto</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Presupuestos presupuesto)
        {
            try
            {
                var resultado = await _presupuestoService.UpdateAsync(id, presupuesto);
                if (!resultado)
                {
                    return NotFound($"Presupuesto con ID {id} no encontrado");
                }
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Elimina un presupuesto existente
        /// </summary>
        /// <param name="id">Identificador del presupuesto a eliminar</param>
        /// <returns>Resultado de la operación</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var resultado = await _presupuestoService.DeleteAsync(id);
                if (!resultado)
                {
                    return NotFound($"Presupuesto con ID {id} no encontrado");
                }
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Verifica si un presupuesto ha superado su límite
        /// </summary>
        /// <param name="id">Identificador del presupuesto</param>
        /// <returns>True si el límite está excedido, False en caso contrario</returns>
        [HttpGet("{id}/verificarLimite")]
        public async Task<ActionResult<bool>> VerificarLimiteExcedido(int id)
        {
            try
            {
                var excedido = await _presupuestoService.VerificarLimiteExcedidoAsync(id);
                return Ok(excedido);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}