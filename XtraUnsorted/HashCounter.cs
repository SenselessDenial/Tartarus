using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    /// <summary>
    /// Records the counts of distinct items.
    /// </summary>
    class HashCounter<T>
    {
        private Dictionary<T, int> values;

        public int TotalNumOfItems
        {
            get
            {
                int count = 0;
                foreach (var item in values)
                    count += item.Value;
                return count;
            }
        }

        public int this[T item] { get { return values[item]; } }

        public HashCounter()
        {
            values = new Dictionary<T, int>();
        }

        public void Add(T item)
        {
            if (!values.ContainsKey(item))
                values.Add(item, 1);
            else
                values[item] += 1;
        }

        public void Sort()
        {
            var list = values.ToList();
            list.Sort(IntSort);

            values.Clear();
            foreach (var item in list)
                values.Add(item.Key, item.Value);
        }

        public string Percent(T key)
        {
            if (values.ContainsKey(key))
            {
                var item = values[key];
                int total = TotalNumOfItems;
                float percent = (float)item * 100 / total;
                return (percent + "%");
            }
            return null;
        }

        public void Clear()
        {
            values.Clear();
        }

        public void PrintPercentages()
        {
            int total = TotalNumOfItems;
            Sort();

            foreach (var item in values)
            {
                float percent = (float)item.Value * 100 / total;
                Logger.Log(item.Key + ": " + percent + "%");
            }

        }

        Comparison<KeyValuePair<T, int>> IntSort = (KeyValuePair<T, int> one, KeyValuePair<T, int> two) =>
        {
            if (one.GetType() == typeof(int))
            {
                if (int.Parse(one.Key.ToString()) > int.Parse(two.Key.ToString()))
                {
                    return 1;
                }
                else if (int.Parse(one.Key.ToString()) < int.Parse(two.Key.ToString()))
                {
                    return -1;
                }
                return 0;
            }
            return 1;
        };


    }
}
