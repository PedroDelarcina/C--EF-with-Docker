using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ClanController : ControllerBase
{
    private readonly ClanService _service;

    public ClanController(ClanService clanService)
    {
        _service = clanService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Clan>>> GetAllClans()
    {
        var clans = await _service.GetAllClansAsync();
        return Ok(new {
         sucess = true,
         count = clans.Count,
         data = clans
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Clan>> GetClanById(int id)
    {
        var clan = await _service.GetClanByIdAsync (id);

        if (clan == null)
        {
            return NotFound();
        }
        return Ok(clan);
    }

    [HttpPost]
    public async Task<ActionResult<Clan>> AddClansAsync(Clan clan)
    {
        var createdClan = await _service.AddClanAsync(clan);
        return CreatedAtAction(nameof(GetClanById), new { id = createdClan.Id }, createdClan);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Clan>> UpdateClanAsync (Clan clan, int id)
    {
        var updatedClan = await _service.UpdateClanAsync(clan, id);
        return Ok(updatedClan);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteClanAsync (int id)
    {
        var deleteClan = await _service.DeleteClanAsync(id);

        if(!deleteClan)
        {
            return NotFound("Clan not found.");
        }
        return NoContent();
    }


}

