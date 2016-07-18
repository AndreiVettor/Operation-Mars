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

        public static void SetVolume(int volume)
        {
            SoundEffect.MasterVolume = volume/100f;
            MediaPlayer.Volume = volume / 100f;
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
