
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Reputation { get; set; }
        public decimal Kda { get; set; }
        public Clan? Clan { get; set; }

        public int ClanId { get; set; }

        public ICollection<Weapons> Weapons { get; set; } = new List<Weapons>();
}

