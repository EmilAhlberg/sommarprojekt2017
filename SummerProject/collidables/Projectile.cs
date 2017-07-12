using System;
using Microsoft.Xna.Framework;
using SummerProject.achievements;

namespace SummerProject.collidables
{
   public abstract class Projectile : ActivatableEntity
    {
        protected Timer despawnTimer;
        protected float despawnTime = 7f; //!!
        public bool IsEvil;

        public Projectile(Vector2 position, bool isEvil, IDs id = IDs.DEFAULT) : base(position, id)
        {
            IsEvil = isEvil;
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
                if (IsEvil)
                    (c2 as Player).Health -= Damage;
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
        }

        protected void ResetSpawnTime()
        {
            despawnTimer.Reset();
        }
    }
}
