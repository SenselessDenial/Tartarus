using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class HeroParty : Party<Hero>
    {
       
        public HeroParty() 
            : base() { }

        public HeroParty(params Hero[] heroes) 
            : base(heroes) { }

        public void ReceiveXP(int xp)
        {
            int xpPerMember = Calc.FindNearestDivisible(xp, NumOfLivingActors) / NumOfLivingActors;

            foreach (var item in actors)
                if (!item.IsDead)
                    item.ReceiveXP(xpPerMember);
        }
        public void ReceiveMoney(int money)
        {
            Logger.Log("The party has received " + money + " money.");
            RunData.Money += money;
        }


    }
}
