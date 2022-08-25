namespace DungeonCrawl.Actors.Static
{
    public class Wall : Actor
    {
        public override int DefaultSpriteId => 825;
        public override string DefaultName => "Wall";
        public override float MovementSpeed { get; set; } = 150;
        public override float MovementCount { get; set; } = 0;
    }
}
