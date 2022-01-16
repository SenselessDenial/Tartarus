using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class PartyOLD : IEnumerable<ActorOLD>
    {
        
        private List<ActorOLD> actors;
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
        public ActorOLD FirstLivingActor
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
        public ActorOLD LastLivingActor
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

        public ActorOLD this[int index] => index >= 0 && index < actors.Count ? actors[index] : throw new Exception("Party_this index not in bounds.");

        public PartyOLD(int capacity, params ActorOLD[] actors)
        {
            this.actors = new List<ActorOLD>();
            Capacity = capacity;
            foreach (var item in actors)
                Add(item);
        }

        public PartyOLD(int capacity)
        {
            actors = new List<ActorOLD>();
            Capacity = capacity;
        }

        public PartyOLD(params ActorOLD[] actors)
        {
            this.actors = new List<ActorOLD>();
            Capacity = actors.Length;
            foreach (var item in actors)
                Add(item);
        }

        public void Add(ActorOLD actor)
        {
            if (Count >= Capacity)
                Logger.Log(actor.Name + " could not be added to party because party is full.");
            else
                actors.Add(actor);
                actor.Party = this;
        }

        public ActorOLD NextLivingActor(int startingIndex)
        {
            for (int i = startingIndex + 1; i < actors.Count; i++)
                if (actors[i].IsDead == false)
                    return actors[i];
            Logger.Log("There are no living members after the given index.");
            return null;
        }

        public ActorOLD NextLivingActor(ActorOLD actor)
        {
            if (actors.Contains(actor))
                return NextLivingActor(IndexOf(actor));
            else
            {
                Logger.Log("Party does not contain actor " + actor.Name);
                return null;
            }
        }

        public void ReceiveXP(PartyOLD party)
        {
            ReceiveXP(party.CalculateXPDrop());
        }

        public void ReceiveXP(int xp)
        {
            int xpPerMember = Calc.FindNearestDivisible(xp, Count) / Count;
            foreach (var member in actors)
                member.ReceiveXP(xpPerMember);
        }

        public int IndexOf(ActorOLD actor)
        {
            return actors.IndexOf(actor);
        }

        public bool Contains(ActorOLD actor)
        {
            return actors.Contains(actor);
        }

        public IEnumerator<ActorOLD> GetEnumerator()
        {
            return actors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int CalculateXPDrop()
        {
            int temp = 0;
            foreach (var item in actors)
                temp += item.CalculateXPDrop();
            return temp;
        }


    }
}
