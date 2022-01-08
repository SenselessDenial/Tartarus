using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public abstract class PartyNew<T> : IEnumerable<T> where T : ActorNew
    {
        protected List<T> actors = new List<T>();
        public int Count => actors.Count;
        public PartyNew()
        {
            actors = new List<T>();
        }

        public PartyNew(params T[] actors)
        {
            foreach (var item in actors)
                Add(item);
        }

        public void Add(T actor)
        {
            if (!actors.Contains(actor))
                actors.Add(actor);
        }

        public T this[int index] => (index >= 0 && index < Count) ? actors[index] : null;

        public bool IsAllDead
        {
            get
            {
                foreach (var item in actors)
                    if (item.IsDead == false)
                        return false;
                return true;
            }
        }

        public T FirstLivingActor
        {
            get
            {
                for (int i = 0; i < actors.Count; i++)
                    if (actors[i].IsDead == false)
                        return actors[i];
                Logger.Log("Party has no more remaining members!");
                return null;
            }
        }

        public T LastLivingActor
        {
            get
            {
                for (int i = actors.Count - 1; i >= 0; i--)
                    if (actors[i].IsDead == false)
                        return actors[i];
                Logger.Log("Party has no more remaining members!");
                return null;
            }
        }

        public int IndexOfFirstLivingActor => actors.IndexOf(FirstLivingActor);
        public int IndexOfLastLivingActor => actors.IndexOf(LastLivingActor);

        public int NumOfLivingActors => (from item in actors
                                         where !item.IsDead
                                         select item).Count();



        public T NextLivingActor(int startingIndex)
        {
            for (int i = startingIndex + 1; i < actors.Count; i++)
                if (actors[i].IsDead == false)
                    return actors[i];
            Logger.Log("There are no living members after the given index.");
            return null;
        }

        public int IndexOfNextLivingActor(int startingIndex)
        {
            if (NextLivingActor(startingIndex) == null)
            {
                Logger.Log("There is no next living actor. returning index of -1.");
                return -1;
            }
            return actors.IndexOf(NextLivingActor(startingIndex));
        }

        public int IndexOf(T actor)
        {
            return actors.IndexOf(actor);
        }

        public T RandomLivingActor()
        {
            List<T> living = new List<T>();
            foreach (var item in actors)
            {
                if (!item.IsDead)
                    living.Add(item);
            }
            return Calc.ChooseRandom(living);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)actors).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)actors).GetEnumerator();
        }
    }
}
