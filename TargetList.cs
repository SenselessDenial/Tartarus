using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class TargetList : Component
    {
        private Encounter encounter;
        public Actor SelectedTarget { get; private set; }
        public List<Actor> AvailableTargets { get; private set; }
        private Actor CurrentActor => encounter.CurrentActor;
        private bool IsHeroesTurn => encounter.IsHeroesTurn;

        public TargetList(Entity entity, Encounter encounter) : base(entity)
        {
            this.encounter = encounter;
            SelectedTarget = null;
            AvailableTargets = new List<Actor>();
        }

        public void RefreshTargets(Skill skill)
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
                    }
                    else
                    {
                        AvailableTargets.AddRange(from item in encounter.Enemies
                                                  where skill.IsUseableOn(item)
                                                  select item);
                    }
                    break;
                case Targets.AllyExcludingSelf:
                    if (IsHeroesTurn)
                    {
                        AvailableTargets.AddRange(from item in encounter.Heroes
                                                  where item != CurrentActor && skill.IsUseableOn(item)
                                                  select item);
                    }
                    else
                    {
                        AvailableTargets.AddRange(from item in encounter.Enemies
                                                  where item != CurrentActor && skill.IsUseableOn(item)
                                                  select item);
                    }
                    break;
                case Targets.AllyParty:
                    if (IsHeroesTurn)
                    {
                        AvailableTargets.AddRange(from item in encounter.Heroes
                                                  where skill.IsUseableOn(item)
                                                  select item);
                    }
                    else
                    {
                        AvailableTargets.AddRange(from item in encounter.Enemies
                                                  where skill.IsUseableOn(item)
                                                  select item);
                    }
                    break;
                case Targets.Enemy:
                    if (IsHeroesTurn)
                    {
                        AvailableTargets.AddRange(from item in encounter.Enemies
                                                  where skill.IsUseableOn(item)
                                                  select item);
                    }
                    else
                    {
                        AvailableTargets.AddRange(from item in encounter.Heroes
                                                  where skill.IsUseableOn(item)
                                                  select item);
                    }
                    break;
                case Targets.EnemyParty:
                    if (IsHeroesTurn)
                    {
                        AvailableTargets.AddRange(from item in encounter.Enemies
                                                  where skill.IsUseableOn(item)
                                                  select item);
                    }
                    else
                    {
                        AvailableTargets.AddRange(from item in encounter.Heroes
                                                  where skill.IsUseableOn(item)
                                                  select item);
                    }
                    break;
                case Targets.RandomEnemy:
                    if (IsHeroesTurn)
                    {
                        AvailableTargets.AddRange(from item in encounter.Enemies
                                                  where skill.IsUseableOn(item)
                                                  select item);
                    }
                    else
                    {
                        AvailableTargets.AddRange(from item in encounter.Heroes
                                                  where skill.IsUseableOn(item)
                                                  select item);
                    }
                    break;
                case Targets.All:
                    AvailableTargets.AddRange(from item in encounter.Enemies
                                              where skill.IsUseableOn(item)
                                              select item);
                    AvailableTargets.AddRange(from item in encounter.Heroes
                                              where skill.IsUseableOn(item)
                                              select item);
                    break;
                default:
                    Logger.Log("Targeting case not accounted for!");
                    break;
            }

            switch (skill.Target)
            {
                case Targets.Self:
                case Targets.Ally:
                case Targets.AllyExcludingSelf:
                case Targets.Enemy:
                    if (AvailableTargets.Count > 0)
                        SelectedTarget = AvailableTargets[0];
                    else
                    {
                        Logger.Log("There are no available targets. Setting selected target to null.");
                        SelectedTarget = null;
                    }
                    break;
                case Targets.AllyParty:
                case Targets.EnemyParty:
                case Targets.RandomEnemy:
                case Targets.All:
                    if (AvailableTargets.Count > 0)
                        SelectedTarget = null;
                    else
                    {
                        Logger.Log("There are no available targets. Setting selected target to null.");
                        SelectedTarget = null;
                    }
                    break;
                default:
                    throw new Exception("Targeting case not accounted for. Cannot set selected targets");
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
