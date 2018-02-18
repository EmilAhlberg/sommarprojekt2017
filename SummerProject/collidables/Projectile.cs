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
        private String team;
        private Enemy prevC2;
        private Timer hitMeBbyOneMoreTime;
        private float chargingBulletHitAgainDelay = 0.05f;
        public String Team { get { return team; } set { Sprite.Team = value; team = value; } }

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
                if (Team == EntityConstants.GetStatsFromID(EntityConstants.TEAM, IDs.DEFAULT_ENEMY))
                {
                    p.Health -= Damage * Difficulty.ENEMY_BULLETDAMAGEFACTOR;///5; ///!! times FACTOR
                    Camera.Shake(Damage);
                }
            }

            if (c2 is Enemy)
            {
                if (Team != EntityConstants.GetStatsFromID(EntityConstants.TEAM, IDs.DEFAULT_ENEMY))
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
