using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using SummerProject.collidables;
using System.Collections.Generic;

namespace SummerProject.factories
{
    class SoundHandler
    {
        private const int ARRAYSIZE = 200;
        private const int NBROFSONGS = 200;
        public static SoundEffect[] Sounds = new SoundEffect[ARRAYSIZE];
        public static Song[] Songs = new Song[NBROFSONGS];
        private static Song song;
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

        public static void TryPlayNextSong() 
        {
           if(MediaPlayer.State == MediaState.Stopped  )
            {
                switch (EntityConstants.TypeToID(song.GetType())) {
                    case IDs.SONG1:
                        PlaySong((int)IDs.SONG2);
                        break;
                    case IDs.SONG2:
                        PlaySong((int)IDs.SONG1);
                        break;
                    default: PlaySong((int)IDs.SONG1);
                        break;
                }
            }
        }

        public static void PlaySong(int ID)
        {
           //     MediaPlayer.Stop();
            song = Songs[ID];
            MediaPlayer.Volume = 0.25f;
            MediaPlayer.Play(song);
        }

        public static void PlaySoundEffect(int ID)
        {
            SoundEffectInstance sei = GetSound(ID).CreateInstance();
            if (seis[ID] != null)
            {
                seis[ID].Stop();
                seis[ID].Dispose();
            }
            if (ID == (int)IDs.MENUCLICK)
            {
                sei.Volume = 0.2f;
                sei.Pitch = 0.1f;
            }
            else if (ID == (int)IDs.EXPLOSIONDEATHSOUND)
            {
                sei.Volume = 0.2f;
                sei.Pitch = 0.7f;
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
