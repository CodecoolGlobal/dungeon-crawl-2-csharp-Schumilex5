namespace DungeonCrawl.Actors.Static
{
    public class Floor : Actor
    {
        public override int DefaultSpriteId => 1;
        public override string DefaultName => "Floor";
        public override float MovementSpeed { get; set; } = 150;
        public override float MovementCount { get; set; } = 0;

        public override bool Detectable => false;
    }
}
