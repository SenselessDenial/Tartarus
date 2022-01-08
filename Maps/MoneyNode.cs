using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    class MoneyNode : RoomNode
    {
        public static new GTexture NodeTexture => RunData.Map.NodeTextures[3];

        private int money;

        public override GTexture Texture => NodeTexture;

        public MoneyNode(int money) : base()
        {
            this.money = money;
        }

        public MoneyNode() 
            : this(Calc.Next(50, 101)) { }

        public override void Invoke()
        {
            RunData.PlayerParty.ReceiveMoney(money);
        }











    }
}
