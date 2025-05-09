using Microsoft.AspNetCore.Mvc;
using SggApp.BLL.Interfaces;
using SggApp.DAL.Entidades;

namespace SggApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonedasController : ControllerBase
    {
        private readonly IMonedaService _monedaService;

        public MonedasController(IMonedaService monedaService)
        {
            _monedaService = monedaService;
        }

        /// <summary>
        /// Obtiene todas las monedas
        /// </summary>
        /// <returns>Lista de monedas</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Monedas>>> GetAll()
        {
            var monedas = await _monedaService.GetAllAsync();
            return Ok(monedas);
        }

        /// <summary>
        /// Obtiene una moneda por su identificador
        /// </summary>
        /// <param name="id">Identificador de la moneda</param>
        /// <returns>Moneda encontrada</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Monedas>> GetById(int id)
        {
            var moneda = await _monedaService.GetByIdAsync(id);
            if (moneda == null)
            {
                return NotFound($"Moneda con ID {id} no encontrada");
            }
            return Ok(moneda);
        }

        /// <summary>
        /// Obtiene una moneda por su código ISO
        /// </summary>
        /// <param name="codigo">Código ISO de la moneda</param>
        /// <returns>Moneda encontrada</returns>
        [HttpGet("porCodigo/{codigo}")]
        public async Task<ActionResult<Monedas>> GetByCodigo(string codigo)
        {
            var moneda = await _monedaService.GetByCodigoAsync(codigo);
            if (moneda == null)
            {
                return NotFound($"Moneda con código {codigo} no encontrada");
            }
            return Ok(moneda);
        }

        /// <summary>
        /// Crea una nueva moneda
        /// </summary>
        /// <param name="moneda">Datos de la moneda a crear</param>
        /// <returns>Moneda creada</returns>
        [HttpPost]
        public async Task<ActionResult<Monedas>> Create([FromBody] Monedas moneda)
        {
            try
            {
                var monedaCreada = await _monedaService.CreateAsync(moneda);
                return CreatedAtAction(nameof(GetById), new { id = monedaCreada.Id }, monedaCreada);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Actualiza los datos de una moneda existente
        /// </summary>
        /// <param name="id">Identificador de la moneda</param>
        /// <param name="moneda">Nuevos datos de la moneda</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Monedas moneda)
        {
            try
            {
                var resultado = await _monedaService.UpdateAsync(id, moneda);
                if (!resultado)
                {
                    return NotFound($"Moneda con ID {id} no encontrada");
                }
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Elimina una moneda existente
        /// </summary>
        /// <param name="id">Identificador de la moneda a eliminar</param>
        /// <returns>Resultado de la operación</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var resultado = await _monedaService.DeleteAsync(id);
                if (!resultado)
                {
                    return NotFound($"Moneda con ID {id} no encontrada");
                }
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}