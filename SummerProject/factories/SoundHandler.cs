using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using SummerProject.collidables;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SummerProject.factories
{
    class SoundHandler
    {
        private const int ARRAYSIZE = 200;
        private const int NBROFSONGS = 200;
        public static SoundEffect[] Sounds = new SoundEffect[ARRAYSIZE];
        public static SoundEffect[] Songs = new SoundEffect[NBROFSONGS];
        private static SoundEffectInstance song;
        public static IDs CurrentSongID { get; private set; }
        private static List<IDs> alreadyAddedSongs = new List<IDs>();
        private static Timer sTimer = new Timer(0);
        private static SoundEffectInstance[] seis = new SoundEffectInstance[ARRAYSIZE];
        private static MediaLibrary mL = new MediaLibrary();
        private static SoundEffect GetSound(int ID)
        {           
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

        //public static void PlayNextIfStopped() 
        //{
        //    if (song == null)
        //        PlaySong((int)IDs.SONG1INTRO);
        //    if (sTimer.IsFinished)
        //    {
        //            switch (currentSongID)
        //            {
        //            case IDs.SONG1INTRO:
        //            case IDs.VICTORY:
        //                    PlaySong((int)IDs.SONG1);
        //                    break;
        //                default:
        //                    PlaySong((int)currentSongID);
        //                    break;
        //            }
        //    }
        //}

        public static void PauseSong()
        {
            song.Pause();
        }

        public static void PlaySong(int ID)
        {
            if ((int)CurrentSongID != ID)
            {
                if (song != null)
                {
                    song.Stop();
                    song.Dispose();
                }
                song = Songs[ID].CreateInstance();
                //sTimer = new Timer((float)Songs[ID].Duration.TotalSeconds - 0.07f);
                song.IsLooped = true;
                song.Play();
                CurrentSongID = (IDs)ID;
                switch (CurrentSongID)
                {
                    case IDs.SONG1INTRO:
                    case IDs.GAMEOVER:
                    case IDs.VICTORY:
                        song.IsLooped = false;
                        break;
                    default:
                        song.IsLooped = true;
                        break;
                }
            }
            else
                if (song.State == SoundState.Paused)
                    song.Resume();
        }

        //public static void Update(GameTime gameTime)
        //{
        //    sTimer.CountDown(gameTime);
        //}

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
            else
            {
                sei.Pitch += SRandom.NextFloat() * 0.3f - 0.15f;
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
