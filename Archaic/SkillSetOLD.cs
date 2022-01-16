using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class SkillSetOLD : IEnumerable<SkillOLD>
    {
        public ActorOLD Actor { get; private set; }

        private readonly List<SkillOLD> skills;
        public SkillOLD this[int index] => skills[index];
        public int Count => skills.Count;
        public List<SkillOLD> UsableSkills => (from item in skills
                                            where item.IsUsableBy(Actor)
                                            select item).ToList();

        public List<SkillOLD> NonBasicSkills => (from item in skills
                                              where !SkillOLD.IsBasic(item)
                                              select item).ToList();

        public SkillSetOLD(ActorOLD actor)
        {
            skills = new List<SkillOLD>();
            Actor = actor;

            Add(SkillOLD.Attack);
        }

        public void Add(SkillOLD skill)
        {
            if (!skills.Contains(skill))
                skills.Add(skill);
        }

        public void Remove(SkillOLD skill)
        {
            skills.Remove(skill);
        }

        public bool Contains(SkillOLD skill)
        {
            return skills.Contains(skill);
        }

        public void Print()
        {
            foreach (var item in skills)
                Logger.Log(item.Name);
        }

        public void SortByElement()
        {
            skills.Sort(sortElement);
        }

        public SkillSetOLD Copy(ActorOLD actor)
        {
            SkillSetOLD temp = new SkillSetOLD(actor);
            foreach (var skill in skills)
                temp.Add(skill);
            return temp;
        }

        public IEnumerator<SkillOLD> GetEnumerator()
        {
            return skills.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return skills.GetEnumerator();
        }

        private Comparison<SkillOLD> sortElement = (SkillOLD x, SkillOLD y) =>
        {
            if (x.Element < y.Element)
                return -1;
            else return x.Element > y.Element ? 1 : 0;
        };

    }
}
