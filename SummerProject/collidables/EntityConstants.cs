﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.collidables
{
    class EntityConstants
    {
        public const int DEFAULT = 0;
        public const int PLAYER = 1;
        public const int ENEMY = 2;
        public const int BULLET = 3;

        public static readonly Dictionary<int, int> HEALTH =
          new Dictionary<int, int>
          {
                {DEFAULT, 10},
                {PLAYER, 10},
                {ENEMY, 10},
                {BULLET, 1}
          };

        public static readonly Dictionary<int, int> DAMAGE =
          new Dictionary<int, int>
          {
                {DEFAULT, 1},
                {PLAYER, 2},
                {ENEMY, 2},
                {BULLET, 10}
          };

        public static readonly Dictionary<int, float> MASS =
          new Dictionary<int, float>
          {
                {DEFAULT, 10},
                {PLAYER, 5},
                {ENEMY, 10},
                {BULLET, 0.05f}
          };

        public static readonly Dictionary<int, int> THRUST =
         new Dictionary<int, int>
         {
                {DEFAULT, 10},
                {PLAYER, 5}, 
                {ENEMY, 5},
                {BULLET, 0}
         };

        public static readonly Dictionary<int, float> TURNSPEED =
        new Dictionary<int, float>
        {
                {DEFAULT, 1000f * (float)Math.PI }, //! //rad per tick
                {PLAYER, 0.05f * (float)Math.PI},
                {ENEMY, 0},
                {BULLET, 0}
        };

        public static readonly Dictionary<int, int> SCORE =
        new Dictionary<int, int>
        {             
                {ENEMY, 100},                
        };        

    }
}
