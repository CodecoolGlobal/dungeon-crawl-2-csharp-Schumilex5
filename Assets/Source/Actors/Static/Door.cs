namespace DungeonCrawl.Actors.Static
{
    public class Door : Actor
    {
        public override int DefaultSpriteId => 434;
        public override string DefaultName => "Door";
        public override float MovementSpeed { get; set; } = 150;
        public override float MovementCount { get; set; } = 0;

    }
}