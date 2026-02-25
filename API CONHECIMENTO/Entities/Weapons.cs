
    public class Weapons
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public WeaponType Type { get; set; }
        public int Damage { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
}

