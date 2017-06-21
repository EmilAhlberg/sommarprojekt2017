using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SummerProject.collidables;

namespace SummerProject.factories
{
    class Drops : Entities
    {
        public Drops(List<Sprite> sprites, int entityCap, float eventTime) : base(sprites, entityCap, eventTime)
        {
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }

        protected override AIEntity CreateEntity(int index)
        {
            throw new NotImplementedException();
        }
    }
}
