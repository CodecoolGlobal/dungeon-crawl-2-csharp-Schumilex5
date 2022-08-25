namespace DungeonCrawl.Actors.Items
{
    public class Potion : Item
    {
        private int _durability = 1;
        public override int DefaultSpriteId => 896;
        public override string DefaultName => "Potion";
        public override float MovementSpeed { get; set; } = 150;
        public override float MovementCount { get; set; } = 0;

        public Potion() : base(1,0 )
        {
            
        }
    }
}