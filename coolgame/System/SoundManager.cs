using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace coolgame
{
    public static class SoundManager
    {
        private static Dictionary<string, SoundEffect> clips = new Dictionary<string, SoundEffect>();
        private static Dictionary<string, Song> songs = new Dictionary<string, Song>();

        public static bool Muted;

        private static float soundVolume;
        public static int SoundVolume
        {
            get { return (int)soundVolume; }
            set
            {
                soundVolume = value/100f;
                if(!Muted)
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
                if (!Muted)
                {
                    MediaPlayer.Volume = musicVolume;
                }
            }
        }

        public static void ToggleMute()
        {
            if (!Muted)
            {
                SoundEffect.MasterVolume = 0;
                MediaPlayer.Volume = 0;
                Muted = true;
            }
            else
            {
                SoundEffect.MasterVolume = soundVolume;
                MediaPlayer.Volume = musicVolume;
                Muted = false;
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

        public static void LoadContent(ContentManager Content)
        {
            AddSong(Content.Load<Song>("music"), "music");
            AddClip(Content.Load<SoundEffect>("towerlaser"), "enemylaser");
            AddClip(Content.Load<SoundEffect>("towerlaser2"), "laser");
            AddClip(Content.Load<SoundEffect>("crawlerhit"), "crawlerhit");
            AddClip(Content.Load<SoundEffect>("steelroachhit"), "steelroachhit");
            AddClip(Content.Load<SoundEffect>("steelroachattack"), "steelroachattack");
            AddClip(Content.Load<SoundEffect>("ebloop"), "electrobeam");
        }

        public static void PlayClip(string clipName)
        {
            if(!Muted)
            {
                clips[clipName].Play();
            }
        }

        public static void PlaySong(string songName)
        {
            MediaPlayer.Play(songs[songName]);
            MediaPlayer.IsRepeating = true;
        }

        public static void PauseMusic()
        {
            MediaPlayer.Pause();
        }

        public static void ResumeMusic()
        {
            MediaPlayer.Resume();
        }

        public static void StopMusic()
        {
            MediaPlayer.Stop();
        }
    }
}
