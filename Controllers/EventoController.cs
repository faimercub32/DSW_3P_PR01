using Microsoft.AspNetCore.Mvc;
using DSW_3P_PR01.Services;
using DSW_3P_PR01.Models;

namespace DSW_3P_PR01.Controllers;

[ApiController]
[Route("api/evento")]
public class EventoController : ControllerBase
{
    private readonly IEventoService _service;

    public EventoController(IEventoService service)
    {
        _service = service;
    }

    //GET - Obtener todas los eventos
    [HttpGet]
    [ProducesResponseType(typeof(List<Evento>),200)]
    public IActionResult ObtenerTodas()
    {
        return Ok(_service.ObtenerTodas());

    }

    //GET - Obtener evento por ID
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Evento), 200)]
    [ProducesResponseType(404)]
    public IActionResult ObtenerPorId(int id)
    {
        var Evento = _service.ObtenerPorId(id);

        if(Evento == null)
        {
            return NotFound();
        }
        return Ok(Evento);
    }

    //POST - Agregar Evento
    [HttpPost]
    [ProducesResponseType(typeof(Evento), 201)]
    [ProducesResponseType(400)]
    public IActionResult Agregar([FromBody] Evento evento)
    {
        try
        {
            var creada = _service.Agregar(evento);

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new {id = creada.Id},
                creada
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new {mensaje = ex.Message});
        }
    }

    //PUT - Actualizar Evento
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public IActionResult Actualizar(int id, [FromBody] Evento evento)
    {
        try
        {
            if (!_service.Actualizar(id, evento))
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new {mensaje = ex.Message});
        }
    }

    //DELETE - Eliminar Evento
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult Eliminar(int id)
    {
        if (!_service.Eliminar(id))
        {
            return NotFound();
        }
        return NoContent();
    }
}