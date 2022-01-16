using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class NumberDisplayer : GraphicsComponent
    {
        public int TargetNum { get; set; }
        private int currentNum;
        private float duration;
        private float time;
        private SoundEffect moneySFX;

        public override int Width => 0;
        public override int Height => 0;

        public NumberDisplayer(Entity entity, int targetNum, float duration)
            : base(entity, true)
        {
            TargetNum = targetNum;
            currentNum = targetNum;
            this.duration = duration;
            time = 0;
            moneySFX = Calc.SFXFromFile("moneysound.wav");
        }

        public override void Update()
        {
            if (currentNum != TargetNum)
            {
                time += TartarusGame.DeltaTime;
                int increment = Math.Sign(TargetNum - currentNum);
                while (time >= duration)
                {
                    time -= duration;

                    if (currentNum == TargetNum)
                        time = 0f;
                    else
                    {
                        currentNum += increment;
                        moneySFX.Play();
                    }
                        
                }
            }
        }

        public override void Render()
        {
            Drawing.Font.DrawOutline("$" + currentNum.ToString(), DrawingPosition, Color.White, PixelFont.Alignment.Left);
        }




    }
}
