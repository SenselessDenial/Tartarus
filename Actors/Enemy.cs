using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class Enemy : Actor
    {
        public Affiliations Affiliation { get; private set; }
        public int Money { get; private set; }

        public EnemyAI AI;


        public Enemy(string name,
                     int str,  int dex, int mag, int end, int res, int spd, int level, int xp, Affiliations affiliation, int money)
            : base(name, str, dex, mag, end, res, spd, level, xp)
        {
            Affiliation = affiliation;
            Money = money;
            AI = EnemyAI.RandomSkillAndTarget;
        }

        public Enemy(Enemy parent) : base(parent)
        {
            Affiliation = parent.Affiliation;
            Money = parent.Money;
            AI = parent.AI;
        }

        public void SetItems(GTexture portrait)
        {
            Portrait = portrait;
        }

        public void SetMoney(int min, int max)
        {
            Money = Calc.Next(min, max + 1);
        }

        public int CalculateXPDrop()
        {
            double a = -0.1 * Modifiers.NumOfWeaknesses;
            double b = 0.5 * Modifiers.NumOfResistances;
            double c = 0.005 * (Endurance + Resilience);

            return (int)(10 * Level * (1 + a + b + c)) + XP;
        }

        public Enemy Copy()
        {
            return new Enemy(this);
        }


    }

    public enum Affiliations
    {
        None,
        Mafia
    }


}
