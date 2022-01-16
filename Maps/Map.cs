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
            set { currentNode = value; selectedIndex = 0; selectedNode = currentNode[selectedIndex]; }
        }
        private RoomNode currentNode;
        private RoomNode selectedNode;
        public int FloorNum { get; private set; }

        private List<Floor> floorPlan;
        public int NumOfFloors => floorPlan.Count;
        private int selectedIndex = 0;

        public bool IsDone => FloorNum == floorPlan.Count;

        public Tileset NodeTextures { get; private set; }

        public static int MAX_ROOMS_PER_FLOOR = 3;

        public string Info => "Floor: " + FloorNum + "\nRoom: ";


        public Map()
        {
            FloorNum = 1;
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

            EnemyNew beast = new EnemyNew(ActorPresets.Beast);
            beast.SetMoney(5, 200);
            EncounterNode b = new EncounterNode(beast);
            RoomNode c = new MoneyNode();

            RoomNode d = new RoomNode();
            RoomNode e = new RoomNode();

            EncounterNode f = new EncounterNode(beast.Copy(), beast.Copy());

            a.Add(b);
            a.Add(c);

            b.Add(d);
            c.Add(e);
            c.Add(d);

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
            DrawConnections(position);
            DrawNodes(position);
        }

        private Floor NextFloor(Floor floor)
        {
            if (floorPlan.Contains(floor) && floorPlan.IndexOf(floor) != NumOfFloors - 1)
            {
                return floorPlan[floorPlan.IndexOf(floor) + 1];
            }
            return null;
        }

        private void DrawConnections(Vector2 position)
        {
            Vector2 drawingPos = position;
            Vector2 nextDrawingPos = position;
            foreach (var floor in floorPlan)
            {
                nextDrawingPos.Y += 20;
                foreach (var node in floor)
                {
                    drawingPos += new Vector2(node.Texture.Width / 2, node.Texture.Height / 2);
                    if (NextFloor(floor) == null)
                        return;

                    foreach (var nextNode in NextFloor(floor))
                    {
                        if (node.NextNodes.Contains(nextNode))
                        {
                            nextDrawingPos += new Vector2(nextNode.Texture.Width / 2, nextNode.Texture.Height / 2);
                            Drawing.DrawLine(drawingPos, nextDrawingPos, Color.Black);
                            nextDrawingPos -= new Vector2(nextNode.Texture.Width / 2, nextNode.Texture.Height / 2);
                        }
                        nextDrawingPos.X += 20;
                    }

                    drawingPos -= new Vector2(node.Texture.Width / 2, node.Texture.Height / 2);
                    drawingPos.X += 20;
                    nextDrawingPos.X = position.X;
                }
                drawingPos.X = position.X;
                drawingPos.Y += 20;
            }
        }

        private void DrawNodes(Vector2 position)
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
