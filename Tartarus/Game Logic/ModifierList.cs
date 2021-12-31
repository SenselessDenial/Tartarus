using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    class ModifierList
    {
        private Dictionary<Elements, Resistances> innateResistances;



        public ModifierList()
        {
            innateResistances = new Dictionary<Elements, Resistances>();
        }

        public void AddInnateResistance(Elements element, Resistances resistance)
        {
            innateResistances.Add(element, resistance);
        }

        public Resistances GetInnateResistance(Elements element)
        {
            return innateResistances.ContainsKey(element) ? innateResistances[element] : Resistances.None;
        }





    }
}
