using Microsoft.EntityFrameworkCore; 
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore;
public class PlayerService
    {
        private readonly PlayerRepository _repository;


    public PlayerService(PlayerRepository repository)
    {
            _repository = repository;
    }

    public async Task<List<Player>> GetAllPlayersAsync()
    {
        return await _repository.GetAllPlayersAsync();
    }

    public async Task<Player> GetPlayerByIdAsync(int id)
    {
      var player = await _repository.GetPlayerByIdAsync(id);

        if (player == null)
        {
            throw new KeyNotFoundException($"Player with ID {id} not found.");
        }

        return player;
    }

    public async Task<List<Player>> GetPlayersByClanIdAsync(int clanId) 
    {
        var players = await _repository.GetPlayersByClanIdAsync(clanId);
        if (players == null || players.Count == 0)
        {
            throw new KeyNotFoundException($"No players found for Clan ID {clanId}.");
        }
        return players;
    }


    public async Task<Player> AddPlayerAsync(Player player)
    {

        if(string.IsNullOrEmpty(player.Name))
        {
            throw new ArgumentException("Player name cannot be null or empty.");
        }
        
        if(player.Name.Length < 3 || player.Name.Length > 30)
        {
            throw new ArgumentException("Player name must be between 3 and 30 characters.");
        }

        bool playerExists = await _repository.PlayerExistByName(player.Name);
        if (playerExists)
        {
            throw new InvalidOperationException($"Player with name {player.Name} already exists.");
        }

        await _repository.AddPlayerAsync(player);
        return player;
    }

    public async Task<Player> UpdatePlayerAsync (Player player, int id)
    {
        var existingPlayer = await _repository.GetPlayerByIdAsync(id);
        if (existingPlayer == null)
        {
            throw new KeyNotFoundException($"Player with ID {id} not found.");
        }

        if (!string.IsNullOrEmpty(player.Name) && (player.Name.Length < 3 || player.Name.Length > 30))
        {
            throw new ArgumentException("Player name must be between 3 and 30 characters.");
        }
        await _repository.UpdatePlayerAsync(player, id);

        return await _repository.GetPlayerByIdAsync(id);
    }

    public async Task<bool> DeletePlayerAsync(int id)
    {
        var existingPlayer = await _repository.GetPlayerByIdAsync(id);

        if (existingPlayer == null)
        {
            return false;
        }
         await _repository.DeletePlayerAsync(id);
         return true;
    }
}

