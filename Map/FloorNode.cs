using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class FloorNode
    {
        public List<FloorNode> NextNodes { get; private set; }

        public FloorNode()
        {
            NextNodes = new List<FloorNode>();
        }

        public void Add(FloorNode node)
        {
            NextNodes.Add(node);
        }

        public void Remove(FloorNode node)
        {
            NextNodes.Remove(node);
        }

        public void Invoke()
        {

        }






    }
}
