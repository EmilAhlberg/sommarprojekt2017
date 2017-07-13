using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace SummerProject.factories
{
    class SoundHandler
    {
        private const int ARRAYSIZE = 200;
        public static SoundEffect[] Sounds = new SoundEffect[ARRAYSIZE];
        private static SoundEffectInstance[] seis = new SoundEffectInstance[ARRAYSIZE];
        private static SoundEffect GetSound(int ID) {
            if(Sounds[ID] != null)
            {
                return Sounds[ID];
            }
            else
            switch (ID)
            {
                    default: return Sounds[(int)IDs.DEFAULT];
            }
        }

        public static void PlaySoundEffect(int ID)
        {
            SoundEffectInstance sei = GetSound(ID).CreateInstance();
            if (seis[ID] != null)
            {
                seis[ID].Stop();
                seis[ID].Dispose();
            }
            sei.Play();
            seis[ID] = sei;
        }

        public static void PlayLoopedSoundEffect(int ID)
        {
            SoundEffectInstance sei = GetSound(ID).CreateInstance();
            if (seis[ID] != null)
            {
                seis[ID].Stop();
                seis[ID].Dispose();
            }
            sei.IsLooped = true;
            sei.Play();
            seis[ID] = sei;
        }

        public static void StopLoopedSoundEffect(int ID)
        {
            if (seis[ID] != null)
            {
                seis[ID].Stop();
                seis[ID].Dispose();
                seis[ID] = null;
            }
        }
    }
}
