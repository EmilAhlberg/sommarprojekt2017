﻿using Microsoft.Xna.Framework;

namespace SummerProject.collidables
{
    public abstract class Entity : Collidable
    {
        public float Damage { get; set; }
        public float Health { get; set; }
        public bool IsActive { get; set; }


        public Entity(Vector2 position, IDs id = IDs.DEFAULT) : base(position, id)
        {
        }

        public override void SetStats(IDs id)
        {
            Damage = EntityConstants.GetStatsFromID(EntityConstants.DAMAGE, id);
            Health = EntityConstants.GetStatsFromID(EntityConstants.HEALTH, id);
            base.SetStats(id);
        }

        public abstract void Death();
        
    }
}
