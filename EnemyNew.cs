using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class EnemyNew : ActorNew
    {
        public Affiliations Affiliation { get; private set; }
        public int Money { get; private set; }


        public EnemyNew(string name,
                     int str, int mag, int end, int res, int spd, int level, int xp, Affiliations affiliation, int money)
            : base(name, str, mag, end, res, spd, level, xp)
        {
            Affiliation = affiliation;
            Money = money;
        }

        public EnemyNew(EnemyNew parent) : base(parent)
        {
            Affiliation = parent.Affiliation;
            Money = parent.Money;
        }

        public int CalculateXPDrop()
        {
            double a = -0.1 * Modifiers.NumOfWeaknesses;
            double b = 0.5 * Modifiers.NumOfResistances;
            double c = 0.005 * (Endurance + Resilience);

            return (int)(10 * Level * (1 + a + b + c)) + XP;
        }



    }

    public enum Affiliations
    {
        None,
        Mafia
    }


}
