namespace DungeonCrawl.Actors.Items
{
    public class Crown : Item
    {
        public override int DefaultSpriteId => 139;
        public override string DefaultName => "Crown";
        public override float MovementSpeed { get; set; } = 150;
        public override float MovementCount { get; set; } = 0;

        public Crown() : base(0, 0)
        {
        
        }
        
    }
}