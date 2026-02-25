using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;


public class PlayerRepository
    {
    private readonly InfestationNewzDbContext _context;

    public PlayerRepository(InfestationNewzDbContext context)
    {
        _context = context;
    }

    public async Task<List<Player>> GetAllPlayersAsync()
    {
       return await _context.Players.ToListAsync();
    }

    public async Task<Player> GetPlayerByIdAsync(int id)
    {
        return await _context.Players.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Player>> GetPlayersByClanIdAsync(int clanId)
    {
        return await _context.Players.Where(x => x.ClanId == clanId).ToListAsync();
    }

    public async Task<bool> PlayerExistByName(string name)
    {
        return await _context.Players.AnyAsync(x => x.Name == name);
    }
    public async Task AddPlayerAsync (Player player)
    {
        _context.Players.Add(player);
        await _context.SaveChangesAsync();
    }   
    public async Task UpdatePlayerAsync (Player player, int id)
    {
        var existingPlayer = await _context.Players.FindAsync(id);

        existingPlayer.Name = player.Name;
        existingPlayer.Reputation = player.Reputation;
        existingPlayer.Kda = player.Kda;
        existingPlayer.Clan = player.Clan;

        _context.Players.Update(existingPlayer);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePlayerAsync(int id)
    {
        var player = await _context.Players.FindAsync(id);

        _context.Players.Remove(player);
        await _context.SaveChangesAsync();
    }
}
