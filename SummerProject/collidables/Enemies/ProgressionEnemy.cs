using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SummerProject.wave;
using SummerProject.collidables.parts;

namespace SummerProject.collidables.enemies
{
    class ProgressionEnemy : Attacker
    {
        public ProgressionEnemy(Vector2 position, Player player, IDs id = IDs.DEFAULT) : base(position, player, id)
        {
        }

        public void FillParts()
        {
            Hull = new RectangularHull(IDs.DEFAULT_ENEMY);
            int level = GameMode.Level;
            switch (level % 10)
            {
                case 1: Hull.AddPart(new EnginePart(), 3); break;
                case 2: Hull.AddPart(new EnginePart(), 3); break;
                case 3: Hull.AddPart(new EnginePart(), 2); Hull.AddPart(new EnginePart(), 0); break;
                case 4: Hull.AddPart(new EnginePart(), 2); Hull.AddPart(new EnginePart(), 0); break;
                case 5: break;
                case 6: Hull.AddPart(new EnginePart(), 0); Hull.AddPart(new EnginePart(), 1); Hull.AddPart(new EnginePart(), 2); Hull.AddPart(new EnginePart(), 3); break;
                case 7: Hull.AddPart(new EnginePart(), 0); Hull.AddPart(new EnginePart(), 1); Hull.AddPart(new EnginePart(), 2); Hull.AddPart(new EnginePart(), 3); break;
                case 8: Hull.AddPart(new EnginePart(), 3); break;
                case 9:
                    Random r = new Random();
                    int n = r.Next(0, 100);
                    if (n < 50)
                    {
                        Hull.AddPart(new EnginePart(), 3); break;
                    }
                    if (n < 60)
                    {
                        Hull.AddPart(new EnginePart(), 2); Hull.AddPart(new EnginePart(), 0); break;
                    }
                    Hull.AddPart(new EnginePart(), 0); Hull.AddPart(new EnginePart(), 1); Hull.AddPart(new EnginePart(), 2); Hull.AddPart(new EnginePart(), 3); break;
                case 0: break;
                default: throw new Exception();
            }
            //e = new RandomEnemy(FarAway(), player); return e;
            //e = new StandardEnemy(FarAway(),  player); e.AddPart(new EnginePart(), 3); return e;
        }

        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            base.SpecificActivation(source, target);
            FillParts();
        }

        public override void Move()
        {
            Hull.TakeAction(typeof (EnginePart));
        }

        protected override void Attack(GameTime gameTime)
        {
            Move();
        }

        protected override void Wait(GameTime gameTime)
        {
            Move();
        }
    }
}
