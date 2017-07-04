using Microsoft.Xna.Framework;
using SummerProject.collidables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    public static class Camera
    {
        public static Matrix CameraMatrix {
            get
            {
                return Matrix.CreateTranslation(-new Vector3(Player.Position, 0)) *
              Matrix.CreateTranslation(new Vector3(WindowSize.Width * 0.5f, WindowSize.Height * 0.5f, 0));
            }
        }
        public static Vector2 CameraPosition { get { return Player.Position - new Vector2(WindowSize.Width * 0.5f, WindowSize.Height * 0.5f); } }
        public static Player Player { private get; set; }

    }
}
