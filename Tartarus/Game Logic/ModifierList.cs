using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class ModifierList
    {
        public Actor Actor { get; private set; }
        private Dictionary<Elements, Resistances> innateResistances;
        private Dictionary<Elements, OffensiveBonuses> innateOffensiveBonuses;


        public ModifierList(Actor actor)
        {
            Actor = actor;
            innateResistances = new Dictionary<Elements, Resistances>();
            innateOffensiveBonuses = new Dictionary<Elements, OffensiveBonuses>();
        }

        public void AddInnateResistance(Elements element, Resistances resistance)
        {
            if (innateResistances.ContainsKey(element))
            {
                Logger.Log("Actor already has innate resistance to this element. Overwriting it.");
                innateResistances.Remove(element);
            }
            innateResistances.Add(element, resistance);
        }

        public Resistances GetInnateResistance(Elements element)
        {
            return innateResistances.ContainsKey(element) ? innateResistances[element] : Resistances.None;
        }

        public void AddInnateOffensiveBonus(Elements element, OffensiveBonuses bonus)
        {
            if (innateOffensiveBonuses.ContainsKey(element))
            {
                Logger.Log("Actor already has innate offensive bonus to this element. Overwriting it.");
                innateOffensiveBonuses.Remove(element);
            }
            innateOffensiveBonuses.Add(element, bonus);
        }

        public OffensiveBonuses GetInnateOffensiveBonus(Elements element)
        {
            return innateOffensiveBonuses.ContainsKey(element) ? innateOffensiveBonuses[element] : OffensiveBonuses.None;
        }




    }

    public enum OffensiveBonuses
    {
        Great = 2,
        Good = 1,
        None = 0,
        Bad = -1,
        Awful = -2
    }

}
