using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class SkillSetNew
    {
        public ActorNew Actor { get; private set; }

        private readonly List<SkillNew> skills;

        public List<SkillNew> NonBasicSkills => (from item in skills
                                              where !SkillNew.IsBasic(item)
                                              select item).ToList();

        public List<SkillNew> UsableSkills => (from item in skills
                                               where item.IsUsableBy(Actor)
                                               select item).ToList();

        public SkillSetNew(ActorNew actor)
        {
            skills = new List<SkillNew>();
            Actor = actor;

            Add(SkillNew.Attack);
        }

        public void Add(SkillNew SkillNew)
        {
            if (!skills.Contains(SkillNew))
                skills.Add(SkillNew);
        }

        public void Remove(SkillNew SkillNew)
        {
            skills.Remove(SkillNew);
        }

        public bool Contains(SkillNew SkillNew)
        {
            return skills.Contains(SkillNew);
        }

        public void SortByElement()
        {
            skills.Sort(sortElement);
        }

        public SkillSetNew Copy(ActorNew actor)
        {
            SkillSetNew temp = new SkillSetNew(actor);
            foreach (var SkillNew in skills)
                temp.Add(SkillNew);
            return temp;
        }

        private Comparison<SkillNew> sortElement = (SkillNew x, SkillNew y) =>
        {
            if (x.Element < y.Element)
                return -1;
            else return x.Element > y.Element ? 1 : 0;
        };







    }
}
