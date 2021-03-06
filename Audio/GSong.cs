using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Tartarus
{
    class GSong
    {
        private Song Song;
        private float Volume = 1f;


        public GSong(string name, string filename, float volume)
        {
            Song = Calc.SongFromFile(name, filename);
            Volume = volume;
        }

        public void Play()
        {
            MediaPlayer.Play(Song);
            MediaPlayer.Volume = Volume;
        }

        public void Stop()
        {
            if (MediaPlayer.Queue.ActiveSong == Song)
                MediaPlayer.Stop();
        }







    }
}
