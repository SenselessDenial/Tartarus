using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    class Player : Actor
    {

        public Player() : base("Player")
        {
            Logger.Log(Name);
            Logger.Log("LVL: " + Level);
            Logger.Log(Stats);
            Skills.Add(Skill.Fireball);
            Skills.Add(Skill.Attack);
            Skills.Add(Skill.Heal);
            Skills.Add(Skill.Revive);
            Skills.SortByElement();
            Modifiers.AddInnateResistance(Elements.Physical, Resistances.Weak);
        }







    }
}
