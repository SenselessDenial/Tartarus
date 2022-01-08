using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class TargetList : Component
    {
        private EncounterNew encounter;
        public ActorNew SelectedTarget { get; private set; }
        public List<ActorNew> AvailableTargets { get; private set; }
        private ActorNew CurrentActor => encounter.CurrentActor;
        private bool IsHeroesTurn => encounter.IsHeroesTurn;

        public TargetList(Entity entity, EncounterNew encounter) : base(entity)
        {
            this.encounter = encounter;
            SelectedTarget = null;
            AvailableTargets = new List<ActorNew>();
        }

        public void RefreshTargets(SkillNew skill)
        {
            AvailableTargets.Clear();
            SelectedTarget = null;

            if (skill == null)
            {
                Logger.Log("Skill is null. Performing clear.");
                return;
            }
            
            switch (skill.Target)
            {
                case Targets.Self:
                    AvailableTargets.Add(CurrentActor);
                    SelectedTarget = CurrentActor;
                    break;
                case Targets.Ally:
                    if (IsHeroesTurn)
                    {
                        AvailableTargets.AddRange(from item in encounter.Heroes
                                                  where skill.IsUseableOn(item)
                                                  select item);
                        SelectedTarget = AvailableTargets[0];
                    }
                    else
                    {
                        AvailableTargets.AddRange(from item in encounter.Enemies
                                                  where skill.IsUseableOn(item)
                                                  select item);
                        SelectedTarget = AvailableTargets[0];
                    }
                    break;
                case Targets.AllyExcludingSelf:
                    if (IsHeroesTurn)
                    {
                        AvailableTargets.AddRange(from item in encounter.Heroes
                                                  where item != CurrentActor && skill.IsUseableOn(item)
                                                  select item);
                        SelectedTarget = AvailableTargets[0];
                    }
                    else
                    {
                        AvailableTargets.AddRange(from item in encounter.Enemies
                                                  where item != CurrentActor && skill.IsUseableOn(item)
                                                  select item);
                        SelectedTarget = AvailableTargets[0];
                    }
                    break;
                case Targets.AllyParty:
                    if (IsHeroesTurn)
                    {
                        AvailableTargets.AddRange(from item in encounter.Heroes
                                                  where skill.IsUseableOn(item)
                                                  select item);
                        SelectedTarget = null;
                    }
                    else
                    {
                        AvailableTargets.AddRange(from item in encounter.Enemies
                                                  where skill.IsUseableOn(item)
                                                  select item);
                        SelectedTarget = null;
                    }
                    break;
                case Targets.Enemy:
                    if (IsHeroesTurn)
                    {
                        AvailableTargets.AddRange(from item in encounter.Enemies
                                                  where skill.IsUseableOn(item)
                                                  select item);
                        SelectedTarget = AvailableTargets[0];
                    }
                    else
                    {
                        AvailableTargets.AddRange(from item in encounter.Heroes
                                                  where skill.IsUseableOn(item)
                                                  select item);
                        SelectedTarget = AvailableTargets[0];
                    }
                    break;
                case Targets.EnemyParty:
                    if (IsHeroesTurn)
                    {
                        AvailableTargets.AddRange(from item in encounter.Enemies
                                                  where skill.IsUseableOn(item)
                                                  select item);
                        SelectedTarget = null;
                    }
                    else
                    {
                        AvailableTargets.AddRange(from item in encounter.Heroes
                                                  where skill.IsUseableOn(item)
                                                  select item);
                        SelectedTarget = null;
                    }
                    break;
                case Targets.RandomEnemy:
                    if (IsHeroesTurn)
                    {
                        AvailableTargets.AddRange(from item in encounter.Enemies
                                                  where skill.IsUseableOn(item)
                                                  select item);
                        SelectedTarget = null;
                    }
                    else
                    {
                        AvailableTargets.AddRange(from item in encounter.Heroes
                                                  where skill.IsUseableOn(item)
                                                  select item);
                        SelectedTarget = null;
                    }
                    break;
                case Targets.All:
                    AvailableTargets.AddRange(from item in encounter.Enemies
                                              where skill.IsUseableOn(item)
                                              select item);
                    AvailableTargets.AddRange(from item in encounter.Heroes
                                              where skill.IsUseableOn(item)
                                              select item);
                    SelectedTarget = null;
                    break;
                default:
                    Logger.Log("Targeting case not accounted for!");
                    break;
            }
        }

        public void MoveNext()
        {
            if (SelectedTarget == null)
            {
                Logger.Log("Selected target is null. Can't move.");
                return;
            }

            int index = AvailableTargets.IndexOf(SelectedTarget);
            index++;
            if (index >= AvailableTargets.Count)
                index = 0;
            SelectedTarget = AvailableTargets[index];
        }

        public void MovePrevious()
        {
            if (SelectedTarget == null)
            {
                Logger.Log("Selected target is null. Can't move.");
                return;
            }

            int index = AvailableTargets.IndexOf(SelectedTarget);
            index--;
            if (index < 0)
                index = AvailableTargets.Count - 1;
            SelectedTarget = AvailableTargets[index];
        }






    }
}
