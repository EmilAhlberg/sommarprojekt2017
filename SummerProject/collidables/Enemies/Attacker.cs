using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SummerProject.collidables.parts;

namespace SummerProject.collidables.enemies
{
    public abstract class Attacker : Enemy
    {
        protected const float WAITTIME = 0.4f;
        protected const float ATTACKTIME = 0.4f;
        protected Timer waitTimer;
        protected Timer attackTimer;

        public Attacker(Vector2 position, Player player, IDs id = IDs.DEFAULT) : base(position, player, id)
        {
            attackTimer = new Timer(ATTACKTIME);
            waitTimer = new Timer(WAITTIME);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (waitTimer.IsFinished)
            {
                Attack(gameTime);
                attackTimer.CountDown(gameTime);
                if (attackTimer.IsFinished)
                {
                    attackTimer.Reset();
                    waitTimer.Reset();
                }
            }
            else
            {
                waitTimer.CountDown(gameTime);
                Wait(gameTime);
            }
        }

        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            base.SpecificActivation(source, target);
            attackTimer = new Timer(ATTACKTIME);
            waitTimer = new Timer(WAITTIME);
        }
        protected abstract void Wait(GameTime gameTime);
        protected abstract void Attack(GameTime gameTime);
    }
}
