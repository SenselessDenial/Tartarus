using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class Map
    {
        private RoomNode currentNode;
        private RoomNode selectedNode;
        public int Floor { get; private set; }


        public Map()
        {
            Floor = 1;
        }

        public void InvokeCurrent()
        {
            if (selectedNode == null)
            {
                Logger.Log("Current node is null.");
                return;
            }

            Floor++;


            selectedNode.Invoke();
        }

        public void GenerateDemoMap()
        {
            RoomNode a = new RoomNode();


            
            //EncounterNode b = new EncounterNode();
        }





    }
}
