using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class Enemy : Actor
    {
        public GTexture Portrait;

        public Enemy(string name,
                     int str, int mag, int end, int res, int spd,
                     int strWeight, int magWeight, int endWeight, int resWeight, int spdWeight)
            : base(name, str, mag, end, res, spd, strWeight, magWeight, endWeight, resWeight, spdWeight) { }




    }
}
