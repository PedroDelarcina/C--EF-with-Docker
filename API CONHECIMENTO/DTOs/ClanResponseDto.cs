
    public class ClanResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MembersCount { get; set; }
        public int TotalPlayers { get; set; }
        public decimal AverageKda { get; set; }
        public List<PlayerListItemDto> TopPlayers { get; set; }
}

