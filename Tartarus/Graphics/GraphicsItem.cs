using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public abstract class GraphicsItem
    {
        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract void Draw();

    }
}
