using Microsoft.Xna.Framework;

namespace SummerProject
{
    public interface ICollidable
    {
        Rectangle BoundBox { get; set; }
        bool IsStatic { get; set; }
        Vector2 Position { get; set; }
        Vector2 PrevPos { get; set; }
    }
}