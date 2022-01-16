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

        public int NumOfWeaknesses => (from item in innateResistances
                                       where item.Value == Resistances.Weak
                                       select item).Count();

        public int NumOfResistances => (from item in innateResistances
                                        where item.Value == Resistances.Strong
                                        select item).Count();

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

        public ModifierList Copy(Actor actor)
        {
            ModifierList temp = new ModifierList(actor);
            foreach (var item in innateResistances)
                temp.AddInnateResistance(item.Key, item.Value);
            foreach (var item in innateOffensiveBonuses)
                temp.AddInnateOffensiveBonus(item.Key, item.Value);
            return temp;
        }


    }
}
