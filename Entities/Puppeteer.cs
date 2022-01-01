using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tartarus
{
    public class Puppeteer : Entity
    {
        public Entity Puppet { get; private set; }

        public Puppeteer(Scene scene, Entity puppet) : base(scene)
        {
            Puppet = puppet;
        }








        public override void Update()
        {
            base.Update();

            if (Puppet != null)
            {
                if (Input.Check(Keys.Left))
                    Puppet.X -= 2;
                if (Input.Check(Keys.Right))
                    Puppet.X += 2;
                if (Input.Check(Keys.Up))
                    Puppet.Y -= 2;
                if (Input.Check(Keys.Down))
                    Puppet.Y += 2;
            }

        }

        public override void Render()
        {
            base.Render();


        }








    }
}
