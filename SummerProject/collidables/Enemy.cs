using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SummerProject.collidables;
using SummerProject.collidables.parts;

namespace SummerProject
{
    class Enemy : IPartCarrier
    {        
        public int WorthScore {get; private set;}
        private Player player;
        protected CompositePart Hull;

        public Enemy(Vector2 position, ISprite sprite, Player player)
        {
            this.player = player;            
            Damage = EntityConstants.DAMAGE[EntityConstants.ENEMY];
            WorthScore = EntityConstants.SCORE[EntityConstants.ENEMY];
            Hull = new RectangularHull(position, sprite, this);
            Hull.Thrust = EntityConstants.THRUST[EntityConstants.ENEMY];      
        }

        public override void Update(GameTime gameTime) //NEEDS FIX !!!TODO!!! Fix particles for parts
        {
            CalculateAngle();
            Hull.Move();
            //Particles.GenerateParticles(Position, 4, angle);
            if (Health < 1)
            {
                ScoreHandler.AddScore(WorthScore);
                Death();
            }    
        }

        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            Health = EntityConstants.HEALTH[EntityConstants.ENEMY];
        }

        private void CalculateAngle()
        {
            float dX = Hull.Position.X - player.Hull.Position.X;
            float dY = Hull.Position.Y - player.Hull.Position.Y;
            Hull.TurnTowardsVector(dX, dY);
        }   
       
        public void Collision(Collidable c2) //NEEDS FIX
        {
            if (c2 is Projectile)
            {
                Projectile b = c2 as Projectile;
                if (IsActive)
                    Health -= b.Damage;
            }
            if (c2 is Player)
                Death();
        }

        public override void Death() //NEEDS FIX
        {
            //Particles.GenerateParticles(Position, 2, angle); //Death animation
            base.Death();
        }

        public bool AddPart(Part part, int pos)
        {
            return Hull.AddPart(part, pos);
        }

        public List<Part> GetParts()
        {
            return Hull.GetParts();
        }
    }
}
