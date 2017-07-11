﻿using System.Collections.Generic;

namespace SummerProject.factories
{
    class SpriteHandler
    {
        private const int ARRAYSIZE = 200;
        public static Sprite[] Sprites = new Sprite[ARRAYSIZE];
        public static Sprite GetSprite(int ID) {
            if(Sprites[ID] != null)
            {
                return new Sprite(Sprites[ID]);
            }
            else
            switch (ID)
            {
                    case (int)IDs.EVILBULLET: return new Sprite(Sprites[(int)IDs.DEFAULT_BULLET]);
                    case (int)IDs.DEFAULT_ENEMY: return new Sprite(Sprites[(int)IDs.RECTHULLPART]);
                    case (int)IDs.PLAYER: return new Sprite(Sprites[(int)IDs.RECTHULLPART]);
                    case (int)IDs.ENEMYASTER: return new Sprite(Sprites[(int)IDs.ENEMYASTER]);
                    case (int)IDs.ENEMYSHOOT: return new Sprite(Sprites[(int)IDs.RECTHULLPART]);
                    default: return new Sprite(Sprites[(int)IDs.DEFAULT]);
            }
        }
    }
}
