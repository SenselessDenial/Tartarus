using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class MapReader : Entity
    {
        private Map Map => RunData.Map;


        public MapReader(Scene scene) : base(scene)
        {

        }

        public override void Update()
        {
            base.Update();

            if (Input.Pressed(MappedKeys.Right))
                Map.MoveNext();
            if (Input.Pressed(MappedKeys.Left))
                Map.MovePrevious();
            if (Input.Pressed(MappedKeys.A))
                Map.InvokeCurrent();

        }

        public override void Render()
        {
            base.Render();


        }







    }
}
