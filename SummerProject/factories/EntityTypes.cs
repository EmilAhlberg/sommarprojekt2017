using System.Collections.Generic;

namespace SummerProject.factories
{
    class EntityTypes
    {
        public const int BULLET = 0;
        public const int HOMINGBULLET = 1;
        public const int EVILBULLET = 100;

        public static readonly Dictionary<int, int> SPRITE =
  new Dictionary<int, int>
  {
                {BULLET, 0},
                {HOMINGBULLET, 1},
                {2,2},
                {EVILBULLET, 0},
  };
    }
}
