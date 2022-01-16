using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class SkillSet
    {
        public Actor Actor { get; private set; }

        private readonly List<Skill> skills;

        public List<Skill> NonBasicSkills => (from item in skills
                                              where !Skill.IsBasic(item)
                                              select item).ToList();

        public List<Skill> UsableSkills => (from item in skills
                                               where item.IsUsableBy(Actor)
                                               select item).ToList();

        public SkillSet(Actor actor)
        {
            skills = new List<Skill>();
            Actor = actor;

            Add(Skill.Attack);
        }

        public void Add(Skill SkillNew)
        {
            if (!skills.Contains(SkillNew))
                skills.Add(SkillNew);
        }

        public void Remove(Skill SkillNew)
        {
            skills.Remove(SkillNew);
        }

        public bool Contains(Skill SkillNew)
        {
            return skills.Contains(SkillNew);
        }

        public void SortByElement()
        {
            skills.Sort(sortElement);
        }

        public SkillSet Copy(Actor actor)
        {
            SkillSet temp = new SkillSet(actor);
            foreach (var SkillNew in skills)
                temp.Add(SkillNew);
            return temp;
        }

        private Comparison<Skill> sortElement = (Skill x, Skill y) =>
        {
            if (x.Element < y.Element)
                return -1;
            else return x.Element > y.Element ? 1 : 0;
        };







    }
}
