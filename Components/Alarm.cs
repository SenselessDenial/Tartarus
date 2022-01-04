using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class Alarm : Component
    {
        // Not finished!


        private float time;
        private float duration;
        public bool IsRunning { get; private set; }


        public Alarm(float duration) : base()
        {
            time = 0f;
            this.duration = duration;
        }

        public void Start()
        {
            IsRunning = true;
        }

        public void Stop()
        {
            IsRunning = false;
        }

        public void Reset()
        {
            time = 0f;
        }

        public override void Update()
        {
            base.Update();

            if (IsRunning)
                time += TartarusGame.DeltaTime;
        }



















    }
}
