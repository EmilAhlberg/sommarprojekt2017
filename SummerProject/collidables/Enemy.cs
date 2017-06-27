using System;
using Microsoft.Xna.Framework;
using SummerProject.collidables;
using SummerProject.collidables.parts;
using SummerProject.factories;
using SummerProject.wave;

namespace SummerProject
{
    class Enemy : AIEntity, IPartCarrier
    {        
        public int WorthScore {get; private set;}
        private Player player;
        public static Projectiles projectiles;
        public bool CanShoot { get; set; }
        protected CompositePart Hull;
        private Timer rageTimer;

        public Enemy(Vector2 position, ISprite sprite, Player player)
            : base(position, sprite)
        {
            this.player = player;            
            Damage = EntityConstants.DAMAGE[EntityConstants.ENEMY];
            Thrust = EntityConstants.THRUST[EntityConstants.ENEMY];
            WorthScore = EntityConstants.SCORE[EntityConstants.ENEMY];
            rageTimer = new Timer(15);
            //Hull = new RectangularHull(position, sprite);                
        }

        public override void Update(GameTime gameTime)
        {
            CalculateAngle();
            Move();
            rageTimer.CountDown(gameTime);
            if (rageTimer.IsFinished)
            {
                Enrage();
            }
            else
                Particles.GenerateParticles(Position, 4, angle);
            if (CanShoot && SRandom.Next(0, 100) < 1)
            {
                projectiles.EvilFire(Position, player.Position);
            }
            
            if (Health < 1)
            {
                ScoreHandler.AddScore(WorthScore);
                Death();
            }    
        }
        


        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            rageTimer.Reset();
            Thrust = EntityConstants.THRUST[EntityConstants.ENEMY];
            CanShoot = SRandom.Next(0, 5) == 0; //! 1/5th chance of being able to shoot
            if (CanShoot)
            {
                sprite.MColor = Color.Red;
            }
            else
                sprite.MColor = Color.White;
            Health = EntityConstants.HEALTH[EntityConstants.ENEMY];
        }

        private void Enrage()
        {
            Thrust = 5 * EntityConstants.THRUST[EntityConstants.ENEMY];
            Particles.GenerateParticles(Position, 10, angle);
            sprite.MColor = Color.Black;
        }

        private void CalculateAngle()
        {
            float dX = Position.X - player.Position.X;
            float dY = Position.Y - player.Position.Y;
            base.CalculateAngle(dX, dY);
        }   
       
        public override void Collision(Collidable c2)
        {
            if (c2 is Projectile)
            {
                Projectile b = c2 as Projectile;
                if (b.IsActive && !b.IsEvil)
                    Health -= b.Damage;
            }
            if (c2 is ExplosionDrop)
            {
                ExplosionDrop ed = c2 as ExplosionDrop;
                if (ed.IsActive)
                    Health -= ed.Damage;
            }
            if (c2 is Player)
                Death();
        }

        public override void Death()
        {
            Particles.GenerateParticles(Position, 2, angle); //Death animation
            DropSpawnPoints.DeathAt(Position);
            base.Death();
        }

        public bool AddPart(Part part, int pos)
        {
            throw new NotImplementedException();
        }
    }
}
