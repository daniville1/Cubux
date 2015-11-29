using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;

namespace Cubux.Engine
{
    class Sound
    {
        public static SoundPlayer soundPlayer;
        
        public static void play(string soundName)
        {
            soundPlayer = new System.Media.SoundPlayer();
            soundPlayer.SoundLocation = @"Sound\" + soundName;
            soundPlayer.Play();
        }

        public static void stop()
        {
            soundPlayer.Stop();
        }
        
    }
}
