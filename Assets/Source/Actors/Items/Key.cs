namespace DungeonCrawl.Actors.Items
{
    public class Key : Item
    {
        public override int DefaultSpriteId => 559;
        public override string DefaultName => "Key";
        public override float MovementSpeed { get; set; } = 150;
        public override float MovementCount { get; set; } = 0;

        public Key() : base(1, 0)
        {
            
        }
    }
}