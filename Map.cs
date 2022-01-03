using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class Map
    {
        public RoomNode CurrentNode
        {
            get => currentNode;
            set { currentNode = value; selectedNode = currentNode[0]; }
        }
        private RoomNode currentNode;
        private RoomNode selectedNode;
        public int FloorNum { get; private set; }

        private List<Floor> floorPlan;
        private int selectedIndex = 0;

        public Tileset NodeTextures { get; private set; }

        public static int MAX_ROOMS_PER_FLOOR = 3;

        public string Info => "Floor: " + FloorNum + "\nRoom: ";


        public Map()
        {
            FloorNum = 0;
            floorPlan = new List<Floor>();
            NodeTextures = new Tileset("nodes.png", 16, 16);
            GenerateDemoMap();
        }

        public void InvokeCurrent()
        {
            if (selectedNode == null)
            {
                Logger.Log("Current node is null.");
                return;
            }
            selectedNode.Invoke();
            FloorNum++;
            CurrentNode = selectedNode;
        }

        public void MovePrevious()
        {
            for (int i = 0; i < currentNode.NextNodes.Count; i++)
            {
                selectedIndex--;

                if (selectedIndex < 0)
                {
                    selectedIndex = currentNode.NextNodes.Count - 1;
                }

                if (currentNode[selectedIndex] != null)
                {
                    selectedNode = currentNode[selectedIndex];
                    return;
                }
            }
            Logger.Log("There are no nodes to move to.");
        }

        public void MoveNext()
        {
            for (int i = 0; i < currentNode.NextNodes.Count; i++)
            {
                selectedIndex++;

                if (selectedIndex >= currentNode.NextNodes.Count)
                {
                    selectedIndex = 0;
                }

                if (currentNode[selectedIndex] != null) 
                {
                    selectedNode = currentNode[selectedIndex];
                    return;
                }
            }
            Logger.Log("There are no nodes to move to.");
        }

        private void GenerateDemoMap()
        {
            RoomNode a = new RoomNode();

            Actor beast = ActorPresets.Beast.Actor;
            EncounterNode b = new EncounterNode(beast);
            RoomNode c = new RoomNode();

            RoomNode d = new RoomNode();
            RoomNode e = new RoomNode();

            EncounterNode f = new EncounterNode(ActorPresets.Beast.Actor);

            a.Add(b);
            a.Add(c);

            b.Add(d);
            c.Add(e);

            d.Add(f);
            e.Add(f);


            Floor one = new Floor(a);
            Floor two = new Floor(b, c);
            Floor three = new Floor(d, e);
            Floor four = new Floor(f);



            Add(one, two, three, four);

            CurrentNode = a;
        }

        private void Add(params Floor[] floors)
        {
            foreach (var floor in floors)
            {
                floorPlan.Add(floor);
            }
        }

        private class Floor : IEnumerable<RoomNode>
        {
            private List<RoomNode> rooms;
            public int Count => rooms.Count;

            public Floor()
            {
                rooms = new List<RoomNode>();
            }

            public Floor(params RoomNode[] nodes)
            {
                rooms = new List<RoomNode>();

                foreach (var node in nodes)
                    rooms.Add(node);
            }

            public void Add(RoomNode node)
            {
                if (rooms.Count == MAX_ROOMS_PER_FLOOR)
                    Logger.Log("Floor is at max capacity. Room did not add");
                else
                    rooms.Add(node);
            }

            public IEnumerator<RoomNode> GetEnumerator()
            {
                return ((IEnumerable<RoomNode>)rooms).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable)rooms).GetEnumerator();
            }
        }

        public void Draw(Vector2 position)
        {
            Vector2 drawingPos = position;
            foreach (var floor in floorPlan)
            {
                foreach (var node in floor)
                {
                    if (node == selectedNode)
                        node.Texture.Draw(drawingPos, Color.Yellow);
                    else
                        node.Texture.Draw(drawingPos);
                    drawingPos.X += 20;
                }
                drawingPos.X = position.X;
                drawingPos.Y += 20;
            }
        }



    }
}
