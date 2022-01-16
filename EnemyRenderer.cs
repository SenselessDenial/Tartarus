using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class EnemyRenderer : GraphicsComponent
    {
        private Encounter encounter;

        private GTexture selector;

        public Actor SelectedTarget;
        public List<Actor> AvailableTargets;

        private static int hpBarSpacing = 10;
        private static int selectorSpacing = 15;
        private static int spacingBetweenEnemies = 10;

        public EnemyRenderer(Entity entity, Encounter encounter)
            :base(entity, true)
        {
            this.encounter = encounter;
            selector = new GTexture("selector.png");
        }

        private Vector2 FindStartDrawingPos()
        {
            Vector2 pos = DrawingPosition;
            int totalWidth = 0;
            foreach (var enemy in encounter.Enemies)
            {
                totalWidth += enemy.Portrait.Width + spacingBetweenEnemies;
            }
            totalWidth -= spacingBetweenEnemies;
            pos.X -= totalWidth / 2;
            return pos;
        }

        public override void Render()
        {
            base.Render();

            Vector2 drawingPos = FindStartDrawingPos();
            Vector2 pos = new Vector2(0, 0);

            foreach (var item in encounter.Enemies)
            {
                if (!item.IsDead)
                {
                    item.Portrait?.Draw(drawingPos + pos);
                    new HealthBar(30, 6, item.HPPercent).Draw(drawingPos + pos + new Vector2(item.Portrait.Width / 2 - 15, -hpBarSpacing));
                    if (AvailableTargets != null)
                    {
                        if ((SelectedTarget == null && AvailableTargets.Contains(item)) || SelectedTarget == item)
                            selector.Draw(drawingPos + pos + new Vector2(item.Portrait.Width / 2 - selector.Width / 2, -hpBarSpacing - selectorSpacing));
                    }
                    
                }
                pos.X += item.Portrait.Width + spacingBetweenEnemies;
            }


        }













    }
}
