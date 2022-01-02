using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class SineWaver : Component
    {
        public float amplitude;
        public float angFrequency;
        public float value;
        public float displacement;

        public SineWaver(Entity entity) 
            : base(entity, true, false) 
        {
            amplitude = 1f;
            angFrequency = 1f;
            displacement = 0f;

            value = amplitude * (float)Math.Sin(angFrequency * TartarusGame.RawTotalTime) + displacement;
        }

        public override void Update()
        {
            base.Update();

            value = amplitude * (float)Math.Sin(angFrequency * TartarusGame.RawTotalTime) + displacement;
        }











    }
}
