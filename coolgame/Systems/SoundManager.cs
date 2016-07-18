using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;

namespace coolgame
{
    public static class SoundManager
    {
        private static Dictionary<string, SoundEffect> clips = new Dictionary<string, SoundEffect>();

        public static void SetVolume(int volume)
        {
            SoundEffect.MasterVolume = volume/100f;
        }

        public static void AddClip(SoundEffect clip, string name)
        {
            clips.Add(name, clip);
        }

        public static void PlayClip(string clipName)
        {
            clips[clipName].Play();
        }
    }
}
