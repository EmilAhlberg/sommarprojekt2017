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
        private static Vector2 offset;
        static Timer shakeTimer = new Timer(0.2f);
        private static float multiplier;
        private const int MAXMULT = 7;

        public static Matrix CameraMatrix {
            get
            {
                return Matrix.CreateTranslation(new Vector3(-CameraPosition, 0));
            }
        }
        public static Vector2 CameraPosition { get { if (Player != null) return Player.Position - new Vector2(WindowSize.Width * 0.5f, WindowSize.Height * 0.5f) + offset; else return Vector2.Zero + offset; } }
        public static Player Player { private get; set; }

        public static void Update(GameTime gameTime)
        {
            if (!shakeTimer.IsFinished)
            {
                shakeTimer.CountDown(gameTime);
                offset = new Vector2(1 - SRandom.NextFloat() * 2, 1 - SRandom.NextFloat()*2)*multiplier;
                if (shakeTimer.IsFinished)
                    offset = Vector2.Zero;
            }
        }

        internal static void Shake(float damage)
        {
            multiplier = damage * MAXMULT;
            shakeTimer.Reset();
        }
    }
}
