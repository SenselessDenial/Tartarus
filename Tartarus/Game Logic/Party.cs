using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class Party : IEnumerable<Actor>
    {
        
        private List<Actor> actors;
        public int Capacity { get; private set; }
        public int Count => actors.Count;
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
        public Actor FirstLivingActor
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
        public Actor LastLivingActor
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

        public Actor this[int index] => index >= 0 && index < actors.Count ? actors[index] : throw new Exception("Party_this index not in bounds.");

        public Party(int capacity, params Actor[] actors)
        {
            this.actors = new List<Actor>();
            Capacity = capacity;
            foreach (var item in actors)
                Add(item);
        }

        public Party(int capacity)
        {
            actors = new List<Actor>();
            Capacity = capacity;
        }

        public Party(params Actor[] actors)
        {
            this.actors = new List<Actor>();
            Capacity = actors.Length;
            foreach (var item in actors)
                Add(item);
        }

        public void Add(Actor actor)
        {
            if (Count >= Capacity)
                Logger.Log(actor.Name + " could not be added to party because party is full.");
            else
                actors.Add(actor);
                actor.Party = this;
        }

        public Actor NextLivingActor(int startingIndex)
        {
            for (int i = startingIndex + 1; i < actors.Count; i++)
                if (actors[i].IsDead == false)
                    return actors[i];
            Logger.Log("There are no living members after the given index.");
            return null;
        }

        public Actor NextLivingActor(Actor actor)
        {
            if (actors.Contains(actor))
                return NextLivingActor(IndexOf(actor));
            else
            {
                Logger.Log("Party does not contain actor " + actor.Name);
                return null;
            }
        }

        public int IndexOf(Actor actor)
        {
            return actors.IndexOf(actor);
        }

        public bool Contains(Actor actor)
        {
            return actors.Contains(actor);
        }

        public IEnumerator<Actor> GetEnumerator()
        {
            return actors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
