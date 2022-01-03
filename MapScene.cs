﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class MapScene : Scene
    {
        private MapReader reader;


        public MapScene() : base()
        {
            // DO NOT INITALIZE OBJECTS HERE!
        }

        public override void Begin()
        {
            base.Begin();
            FillColor = Color.Blue;
            Camera.Scale = new Vector2(4);


            reader = new MapReader(this);
        }

        public override void End()
        {
            base.End();

            Entities.Clear();
        }

        public override void Update()
        {
            base.Update();


        }

        public override void Render()
        {
            base.Render();

            RunData.Map.Draw(new Vector2(50, 50));
        }


    }
}
