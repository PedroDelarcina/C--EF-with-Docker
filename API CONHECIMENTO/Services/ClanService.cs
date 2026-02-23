using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ClanService
    {
    private readonly ClanRepository _clanRepository;

    public ClanService(ClanRepository clanRepository)
    {
        _clanRepository = clanRepository;
    }

    public async Task<List<Clan>> GetAllClansAsync()
    {
        return await _clanRepository.GetAllClansAsync();
    }

    public async Task<Clan> GetClanByIdAsync(int id)
    {
        var clan = await _clanRepository.GetClanById(id);
        if (clan == null)
        {
            throw new KeyNotFoundException($"Clan with ID {id} not found.");
        }
        return clan;
    }

    public async Task<Clan> AddClanAsync (Clan clan)
    {
        if(string.IsNullOrWhiteSpace(clan.Name))
        {
            throw new ArgumentException("Clan name cannot be empty.");
        }   

        if(clan.Name.Length < 5 || clan.Name.Length > 50)
        {
            throw new ArgumentException("Clan name must be between 5 and 50 characters.");
        }

        await _clanRepository.AddClanAsync(clan);
        return clan;
    }

    public async Task<Clan> UpdateClanAsync ( Clan clan , int id)
    {
        var updateClan = await _clanRepository.GetClanById(id);
        if (updateClan == null)
        {
            throw new KeyNotFoundException($"Clan with ID {id} not found.");
        }
        
        if(string.IsNullOrWhiteSpace(clan.Name))
        {
            throw new ArgumentException("Clan name cannot be empty.");
        }

        if(clan.Name.Length < 5 || clan.Name.Length > 50)
        {
            throw new ArgumentException("Clan name must be between 5 and 50 characters.");
        }

        bool existsClan = await _clanRepository.ClanExistByName(clan.Name);
        if (existsClan)
        {
            throw new ArgumentException($"Clan with name {clan.Name} already exists.");
        }

        await _clanRepository.UpdateClanAsync(clan, id);
        return clan;
    }

    public async Task<bool> DeleteClanAsync (int id)
    {
        var deleteClan = await _clanRepository.GetClanById(id);
        if (deleteClan == null)
        {
            return false;
        }
        await _clanRepository.DeleteClanAsync(id);
        return true;
    }
}

