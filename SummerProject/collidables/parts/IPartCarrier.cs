using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.collidables.parts
{
    public interface IPartCarrier
    {
        void Update(GameTime gameTime);
        List<Part> Parts { get; }
        bool AddPart(Part part, int pos);
        void Collision(ICollidable c2);
        bool IsEvil { get; }
    }
}
