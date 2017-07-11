using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SummerProject.collidables.parts;
using SummerProject.collidables.enemies;

namespace SummerProject.collidables.Enemies
{
    class Shooter : Attacker
    {
        public Shooter(Vector2 position, Player player, IDs id = IDs.DEFAULT) : base(position, player, id)
        {
            attackTimer = new Timer(0.4f); //!
            waitTimer = new Timer(1f);
        }

        protected override void AI(GameTime gameTime)
        {
            CalculateAngle();
            ThrusterAngle = Angle;
            base.AI(gameTime);
        }

        protected override void Attack(GameTime gameTime)
        {
            Hull.TakeAction(typeof(SprayGunPart));
        }

        protected override void Wait(GameTime gameTime)
        {   
            Move();
        }
    }
}
