using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using System.Collections.Generic;
using System.Threading.Tasks;


[Route("api/[controller]")]
[ApiController]
public class PlayerController : ControllerBase
{
    private readonly PlayerService _service;


    public PlayerController(PlayerService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Player>>> GetAllPlayers()
    {
        var player = await _service.GetAllPlayersAsync();
        return Ok(player);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Player>> GetPlayerById (int id)
    {
        var player = await _service.GetPlayerByIdAsync(id);
        return Ok(player);
    }

    [HttpPost]
    public async Task<ActionResult<Player>> AddPlayer (Player player)
    {
        var playerAdded = await _service.AddPlayerAsync(player);
        return CreatedAtAction(nameof(GetPlayerById), new { id = playerAdded.Id }, playerAdded);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Player>> UpdatePlayer (Player player, int id)
    {
        var playerUpdate = await _service.UpdatePlayerAsync(player, id);
        return Ok(player);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePlayer (int id)
    {
        var deletePlayer = await _service.DeletePlayerAsync(id);

        if (!deletePlayer)
        {
            return NotFound("Player not found."); 
        }
        return NoContent();
    }

}

