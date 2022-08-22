﻿namespace DungeonCrawl.Actors.Items
{
    public class Sword : Item
    {
        public override int DefaultSpriteId => 415;
        public override string DefaultName => "Sword";

        public Sword() : base(6, 5, -2)
        {
            
        }
        
    }
}