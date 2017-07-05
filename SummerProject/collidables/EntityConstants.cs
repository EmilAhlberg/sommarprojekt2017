using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.collidables
{
    class EntityConstants
    {

        public static readonly Dictionary<int, int> HEALTH =
          new Dictionary<int, int>
          {
                {(int)IDs.DEFAULT, 5},
                {(int)IDs.PLAYER, 5},
                {(int)IDs.DEFAULT_ENEMY, 1},
                {(int)IDs.DEFAULT_BULLET, 1}
          };

        public static readonly Dictionary<int, int> DAMAGE =
          new Dictionary<int, int>
          {
                {(int)IDs.DEFAULT, 1},
                {(int)IDs.PLAYER, 1},
                {(int)IDs.DEFAULT_ENEMY, 1},
                {(int)IDs.DEFAULT_BULLET, 1}
          };

        public static readonly Dictionary<int, float> MASS =
          new Dictionary<int, float>
          {
                {(int)IDs.DEFAULT, 10},
                {(int)IDs.PLAYER, 5},
                {(int)IDs.DEFAULT_ENEMY, 10},
                {(int)IDs.DEFAULT_BULLET, 10f}
          };

        public static readonly Dictionary<int, float> THRUST =
         new Dictionary<int, float>
         {
                {(int)IDs.DEFAULT, 10},
                {(int)IDs.PLAYER, 5/3f},
                {(int)IDs.DEFAULT_ENEMY, 3},
                {(int)IDs.DEFAULT_BULLET, 0}
         };

        public static readonly Dictionary<int, float> TURNSPEED =
        new Dictionary<int, float>
        {
                {(int)IDs.DEFAULT, 1000f * (float)Math.PI }, //! //rad per tick
                {(int)IDs.PLAYER, 0.1f * (float)Math.PI},
                {(int)IDs.DEFAULT_ENEMY, 1000f * (float)Math.PI},
                {(int)IDs.DEFAULT_BULLET, 0},
                {(int)IDs.HOMINGBULLET, 0.07f * (float)Math.PI}
        };

        public static readonly Dictionary<int, float> FRICTION =
          new Dictionary<int, float>
          {
                {(int)IDs.DEFAULT, 100},
                {(int)IDs.PLAYER, 25},
                {(int)IDs.DEFAULT_ENEMY, 100},
                {(int)IDs.DEFAULT_BULLET, 0f},
                {(int)IDs.MINEBULLET, 200}
          };

        public static readonly Dictionary<int, int> SCORE =
        new Dictionary<int, int>
        {
                {(int)IDs.DEFAULT_ENEMY, 100},
        };
    }
}
