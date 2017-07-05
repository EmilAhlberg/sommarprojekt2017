using Microsoft.Xna.Framework;

namespace SummerProject.collidables
{
    public abstract class Entity : Collidable
    {
        public float Damage { get; set; }
        public float Health { get; set; }
        public bool IsActive { get; set; }


        public Entity(Vector2 position, ISprite sprite) : base(position, sprite)
        {
        }

        public abstract void Death();
        
    }
}
