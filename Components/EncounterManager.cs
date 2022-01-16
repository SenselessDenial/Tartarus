using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tartarus
{
    public class EncounterManager : Component
    {
        public EncounterOLD Encounter { get; private set; }
        public PartyOLD ActiveParty => Encounter.ActiveParty;
        public ActorOLD CurrentActor => Encounter.CurrentActor;
        private ActorOLD previousActor;
        private ActorOLD currentTarget = null;
        private SkillOLD currentSkill = null;

        public bool IsOver => Encounter.IsOver;

        public RadioButtonSet CurrentChoice
        {
            get => currentChoice;
            private set
            {
                if (value == choices)
                {
                    previousChoice = null;
                    currentChoice = choices;
                }
                previousChoice = currentChoice;
                currentChoice = value;

                if (currentChoice == targets && previousChoice == skills)
                {
                    targets.IsVisible = false;
                    GenerateTargets();
                    targets.IsVisible = true;
                    skills.IsVisible = true;
                }
                else if (currentChoice == targets && previousChoice != skills)
                {
                    targets.IsVisible = false;
                    GenerateTargets();
                    targets.IsVisible = true;
                    skills.IsVisible = false;
                }
                else if (currentChoice == skills)
                {
                    targets.IsVisible = false;
                    skills.IsVisible = true;
                }
            }
        }
        private RadioButtonSet currentChoice;
        private RadioButtonSet previousChoice;

        private RadioButtonSet choices;
        private RadioButtonSet skills;
        private RadioButtonSet targets;


        public EncounterManager(EncounterOLD encounter)
        {
            Encounter = encounter;
            choices = new RadioButtonSet(new Vector2(20, 20), true);
            skills = new RadioButtonSet(new Vector2(60, 20), true);
            targets = new RadioButtonSet(new Vector2(120, 20), true);
            previousActor = null;
            Reset();
        }

        public EncounterManager(PartyOLD alpha, PartyOLD beta) 
            : this(new EncounterOLD(alpha, beta)) { }

        private void Reset()
        {
            choices.IsVisible = false;
            GenerateChoices();
            GenerateSkill();
            CurrentChoice = choices;
            choices.IsVisible = true;
            skills.IsVisible = false;
            targets.IsVisible = false;
        }


        public void GenerateChoices()
        {
            choices.Clear();

            Button attack = new Button("attack", () => 
            {
                currentSkill = SkillOLD.Attack;
                CurrentChoice = targets;
            });
            Button gun = new Button("gun", () =>
            {
                currentSkill = SkillOLD.Gun;
                CurrentChoice = targets;
            });
            Button skill = new Button("skill", () =>
            {
                CurrentChoice = skills;
            });
            Button item = new Button("item", () =>
            {
                if (CurrentActor.HeldItem != null)
                    currentSkill = CurrentActor.HeldItem.Skill;
                CurrentChoice = targets;
            });
            Button guard = new Button("guard", () =>
            {
                currentSkill = SkillOLD.Guard;
                CurrentChoice = targets;
            });
            Button pass = new Button("pass", () =>
            {
                currentSkill = SkillOLD.Pass;
                CurrentChoice = targets;
            });
            if (!CurrentActor.HasSkill(SkillOLD.Gun))
                gun.IsSelectable = false;
            if (CurrentActor.Skills.NonBasicSkills.Count == 0)
                skill.IsSelectable = false;
            if (CurrentActor.HeldItem == null)
                item.IsSelectable = false;
            if (!CurrentActor.HasSkill(SkillOLD.Guard))
                guard.IsSelectable = false;
            if (!CurrentActor.HasSkill(SkillOLD.Pass))
                pass.IsSelectable = false;

            choices.Add(attack, gun, skill, item, guard, pass);
        }

        public void GenerateSkill()
        {
            skills.Clear();
            foreach (var item in Encounter.CurrentActor.Skills.NonBasicSkills)
            {
                Button temp = new Button(item.Name, () => 
                {
                    currentSkill = item;
                    CurrentChoice = targets;
                });
                if (!item.IsUsableBy(CurrentActor) || !item.IsUseableCurrentlyIn(Encounter, CurrentActor))
                    temp.IsSelectable = false;

                skills.Add(temp);
            }

        }

        public void GenerateTargets()
        {
            targets.Clear();
            if (currentSkill == null)
            {
                Logger.Log("Current skill is null. Targets cannot be generated.");
                return;
            }

            Targets target = currentSkill.Target;

            if (target == Targets.Enemy)
            {
                foreach (var member in Encounter.InactiveParty)
                {
                    Button temp = new Button(member.Name, () =>
                    {
                        currentTarget = member;
                        Proceed();
                    });
                    if (!currentSkill.IsUseableOn(member))
                        temp.IsSelectable = false;

                    targets.Add(temp);
                }
            }
            else if (target == Targets.EnemyParty)
            {
                Button temp = new Button("Enemy Party", () =>
                {
                    currentTarget = Encounter.InactiveParty.FirstLivingActor;
                    Proceed();
                });
                if (!currentSkill.IsUseableOn(Encounter.InactiveParty))
                    temp.IsSelectable = false;

                targets.Add(temp);
            }
            else if (target == Targets.AllyParty)
            {
                Button temp = new Button("Ally Party", () =>
                {
                    currentTarget = Encounter.ActiveParty.FirstLivingActor;
                    Proceed();
                });
                if (!currentSkill.IsUseableOn(Encounter.ActiveParty))
                    temp.IsSelectable = false;

                targets.Add(temp);
            }
            else if (target == Targets.Ally)
            {
                foreach (var member in Encounter.ActiveParty)
                {
                    Button temp = new Button(member.Name, () =>
                    {
                        currentTarget = member;
                        Proceed();
                    });
                    if (!currentSkill.IsUseableOn(member))
                        temp.IsSelectable = false;

                    targets.Add(temp);
                }
            }
            else if (target == Targets.AllyExcludingSelf)
            {
                foreach (var member in Encounter.ActiveParty)
                {
                    if (member == CurrentActor)
                        continue;

                    Button temp = new Button(member.Name, () =>
                    {
                        currentTarget = member;
                        Proceed();
                    });
                    if (!currentSkill.IsUseableOn(member))
                        temp.IsSelectable = false;

                    targets.Add(temp);
                }
            }
            else if (target == Targets.Self)
            {
                var member = CurrentActor;
                Button temp = new Button(member.Name, () =>
                {
                    currentTarget = member;
                    Proceed();
                });
                if (!currentSkill.IsUseableOn(member))
                    temp.IsSelectable = false;

                targets.Add(temp);
            }
            else
                Logger.Log("Target type not yet implemented!");

        }

        public void GoBack()
        {
            if (previousChoice == null)
            {
                Logger.Log("Can't go back. Previous choice is null.");
                return;
            }
            if (CurrentChoice != choices)
                CurrentChoice.IsVisible = false;


            CurrentChoice = previousChoice;
            if (CurrentChoice == choices)
                previousChoice = null;
            else if (CurrentChoice == skills)
                previousChoice = choices;
        }

        public void Proceed()
        {
            previousActor = CurrentActor;
            bool success = Encounter.UseSkill(currentSkill, CurrentActor, currentTarget);
            if (success)
                Reset();
            if (IsOver)
                Encounter.GiveXPToParty(true);
            currentSkill = null;
            currentTarget = null;
        }

        public void MoveNext()
        {
            CurrentChoice.MoveNext();
        }

        public void MovePrevious()
        {
            CurrentChoice.MovePrevious();
        }

        public void Invoke()
        {
            CurrentChoice.Invoke();
        }

        public override void Update()
        {
            choices.Update();
            skills.Update();
            targets.Update();
        }

        public override void Render()
        {
            Drawing.Font.Draw("Current: " + CurrentActor.Name, new Vector2(20, 10));

            if (choices.IsVisible)
                choices.Render();
            if (skills.IsVisible)
                skills.Render();
            if (targets.IsVisible)
                targets.Render();
        }


    }
}
