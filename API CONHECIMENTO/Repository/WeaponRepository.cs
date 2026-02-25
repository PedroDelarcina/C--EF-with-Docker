using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
 public class WeaponRepository
 {
    private readonly InfestationNewzDbContext _context;

    public WeaponRepository(InfestationNewzDbContext context)
    {
        _context = context;
    }

    public async Task<List<Weapons>> GetAllWeaponsAsync()
    {
       return await _context.Weapons.ToListAsync();
    }

    public async Task<Weapons> GetWeaponsById(int id) 
    {
        return await _context.Weapons.FirstOrDefaultAsync(w => w.Id == id);
    }
    public async Task AddWeaponsAsync( Weapons weapon)
    {
        _context.Weapons.Add(weapon);
        await _context.SaveChangesAsync();
    }



}

