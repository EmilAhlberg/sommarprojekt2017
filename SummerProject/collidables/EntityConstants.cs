using SummerProject.collidables;
using SummerProject.collidables.bullets;
using SummerProject.collidables.Enemies;
using SummerProject.collidables.parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.collidables
{
    class EntityConstants
    {
        public static IDs TypeToID(Type t)
        {
            while (!TYPETOID.Keys.Contains(t))
            {
                if (t == typeof(object))
                    return IDs.DEFAULT;
                t = t.BaseType;
            }
            return TYPETOID[t];
        }

        public static readonly Dictionary<Type, IDs> TYPETOID =
            new Dictionary<Type, IDs>
            {
               {typeof(Player), IDs.PLAYER },
               {typeof(Enemy), IDs.DEFAULT_ENEMY },
               {typeof(Asteroid), IDs.ENEMYASTER },
               {typeof(Wall), IDs.WALL },
               #region Bullets
               {typeof(Bullet), IDs.DEFAULT_BULLET },
               {typeof(SprayBullet), IDs.SPRAYBULLET },
               {typeof(MineBullet), IDs.MINEBULLET },
               {typeof(ChargingBullet), IDs.CHARGINGBULLET },
                #endregion
                #region Particles
               {typeof(Particle), IDs.DEFAULT_PARTICLE },
                #endregion
                #region Drops
                {typeof(HealthDrop), IDs.HEALTHDROP},
                {typeof(ExplosionDrop), IDs.EXPLOSIONDROP },
                {typeof(EnergyDrop), IDs.ENERGYDROP },
                #endregion 
                #region Parts
                {typeof(RectangularHull), IDs.RECTHULLPART },
               {typeof(GunPart), IDs.GUNPART },
               {typeof(SprayGunPart), IDs.SPRAYGUNPART },
               {typeof(MineGunPart), IDs.MINEGUNPART },
               {typeof(ChargingGunPart), IDs.CHARGINGGUNPART },
               {typeof(EnginePart), IDs.ENGINEPART },
               {typeof(Collidable), IDs.DEFAULT }
               #endregion
            };

        public static readonly Dictionary<IDs, Type> IDTOTYPE = ReverseDic(TYPETOID);

        private static Dictionary<TValue, TKey> ReverseDic<TKey, TValue>(Dictionary<TKey, TValue> source)
        {
            var dictionary = new Dictionary<TValue, TKey>();
            foreach (var entry in source)
            {
                if (!dictionary.ContainsKey(entry.Value))
                    dictionary.Add(entry.Value, entry.Key);
            }
            return dictionary;
        }

        public static float GetStatsFromID(Dictionary<int, float> dic, IDs id)
        {
            while (!dic.Keys.Contains((int)id))
            {
                if (!IDTOTYPE.Keys.Contains(id))
                    return dic[(int)IDs.DEFAULT];
                id = TypeToID(IDTOTYPE[id].BaseType);
            }
            return dic[(int)id];
        }

        public static readonly Dictionary<int, float> HEALTH =
          new Dictionary<int, float>
          {
                {(int)IDs.DEFAULT, 5},
                {(int)IDs.PLAYER, 5},
                {(int)IDs.DEFAULT_ENEMY, 1},
                {(int)IDs.ENEMYASTER, 3},
                {(int)IDs.DEFAULT_BULLET, 1}
          };

        public static readonly Dictionary<int, float> DAMAGE =
          new Dictionary<int, float>
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
                {(int)IDs.HOMINGBULLET, 0.07f * (float)Math.PI}
        };

        public static readonly Dictionary<int, float> FRICTION =
          new Dictionary<int, float>
          {
                {(int)IDs.DEFAULT, 100},
                {(int)IDs.PLAYER, 25},
                {(int)IDs.DEFAULT_ENEMY, 100},
                {(int)IDs.ENEMYASTER, 0},
                {(int)IDs.DEFAULT_BULLET, 0f},
                {(int)IDs.MINEBULLET, 200}
          };

        public static readonly Dictionary<int, float> SCORE =
        new Dictionary<int, float>
        {
                {(int)IDs.DEFAULT, 1},
                {(int)IDs.DEFAULT_ENEMY, 100},
        };


    }
}
