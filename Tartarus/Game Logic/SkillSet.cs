using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class SkillSet : IEnumerable<Skill>
    {
        private List<Skill> skills;
        public Actor Actor { get; private set; }
        public Skill this[int index] => skills[index];
        public int Count => skills.Count;
        public List<Skill> UsableSkills => (from item in skills
                                            where item.IsUsableBy(Actor)
                                            select item).ToList();

        public List<Skill> NonBasicSkills => (from item in skills
                                              where !Skill.IsBasic(item)
                                              select item).ToList();

        public SkillSet(Actor actor)
        {
            skills = new List<Skill>();
            Actor = actor;

            Add(Skill.Attack);
        }

        public void Add(Skill skill)
        {
            if (!skills.Contains(skill))
                skills.Add(skill);
        }

        public void Remove(Skill skill)
        {
            skills.Remove(skill);
        }

        public bool Contains(Skill skill)
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

        public IEnumerator<Skill> GetEnumerator()
        {
            return skills.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return skills.GetEnumerator();
        }

        private Comparison<Skill> sortElement = (Skill x, Skill y) =>
        {
            if (x.Element < y.Element)
                return -1;
            else return x.Element > y.Element ? 1 : 0;
        };

    }
}
