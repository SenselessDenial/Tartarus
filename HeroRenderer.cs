using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class HeroRenderer : GraphicsComponent
    {
        private Encounter encounter;

        private GTexture selector;

        public Actor SelectedTarget;
        public List<Actor> AvailableTargets;
        private static int selectorSpacing = 15;

        private GTexture turnIcon;

        public HeroRenderer(Entity entity, Encounter encounter) 
            : base(entity, true)
        {
            this.encounter = encounter;
            selector = new GTexture("selector.png");
            turnIcon = new GTexture("turn_icon.png");
        }

        public override void Render()
        {
            Vector2 pos = new Vector2(-1, 160);
            Drawing.DrawBox(new Rectangle((int)pos.X, (int)pos.Y + 20, Scene.Camera.Width + 1, Scene.Camera.Height - (int)pos.Y + 20), Color.White);
            
            foreach (var item in encounter.Heroes)
            {
                item.Draw(pos);

                if (encounter.CurrentActor == item)
                    turnIcon.Draw(pos + new Vector2(0, -1), DrawAlignment.BottomLeft);

                if ((SelectedTarget == null && AvailableTargets.Contains(item)) || SelectedTarget == item)
                    selector.Draw(pos + new Vector2((Hero.CardBase.Width / 2), -selectorSpacing), DrawAlignment.TopCenter);

                pos.X += Hero.CardBase.Width - 1;
            }
        }





    }
}
