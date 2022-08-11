namespace DungeonCrawl.Actors.Items
{
    public abstract class Item : Actor
    {
        private int _durability;
        private int _bonusDamage;

        public Item(int durability, int bonusDamage)
        {
            _durability = durability;
            _bonusDamage = bonusDamage;
        }
        
        public virtual int GetDurability()
        {
            return _durability;
        }
        public virtual int GetBonusDmg()
        {
            return _bonusDamage;
        }

        public virtual void ChangeDurability(int value)
        {
            _durability += value;
        }
    }
}