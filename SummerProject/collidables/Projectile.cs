using System;
using Microsoft.Xna.Framework;
using SummerProject.achievements;

namespace SummerProject.collidables
{
   public abstract class Projectile : ActivatableEntity
    {
        protected Timer despawnTimer;
        protected float despawnTime = 7f; //!!
        private bool isEvil;
        public bool IsEvil { get { return isEvil; } set { Sprite.IsEvil = value; isEvil = value; } }

        public Projectile(Vector2 position, IDs id = IDs.DEFAULT) : base(position, id)
        {
            despawnTimer = new Timer(despawnTime);
        }

        protected void UpdateTimer(GameTime gameTime)
        {
            if (IsActive)
            {
                despawnTimer.CountDown(gameTime);
                if (despawnTimer.IsFinished || WindowSize.IsOutOfBounds(Position))
                    Death();
            }
        }

        protected override void HandleCollision(ICollidable c2)
        {
            if (c2 is Player)
            {
                Player p = (c2 as Player);
                if (IsEvil && !p.phaseOut)
                    p.Health -= Damage;
            }

            if (c2 is Enemy)
            {
                if (!IsEvil)
                {
                    Enemy e = c2 as Enemy;
                    e.Health -= Damage;
                    e.AddForce(Velocity*Velocity.Length()/2*Mass/e.Hull.Mass); //! remove lator
                    Traits.SHOTSHIT.Counter++;
                }
            }
            if (Health <= 0)
                Death();
        }

        protected void ResetSpawnTime()
        {
            despawnTimer.Reset();
        }
    }
}
