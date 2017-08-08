using System;
using Microsoft.Xna.Framework;
using SummerProject.achievements;
using SummerProject.wave;
using SummerProject.collidables.bullets;

namespace SummerProject.collidables
{
   public abstract class Projectile : ActivatableEntity
    {
        protected Timer despawnTimer;
        protected float despawnTime = 7f; //!!
        private bool isEvil;
        private Enemy prevC2;
        private Timer hitMeBbyOneMoreTime;
        private float chargingBulletHitAgainDelay = 0.05f;
        public bool IsEvil { get { return isEvil; } set { Sprite.IsEvil = value; isEvil = value; } }

        public Projectile(Vector2 position, IDs id = IDs.DEFAULT) : base(position, id)
        {
            despawnTimer = new Timer(despawnTime);
            hitMeBbyOneMoreTime = new Timer(chargingBulletHitAgainDelay);
        }

        protected void UpdateTimer(GameTime gameTime)
        {
            if (IsActive)
            {
                hitMeBbyOneMoreTime.CountDown(gameTime);
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
                if (IsEvil)
                    p.Health -= Damage * Difficulty.ENEMY_BULLETDAMAGEFACTOR;///5; ///!! times FACTOR
            }

            if (c2 is Enemy)
            {
                if (!IsEvil)
                {
                    if (this.id == IDs.CHARGINGBULLET)
                    {
                        if (c2 != prevC2 || hitMeBbyOneMoreTime.IsFinished)
                        {
                            Enemy e = c2 as Enemy;
                            e.Health -= Damage;
                            Traits.SHOTSHIT.Counter++;
                            prevC2 = e;
                            hitMeBbyOneMoreTime.Reset();
                        }
                    }
                    else
                    {
                        Enemy e = c2 as Enemy;
                        e.Health -= Damage;
                        e.AddForce(Velocity * Velocity.Length() / 2 * Mass / e.Hull.Mass); //! remove lator
                        Traits.SHOTSHIT.Counter++;
                    }
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
