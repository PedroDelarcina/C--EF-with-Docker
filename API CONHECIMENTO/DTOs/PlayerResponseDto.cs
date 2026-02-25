
    public class PlayerResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Reputation { get; set; }
        public decimal Kda { get; set; }
        public int ClanId { get; set; }
        public string? ClanName { get; set; }
        public List<WeaponResponseDto>? Weapons { get; set; } 
    }

