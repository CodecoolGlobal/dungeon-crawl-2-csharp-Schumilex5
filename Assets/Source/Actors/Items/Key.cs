﻿namespace DungeonCrawl.Actors.Items
{
    public class Key : Item
    {
        public override int DefaultSpriteId => 559;
        public override string DefaultName => "Key";

        public Key() : base(1, 0)
        {
            
        }
    }
}