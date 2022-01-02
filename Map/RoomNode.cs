using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class RoomNode
    {
        public List<RoomNode> NextNodes { get; private set; }

        public RoomNode()
        {
            NextNodes = new List<RoomNode>();
        }

        public void Add(RoomNode node)
        {
            NextNodes.Add(node);
        }

        public void Remove(RoomNode node)
        {
            NextNodes.Remove(node);
        }

        public virtual void Invoke()
        {
            Logger.Log("Nothing to see here!");
        }






    }
}
