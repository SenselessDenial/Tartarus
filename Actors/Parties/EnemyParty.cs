using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class EnemyParty : Party<Enemy>
    {

        public int MoneyDrop
        {
            get
            {
                int money = 0;
                foreach (var item in actors)
                    money += item.Money;
                return money;
            }
        }

        public int XPDrop
        {
            get
            {
                int xp = 0;
                foreach (var item in actors)
                    xp += item.CalculateXPDrop();
                return xp;
            }
        }

        public EnemyParty()
            : base() { }

        public EnemyParty(params Enemy[] enemies)
            : base(enemies) { }





    }
}
