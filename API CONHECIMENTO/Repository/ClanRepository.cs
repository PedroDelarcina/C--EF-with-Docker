using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ClanRepository
  {
    private readonly InfestationNewzDbContext _context;

    public ClanRepository(InfestationNewzDbContext context)
    {
        _context = context;
    }

    public async Task<List<Clan>> GetAllClansAsync()
    {
       return await _context.Clans.ToListAsync();
    }

    public async Task<Clan> GetClanById (int id)
    {
        return await _context.Clans.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<bool> ClanExistByName(string name)
    {
        return await _context.Clans.AnyAsync(x => x.Name == name);
    }

    public async Task AddClanAsync (Clan clan)
    {
        _context.Clans.Add(clan);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateClanAsync(Clan clan, int id) 
    {
        var clanUpdate = await _context.Clans.FindAsync(id);

        clanUpdate.Name = clan.Name;
        clanUpdate.Description = clan.Description;
        clanUpdate.MembersCount = clan.MembersCount;

        _context.Clans.Update(clanUpdate);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteClanAsync(int id)
    {
        var clan = await _context.Clans.FindAsync(id);
        _context.Clans.Remove(clan);
        await _context.SaveChangesAsync();
    }

}

