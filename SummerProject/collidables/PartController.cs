using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.collidables;
using SummerProject.collidables.parts;
using SummerProject.factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    public abstract class PartController : IPartCarrier, IActivatable, ICollidable
    {
        public CompositePart Hull { get; set; } //!
        public bool IsBusy { get; set; }
        public Vector2 Position { get { return Hull.Position; } set { Hull.Position = value; } }
        public IEnumerable<Collidable> Collidables { get { return Parts; } }
        public List<Part> Parts { get { return Hull.Parts; } }
        public float Angle { set { Hull.Angle = value; } get { return Hull.Angle; } }
        public float ThrusterAngle { set { Hull.ThrusterAngle = value; } get { return Hull.ThrusterAngle; } }
        protected float TurnSpeed { set { Hull.TurnSpeed = value; } get { return Hull.TurnSpeed; } }
        public float friction { set { Hull.friction = value; } get { return Hull.friction; } }
        public bool IsActive { get; set; }
        public bool IsEvil { get; }
        private const float startingEnergy = 100f;//!
        public float maxEnergy { get; protected set; }
        protected const float maxEnergyCap = 300; //!
        private float energy;
        public float Energy
        {
            get { return energy; }
            set
            {
                if (maxEnergy < maxEnergyCap)
                    maxEnergy += value - energy;
                energy = maxEnergy;
            }
        }
        public float maxHealth { get; protected set; }
        protected const float maxHealthCap = 15; //!
        private float health;
        public float Health { get { return health; }
            set
            {
                if (health == maxHealth && maxHealth < maxHealthCap)
                    maxHealth++;
                health += value-health;
                if (health > maxHealthCap)
                    health = maxHealthCap;
                if (health > maxHealth)
                    maxHealth = Health;
            }
        }
        public float Damage { get; set; }
        private IDs id;

        public PartController(Vector2 position, bool isEvil, IDs id = IDs.DEFAULT)
        {
            IsEvil = isEvil;
            if (id == IDs.DEFAULT)
                id = EntityConstants.TypeToID(GetType());
            Hull = new RectangularHull(id);
            Hull.Carrier = this;
            Position = position;
            this.id = id;
            SetStats(id);
        }

        public void Collision(ICollidable c2)
        {
            if (IsActive)
                HandleCollision(c2);
        }

        protected abstract void HandleCollision(ICollidable c2);

        public virtual void SetStats(IDs id)
        {
            Energy = startingEnergy;
            maxEnergy = Energy;
            Health = EntityConstants.GetStatsFromID(EntityConstants.HEALTH, id);
            maxHealth = Health;
            Damage = EntityConstants.GetStatsFromID(EntityConstants.DAMAGE, id);
        }

        public virtual void Update(GameTime gameTime)
        {
            CalculateAngle();
            Move();
            Hull.Update(gameTime);
            if (Health <= 0 && IsActive)
                Death();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Hull.Draw(spriteBatch, gameTime);
        }

        protected abstract void CalculateAngle();
        public abstract void Move();

        public virtual void Death()
        {
            IsActive = false;
            Hull.Death();
            Stop();
            Position = EntityFactory.FarAway();
        }

        public bool AddPart(Part part, int pos)
        {
            return Hull.AddPart(part, pos);
        }

        public void Stop()
        {
            Hull.Stop();
        }

        public void AddSpeed(float speed, float angle)
        {
            Hull.AddSpeed(speed, angle);
        }

        public void AddForce(Vector2 appliedForce)
        {
            Hull.AddForce(appliedForce);
        }

        public void CalculateAngle(float dX, float dY)
        {
            Hull.CalculateAngle(dX, dY);
        }

        

        public void AddForce(float force, float angle) { Hull.AddForce(force * (new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)))); }

        public void Activate(Vector2 source, Vector2 target)
        {
            SetStats(id);
            Position = source;
            IsActive = true;
            maxEnergy = startingEnergy;
            maxHealth = Health;
            Energy = maxEnergy;
            SpecificActivation(source, target);
        }

        protected virtual void SpecificActivation(Vector2 scource, Vector2 target) { }
    }
}
