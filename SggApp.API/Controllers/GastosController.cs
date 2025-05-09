using Microsoft.AspNetCore.Mvc;
using SggApp.BLL.Interfaces;
using SggApp.DAL.Entidades;

namespace SggApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GastosController : ControllerBase
    {
        private readonly IGastoService _gastoService;

        public GastosController(IGastoService gastoService)
        {
            _gastoService = gastoService;
        }

        /// <summary>
        /// Obtiene todos los gastos
        /// </summary>
        /// <returns>Lista de gastos</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gastos>>> GetAll()
        {
            var gastos = await _gastoService.GetAllAsync();
            return Ok(gastos);
        }

        /// <summary>
        /// Obtiene un gasto por su identificador
        /// </summary>
        /// <param name="id">Identificador del gasto</param>
        /// <returns>Gasto encontrado</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Gastos>> GetById(int id)
        {
            var gasto = await _gastoService.GetByIdAsync(id);
            if (gasto == null)
            {
                return NotFound($"Gasto con ID {id} no encontrado");
            }
            return Ok(gasto);
        }

        /// <summary>
        /// Obtiene los gastos de un usuario específico
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario</param>
        /// <returns>Lista de gastos del usuario</returns>
        [HttpGet("porUsuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<Gastos>>> GetByUsuarioId(int usuarioId)
        {
            var gastos = await _gastoService.GetByUsuarioIdAsync(usuarioId);
            return Ok(gastos);
        }

        /// <summary>
        /// Obtiene los gastos por categoría
        /// </summary>
        /// <param name="categoriaId">Identificador de la categoría</param>
        /// <returns>Lista de gastos de la categoría</returns>
        [HttpGet("porCategoria/{categoriaId}")]
        public async Task<ActionResult<IEnumerable<Gastos>>> GetByCategoriaId(int categoriaId)
        {
            var gastos = await _gastoService.GetByCategoriaIdAsync(categoriaId);
            return Ok(gastos);
        }

        /// <summary>
        /// Obtiene los gastos realizados en un período específico
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio del período</param>
        /// <param name="fechaFin">Fecha de fin del período</param>
        /// <returns>Lista de gastos en el período</returns>
        [HttpGet("porPeriodo")]
        public async Task<ActionResult<IEnumerable<Gastos>>> GetByPeriodo([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
        {
            var gastos = await _gastoService.GetByPeriodoAsync(fechaInicio, fechaFin);
            return Ok(gastos);
        }

        /// <summary>
        /// Crea un nuevo gasto
        /// </summary>
        /// <param name="gasto">Datos del gasto a crear</param>
        /// <returns>Gasto creado</returns>
        [HttpPost]
        public async Task<ActionResult<Gastos>> Create([FromBody] Gastos gasto)
        {
            try
            {
                var gastoCreado = await _gastoService.CreateAsync(gasto);
                return CreatedAtAction(nameof(GetById), new { id = gastoCreado.Id }, gastoCreado);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Actualiza los datos de un gasto existente
        /// </summary>
        /// <param name="id">Identificador del gasto</param>
        /// <param name="gasto">Nuevos datos del gasto</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Gastos gasto)
        {
            try
            {
                var resultado = await _gastoService.UpdateAsync(id, gasto);
                if (!resultado)
                {
                    return NotFound($"Gasto con ID {id} no encontrado");
                }
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Elimina un gasto existente
        /// </summary>
        /// <param name="id">Identificador del gasto a eliminar</param>
        /// <returns>Resultado de la operación</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var resultado = await _gastoService.DeleteAsync(id);
            if (!resultado)
            {
                return NotFound($"Gasto con ID {id} no encontrado");
            }
            return NoContent();
        }
    }
}