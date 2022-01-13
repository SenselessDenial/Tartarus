﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class EncounterNewEntity : Entity
    {
        public EncounterNew Encounter { get; private set; }
        private EnemyRenderer enemyRenderer;
        private HeroRenderer heroRenderer;
        private TargetList targetList;
        private SelectionMatrix options;

        public ActorNew SelectedTarget => targetList.SelectedTarget;
        public List<ActorNew> AvailableTargets => targetList.AvailableTargets;

        private ActorNew CurrentActor => Encounter.CurrentActor;
        private HeroNew CurrentHero => Encounter.CurrentHero;

        private EnemyNew CurrentEnemy => Encounter.CurrentEnemy;

        private SkillNew currentSkill = null;

        public bool HeroesWin => Encounter.HeroesWin;
        public bool EnemiesWin => Encounter.EnemiesWin;

        private OptionStates State
        {
            get => state;
            set
            {
                prevState = state;
                state = value;
                switch (state)
                {
                    case OptionStates.None:
                        options.Clear();
                        break;
                    case OptionStates.Options:
                        RefreshTargets(null);
                        if (Encounter.IsOver)
                        {
                            return;
                        }
                        if (Encounter.IsHeroesTurn)
                        {
                            GenerateOptions();
                        }
                        else
                        {
                            options.Clear();
                            if (CurrentEnemy != null)
                            {
                                SkillNew s = CurrentEnemy.AI.ChooseSkill(CurrentEnemy, Encounter);
                                List<ActorNew> targets = CurrentEnemy.AI.ChooseTargets(s, Encounter);
                                Encounter.UseSkillAndProceed(s, targets);
                                State = OptionStates.Options;
                            }
                            else
                            {
                                Logger.Log("AOO");
                            }
                            
                        }
                        break;
                    case OptionStates.Skills:
                        RefreshTargets(null);
                        GenerateSkills();
                        break;
                    case OptionStates.Targets:
                        RefreshTargets(currentSkill);
                        break;
                    default:
                        options.Clear();
                        break;
                }
            }
        }

        private OptionStates state = OptionStates.None;
        private OptionStates prevState;
        private Point prevSelection = Point.Zero;

        private enum OptionStates
        {
            None,
            Options,
            Skills,
            Targets
        }

        private void RefreshTargets(SkillNew skill)
        {
            targetList.RefreshTargets(skill);
            UpdateTargets();
        }

        private void UpdateTargets()
        {
            enemyRenderer.SelectedTarget = targetList.SelectedTarget;
            enemyRenderer.AvailableTargets = targetList.AvailableTargets;
            heroRenderer.SelectedTarget = targetList.SelectedTarget;
            heroRenderer.AvailableTargets = targetList.AvailableTargets;
        }


        public EncounterNewEntity(Scene scene, EncounterNew encounter) 
            : base(scene)
        {
            Encounter = encounter;
            targetList = new TargetList(this, encounter);
            enemyRenderer = new EnemyRenderer(this, encounter);
            enemyRenderer.Offset = new Vector2(Scene.Camera.Width / 2, 50);
            heroRenderer = new HeroRenderer(this, encounter);
            options = new SelectionMatrix(this, 3, 2, 50, 15);
            options.Offset = new Vector2(Scene.Camera.Width / 2, Scene.Camera.Height - 40);
            options.FocusPoint = new Point(1, 0);
            State = OptionStates.Options;
        }

        public void GenerateOptions()
        {
            options.Clear();
            Button attack = new Button("Attack", () => 
            { currentSkill = SkillNew.Attack; State = OptionStates.Targets; prevSelection = new Point(0, 0); });
            Button gun = new Button("Gun", () => 
            { currentSkill = SkillNew.Gun; State = OptionStates.Targets; prevSelection = new Point(1, 0); });
            Button skill = new Button("Skill", () => 
            { State = OptionStates.Skills; prevSelection = new Point(2, 0); });
            Button item = new Button("Item", () => 
            {
                if (CurrentHero.HasItem)
                    currentSkill = CurrentHero.Item.Skill;

                State = OptionStates.Targets;
                prevSelection = new Point(0, 1);
            
            });
            Button guard = new Button("Guard", () => 
            { currentSkill = SkillNew.Guard; State = OptionStates.Targets; prevSelection = new Point(1, 1); });
            Button pass = new Button("Pass", () => 
            { currentSkill = SkillNew.Pass; State = OptionStates.Targets; prevSelection = new Point(2, 1); });
            attack.IsSelectable = SkillNew.Attack.IsUsableBy(CurrentActor);
            gun.IsSelectable = SkillNew.Gun.IsUsableBy(CurrentActor);
            skill.IsSelectable = CurrentActor.Skills.NonBasicSkills.Count > 0;
            item.IsSelectable = CurrentHero.HasItem;
            guard.IsSelectable = SkillNew.Guard.IsUsableBy(CurrentActor);
            pass.IsSelectable = SkillNew.Pass.IsUsableBy(CurrentActor);

            options.Add(attack, gun, skill, item, guard, pass);
        }

        public void GenerateSkills()
        {
            options.Clear();
            List<SkillNew> nonbasics = CurrentActor?.Skills.NonBasicSkills;


            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    
                    SkillNew tempSkill = (i + (j * 3) < nonbasics.Count) ? CurrentActor.Skills.NonBasicSkills[i + (j * 3)] : null;
                    Button temp;
                    if (tempSkill != null)
                    {
                        int x = i; // IDK why but this is necessary. Somehow i and j persist, so they need temp ints
                        int y = j; // to save the previous selection. Weird.
                        temp = new Button(tempSkill.Name, () =>
                        {
                            currentSkill = tempSkill; State = OptionStates.Targets; prevSelection = new Point(x, y); });
                        temp.IsSelectable = tempSkill.IsUsableBy(CurrentActor);
                    }
                    else
                    {
                        temp = new Button("--", () => { });
                        temp.IsSelectable = false;
                    }
                    options.Add(i + (j * 3), temp);
                }
            }

        }

        private void GoBack()
        {
            switch (state)
            {
                case OptionStates.Skills:
                    State = OptionStates.Options;
                    options.CurrentPoint = new Point(2, 0);
                    break;
                case OptionStates.Targets:
                    if (prevState == OptionStates.Skills)
                    {
                        Logger.Log(prevSelection);
                        State = OptionStates.Skills;
                        options.CurrentPoint = prevSelection;
                    }
                    if (prevState == OptionStates.Options)
                    {
                        State = OptionStates.Options;
                        options.CurrentPoint = prevSelection;
                    }
                    break;
                case OptionStates.None:
                case OptionStates.Options:
                default:
                    Logger.Log("There is nothing to go back to.");
                    break;
            }
        }

        public override void Update()
        {
            base.Update();

            switch (State)
            {
                case OptionStates.Options:
                case OptionStates.Skills:
                    if (Input.Pressed(MappedKeys.Down))
                        options.MoveDown();
                    if (Input.Pressed(MappedKeys.Up))
                        options.MoveUp();
                    if (Input.Pressed(MappedKeys.Left))
                        options.MoveLeft();
                    if (Input.Pressed(MappedKeys.Right))
                        options.MoveRight();
                    if (Input.Pressed(MappedKeys.A))
                        options.Invoke();
                    break;
                case OptionStates.Targets:
                    if (Input.Pressed(MappedKeys.Right))
                    {
                        targetList.MoveNext();
                        UpdateTargets();
                    }
                    if (Input.Pressed(MappedKeys.Left))
                    {
                        targetList.MovePrevious();
                        UpdateTargets();
                    }
                    if (Input.Pressed(MappedKeys.A))
                    {
                        if (SelectedTarget != null)
                            Encounter.UseSkillAndProceed(currentSkill, SelectedTarget);
                        if (SelectedTarget == null && AvailableTargets.Count > 0)
                            Encounter.UseSkillAndProceed(currentSkill, AvailableTargets);
                        State = OptionStates.Options;
                    }
                        
                    break;
                case OptionStates.None:
                default:
                    break;
            }

            if (Input.Pressed(MappedKeys.B))
                GoBack();


            
        }








    }
}
