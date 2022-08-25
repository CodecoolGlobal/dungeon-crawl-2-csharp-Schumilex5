namespace DungeonCrawl.Actors.Items
{
    public class Sword : Item
    {
        public override int DefaultSpriteId => 415;
        public override string DefaultName => "Sword";
        public override float MovementSpeed { get; set; } = 150;
        public override float MovementCount { get; set; } = 0;

        public Sword() : base(6, 5, -2)
        {
            
        }
        
    }
}