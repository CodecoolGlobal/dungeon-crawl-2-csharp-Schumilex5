namespace DungeonCrawl.Actors.Items
{
    public class Potion : Item
    {
        private int _durability = 1;
        public override int DefaultSpriteId => 896;
        public override string DefaultName => "Potion";
        
        public Potion() : base(1,0 )
        {
            
        }
    }
}