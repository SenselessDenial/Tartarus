using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    /// <summary>
    /// Chooses between weighted items in a pool.
    /// </summary>
    class ChoicePool<T>
    {
        private Dictionary<T, int> items;

        public int NumOfItems => items.Count;
        public int TotalWeight
        {
            get
            {
                int total = 0;
                foreach (var item in items)
                {
                    total += item.Value;
                }
                return total;
            }
        }

        public ChoicePool()
        {
            items = new Dictionary<T, int>();
        }

        public void Add(T item, int weight)
        {
            items.Add(item, weight);
        }

        public void Add(T item)
        {
            items.Add(item, 1);
        }

        public T Choose()
        {
            int choice = Calc.Next(0, TotalWeight) + 1;
            int current = 0;

            foreach (var item in items)
            {
                current += item.Value;
                if (current >= choice)
                {
                    return item.Key;
                }
            }
            return items.ToList()[0].Key;
        }


    }
}
