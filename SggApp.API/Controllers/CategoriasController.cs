using Microsoft.AspNetCore.Mvc;
using SggApp.BLL.Interfaces;
using SggApp.DAL.Entidades;

namespace SggApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        /// <summary>
        /// Obtiene todas las categorías
        /// </summary>
        /// <returns>Lista de categorías</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categorias>>> GetAll()
        {
            var categorias = await _categoriaService.GetAllAsync();
            return Ok(categorias);
        }

        /// <summary>
        /// Obtiene una categoría por su identificador
        /// </summary>
        /// <param name="id">Identificador de la categoría</param>
        /// <returns>Categoría encontrada</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Categorias>> GetById(int id)
        {
            var categoria = await _categoriaService.GetByIdAsync(id);
            if (categoria == null)
            {
                return NotFound($"Categoría con ID {id} no encontrada");
            }
            return Ok(categoria);
        }

        /// <summary>
        /// Crea una nueva categoría
        /// </summary>
        /// <param name="categoria">Datos de la categoría a crear</param>
        /// <returns>Categoría creada</returns>
        [HttpPost]
        public async Task<ActionResult<Categorias>> Create([FromBody] Categorias categoria)
        {
            try
            {
                var categoriaCreada = await _categoriaService.CreateAsync(categoria);
                return CreatedAtAction(nameof(GetById), new { id = categoriaCreada.Id }, categoriaCreada);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Actualiza los datos de una categoría existente
        /// </summary>
        /// <param name="id">Identificador de la categoría</param>
        /// <param name="categoria">Nuevos datos de la categoría</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Categorias categoria)
        {
            try
            {
                var resultado = await _categoriaService.UpdateAsync(id, categoria);
                if (!resultado)
                {
                    return NotFound($"Categoría con ID {id} no encontrada");
                }
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Elimina una categoría existente
        /// </summary>
        /// <param name="id">Identificador de la categoría a eliminar</param>
        /// <returns>Resultado de la operación</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var resultado = await _categoriaService.DeleteAsync(id);
                if (!resultado)
                {
                    return NotFound($"Categoría con ID {id} no encontrada");
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