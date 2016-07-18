using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace coolgame
{
    public static class SoundManager
    {
        private static Dictionary<string, SoundEffect> clips = new Dictionary<string, SoundEffect>();
        private static Dictionary<string, Song> songs = new Dictionary<string, Song>();

        public static bool muted;

        private static float soundVolume;
        public static int SoundVolume
        {
            get { return (int)soundVolume; }
            set
            {
                soundVolume = value/100f;
                if(!muted)
                {
                    SoundEffect.MasterVolume = soundVolume;
                }
            }
        }

        private static float musicVolume;
        public static int MusicVolume
        {
            get { return (int)musicVolume; }
            set
            {
                musicVolume = value/100f;
                if (!muted)
                {
                    MediaPlayer.Volume = musicVolume;
                }
            }
        }

        public static void ToggleMute()
        {
            if (!muted)
            {
                SoundEffect.MasterVolume = 0;
                MediaPlayer.Volume = 0;
                muted = true;
                Debug.Log(SoundEffect.MasterVolume.ToString());
            }
            else
            {
                SoundEffect.MasterVolume = soundVolume;
                MediaPlayer.Volume = musicVolume;
                muted = false;
                Debug.Log(SoundEffect.MasterVolume.ToString());
            }
        }

        public static void AddClip(SoundEffect clip, string name)
        {
            clips.Add(name, clip);
        }

        public static void AddSong(Song song, string name)
        {
            songs.Add(name, song);
        }

        public static void PlayClip(string clipName)
        {
            clips[clipName].Play();
        }

        public static void PlaySong(string songName)
        {
            MediaPlayer.Play(songs[songName]);
        }

    }
}
