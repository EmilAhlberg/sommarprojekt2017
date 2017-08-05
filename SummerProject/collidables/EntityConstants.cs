using SummerProject.collidables;
using SummerProject.collidables.bullets;
using SummerProject.collidables.Enemies;
using SummerProject.collidables.parts;
using SummerProject.collidables.parts.guns;
using SummerProject.wave;
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
               {typeof(Shooter), IDs.ENEMYSHOOT },
               {typeof(Wall), IDs.WALL },
                #region Bullets
               {typeof(Bullet), IDs.DEFAULT_BULLET },
               {typeof(SprayBullet), IDs.SPRAYBULLET },
               {typeof(MineBullet), IDs.MINEBULLET },
               {typeof(ChargingBullet), IDs.CHARGINGBULLET },
               {typeof(GravityBullet), IDs.GRAVITYBULLET },
                #endregion
                #region Particles
               {typeof(Particle), IDs.DEFAULT_PARTICLE },
                #endregion
                #region Drops
                {typeof(HealthDrop), IDs.HEALTHDROP},
                {typeof(ExplosionDrop), IDs.EXPLOSIONDROP },
                {typeof(EnergyDrop), IDs.ENERGYDROP },
                {typeof(MoneyDrop), IDs.MONEYDROP },
                {typeof(Drop), IDs.DEFAULT_DROP},
                #endregion 
                #region Parts
               {typeof(Part), IDs.DEFAULT_PART },
               {typeof(RectangularHull), IDs.RECTHULLPART },
               {typeof(GunPart), IDs.GUNPART },
               {typeof(SprayGunPart), IDs.SPRAYGUNPART },
               {typeof(MineGunPart), IDs.MINEGUNPART },
               {typeof(ChargingGunPart), IDs.CHARGINGGUNPART },
               {typeof(GravityGunPart), IDs.GRAVITYGUNPART },
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

        public static T GetStatsFromID<T>(Dictionary<int, T> dic, IDs id)
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
                {(int)IDs.DEFAULT, 0},
                {(int)IDs.PLAYER, 3},
                {(int)IDs.BOSS1, 50},
                {(int)IDs.BOSS2, 100},
                {(int)IDs.BOSS3, 200},
                  {(int)IDs.BOSS4, 300},
                {(int)IDs.DEFAULT_ENEMY, 1},
                {(int)IDs.ENEMYASTER, 3},
                {(int)IDs.DEFAULT_BULLET, 1},
                {(int)IDs.RECTHULLPART, 1},
                {(int)IDs.DEFAULT_PART, 0},
                {(int)IDs.DEFAULT_DROP, 1}

          };

        public static readonly Dictionary<int, float> DAMAGE =
          new Dictionary<int, float>
          {
                {(int)IDs.DEFAULT, 1},
                {(int)IDs.PLAYER, 100},
                {(int)IDs.DEFAULT_ENEMY, 1},
                {(int)IDs.DEFAULT_BULLET, 1},
                {(int)IDs.GRAVITYBULLET, 0},
                {(int)IDs.CHARGINGBULLET, 0},
                {(int)IDs.SPRAYBULLET, 0.15f }
          };

        public static readonly Dictionary<int, float> MASS =
          new Dictionary<int, float>
          {
                {(int)IDs.DEFAULT, 10},
                {(int)IDs.PLAYER, 5},    // Player acceleration
                {(int)IDs.DEFAULT_PART, 3},
                {(int)IDs.DEFAULT_BULLET, 3f},
                {(int)IDs.ENGINEPART, 0},
                {(int)IDs.SPRAYBULLET, 1f},
                {(int)IDs.CHARGINGBULLET, 7f},
                {(int)IDs.MINEBULLET, 5f},
                {(int)IDs.RECTHULLPART, 2f},
                {(int)IDs.ENEMYASTER, 100f}
          };

        public static readonly Dictionary<int, float> THRUST =
         new Dictionary<int, float>
         {
                {(int)IDs.DEFAULT, 10},
                 {(int)IDs.TURBOENGINEPART, 30f},
                {(int)IDs.DEFAULT_BULLET, 0},
                {(int)IDs.ENGINEPART, 10f}   // overall speed
         };

        public static readonly Dictionary<int, float> TURNSPEED =
        new Dictionary<int, float>
        {
                {(int)IDs.DEFAULT, 1000f * (float)Math.PI }, //! //rad per tick
                {(int)IDs.PLAYER, 0.1f * (float)Math.PI},
                {(int)IDs.DEFAULT_ENEMY, 1000f * (float)Math.PI},
                {(int)IDs.ENEMYSHOOT, 0.01f * (float)Math.PI},
                {(int)IDs.HOMINGBULLET, 0.07f * (float)Math.PI}
        };

        public static readonly Dictionary<int, float> FRICTION =
          new Dictionary<int, float>
          {
                {(int)IDs.DEFAULT, 100},
                {(int)IDs.PLAYER, 5},               // Player de-acceleratiion (lower more)
                {(int)IDs.DEFAULT_ENEMY, 5},       // Enemy speed
                {(int)IDs.ENEMYASTER, 0},
                {(int)IDs.DEFAULT_BULLET, 0f},
                {(int)IDs.MINEBULLET, 170},
                {(int)IDs.GRAVITYBULLET, 170},
                {(int)IDs.DEFAULT_PART, 0},
                {(int)IDs.ENGINEPART, 55}
          };

        public static readonly Dictionary<int, float> SCORE =
        new Dictionary<int, float>
        {
                {(int)IDs.DEFAULT, 1},
                {(int)IDs.DEFAULT_ENEMY, 100}
        };

        public static readonly Dictionary<int, float> PRICE =
        new Dictionary<int, float>
        {
                {(int)IDs.GUNPART, 10000},
                {(int)IDs.EMPTYPART, 0},
                {(int)IDs.SPRAYGUNPART, 12000},
                {(int)IDs.MINEGUNPART, 20000},
                {(int)IDs.CHARGINGGUNPART, 15000},
                {(int)IDs.GRAVITYGUNPART, 30000},
                {(int)IDs.RECTHULLPART, 8000},
                {(int)IDs.ENGINEPART, 14000},
                {(int)IDs.DEFAULT_PART, 15000},
                {(int)IDs.DEFAULT, 0}
        };

        public static readonly Dictionary<int, string> NAME =
        new Dictionary<int, string>
        {
                {(int)IDs.GUNPART, "Plasma Gun"},
                {(int)IDs.HAMMERPART, "Sell Parts"},
                {(int)IDs.SPRAYGUNPART, "Spray Gun"},
                {(int)IDs.MINEGUNPART, "Mine Deployer"},
                {(int)IDs.GRAVITYGUNPART, "Black Hole Gun"},
                {(int)IDs.CHARGINGGUNPART, "Fusion Gun"},
                {(int)IDs.RECTHULLPART, "Hull"},
                {(int)IDs.ENGINEPART, "Engine"},
                {(int)IDs.ROTATEPART, "Rotate"},
                {(int)IDs.DEFAULT, "NO_NAME"}
        };
    }
}
