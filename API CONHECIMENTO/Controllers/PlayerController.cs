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
    public async Task<ActionResult<List<PlayerListItemDto>>> GetAllPlayers()
    {
        var players = await _service.GetAllPlayersAsync();

        var response = players.Select(p => new PlayerListItemDto
        {
            Id = p.Id,
            Name = p.Name,
            Kda = p.Kda,
            Reputation = p.Reputation,
            ClanName = p.Clan?.Name,
            WeaponsCount = p.Weapons?.Count ?? 0,
        }).ToList();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PlayerResponseDto>> GetPlayerById(int id)
    {
        try
        {
            var player = await _service.GetPlayerByIdAsync(id);

            var response = new PlayerResponseDto
            {
                Id = player.Id,
                Name = player.Name,
                Reputation = player.Reputation,
                Kda = player.Kda,
                ClanId = player.ClanId,
                ClanName = player.Clan?.Name,
                Weapons = player.Weapons?.Select(w => new WeaponResponseDto
                {
                    Id = w.Id,
                    Name = w.Name,
                    Type = w.Type,
                    TypeName = w.Type.ToString(),
                    Damage = w.Damage
                }).ToList()

            };

            return Ok(response);
        }
        catch (KeyNotFoundException ex )
        {
            return NotFound("Player not found.");
        }
    }

    [HttpGet("clan/{clanId}")]
    public async Task<ActionResult<List<Player>>> GetPlayersByClanId(int clanId)
    {
        try
        {
            var players = await _service.GetPlayersByClanIdAsync(clanId);
            return Ok(players);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound("No players found for the specified Clan ID.");
        }
    }

    [HttpPost]
    public async Task<ActionResult<PlayerResponseDto>> AddPlayer (CreatePlayerDto playerDto)
    {
        var player = new Player
        {
            Name = playerDto.Name,
            Reputation = playerDto.Reputation,
            Kda = playerDto.Kda,
            ClanId = playerDto.ClanId
        };

        var created = await _service.AddPlayerAsync(player);

        var response = await GetPlayerById(created.Id);

        return CreatedAtAction(nameof(GetPlayerById), new { id = created.Id }, response.Value);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Player>> UpdatePlayer (CreatePlayerDto createPlayerDto, int id)
    {
        var player = new Player
        {
            Name = createPlayerDto.Name,
            Reputation = createPlayerDto.Reputation,
            Kda = createPlayerDto.Kda,
            ClanId = createPlayerDto.ClanId
        };

        var updatedPlayer = await _service.UpdatePlayerAsync(player, id);

        var response = await GetPlayerById(updatedPlayer.Id);

        return Ok(response.Value);

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

