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


        public GSong()
        {
            Song = Calc.SongFromFile("abc", "Triumphant.wav");
        }

        public void Play()
        {
            MediaPlayer.Play(Song);

            


        }







    }
}
