using Microsoft.AspNetCore.Mvc;
using ToyStore.Application.Interfaces;
using ToyStore.Domain.Entities;

[Route("api/[controller]")]
[ApiController]
public class ToyController : ControllerBase
{
    private readonly IToyService _service;

    public ToyController(IToyService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<Toy>> GetToys()
    {
        return await _service.GetAllToysWithStoresAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Toy>> GetToy(int id)
    {
        var toy = await _service.GetToyDetailsAsync(id);
        if (toy == null)
        {
            return NotFound(); 
        }
        return toy;
    }

    [HttpPost]
    public async Task<ActionResult<Toy>> CreateToy([FromBody] Toy toy)
    {
        await _service.AddToyAsync(toy);
        return CreatedAtAction(nameof(GetToy), new { id = toy.Id }, toy); 
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateToy(int id, [FromBody] Toy toy)
    {
        if (id != toy.Id)
        {
            return BadRequest();
        }

        await _service.UpdateToyAsync(toy);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteToy(int id)
    {
        var toy = await _service.GetAllToysWithStoresAsync();
        if (toy == null)
        {
            return NotFound();
        }

        await _service.DeleteToyAsync(id);
        return NoContent(); 
    }
}
