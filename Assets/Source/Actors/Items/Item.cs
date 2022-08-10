namespace DungeonCrawl.Actors.Items
{
    public abstract class Item : Actor
    {
        public int _durability;
        
        public virtual int GetDurability()
        {
            return this._durability;
        }

        public virtual void ChangeDurability(int value)
        {
            this._durability += value;
        }
    }
}