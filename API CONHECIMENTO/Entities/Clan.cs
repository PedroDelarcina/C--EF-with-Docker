     public class Clan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int MembersCount { get; set; }

        public ICollection<Player> Players { get; set; } = new List<Player>();
}

