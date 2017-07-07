using System.Collections.Generic;

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
                    default: return new Sprite(Sprites[(int)IDs.DEFAULT]);
            }
        }
    }
}
