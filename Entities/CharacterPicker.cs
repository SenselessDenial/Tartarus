﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using static Tartarus.ActorPresets;

namespace Tartarus
{
    public class CharacterPicker : Entity
    {
        private List<HeroNew> presets;
        private SoundEffect movingSFX;

        private int Count => presets.Count;
        

        private int CurrentIndex = 0;
        private int SelectedIndex = -1;
        public bool IsDone = false;

        public HeroNew CurrentHero => presets[CurrentIndex];

        public CharacterPicker(Scene scene, SoundEffect movingSFX) 
            : base(scene)
        {
            presets = new List<HeroNew>();
            this.movingSFX = movingSFX;
        }

        public CharacterPicker(Scene scene)
            : this(scene, null) { }
        

        public void Swap(int index1, int index2)
        {
            if (index1 < 0 || index1 >= Count || index2 < 0 || index2 >= Count)
            {
                Logger.Log("index is out of bounds. Cannot swap.");
                return;
            }
            HeroNew temp = presets[index1];
            presets[index1] = presets[index2];
            presets[index2] = temp;
        }

        public void Add(HeroNew preset)
        {
            if (!presets.Contains(preset))
                presets.Add(preset);
            presets.Sort(sortUnlocked);
        }

        public void MoveNext()
        {
            for (int i = 0; i < Count; i++)
            {
                CurrentIndex += 1;
                if (CurrentIndex >= Count)
                    CurrentIndex = 0;
                if (presets[CurrentIndex].IsUnlocked == true)
                {
                    movingSFX?.Play();
                    return;
                }
                    
            }
            Logger.Log("There are no unlocked presets!");
        }

        public void MovePrevious()
        {
            for (int i = 0; i < Count; i++)
            {
                CurrentIndex -= 1;
                if (CurrentIndex < 0)
                    CurrentIndex = Count - 1;
                if (presets[CurrentIndex].IsUnlocked == true)
                {
                    movingSFX?.Play();
                    return;
                }
                    
            }
            Logger.Log("There are no unlocked presets!");
        }

        public void Select()
        {
            if (SelectedIndex == -1)
            {
                SelectedIndex = CurrentIndex;
            }
            else
            {
                Swap(SelectedIndex, CurrentIndex);
                SelectedIndex = -1;
            }
        }


        public void CreateParty()
        {
            HeroNew a = new HeroNew(presets[0]);
            HeroNew b = new HeroNew(presets[1]);
            HeroNew c = new HeroNew(presets[2]);
            HeroNew d = new HeroNew(presets[3]);

            RunData.PlayerParty = new HeroParty(a, b, c, d);
            Logger.Log("Party Created!");
            IsDone = true;
        }

        public override void Update()
        {
            base.Update();

            if (IsDone)
                return;

            if (Input.Pressed(MappedKeys.B))
                SelectedIndex = -1;
            if (Input.Pressed(MappedKeys.Up))
                MovePrevious();
            if (Input.Pressed(MappedKeys.Down))
                MoveNext();
            if (Input.Pressed(MappedKeys.A))
                Select();
        }

        public override void Render()
        {
            base.Render();
            Vector2 drawPos = Position;
            for (int i = 0; i < Count; i++)
            {
                if (!presets[i].IsUnlocked)
                    Drawing.Font.DrawOutline("???", drawPos, Color.DarkGray);
                else
                {
                    Color color = Color.LightGray;
                    if (IsDone)
                        color = Color.Green;
                    else if (i == SelectedIndex && i == CurrentIndex)
                        color = Color.Yellow;
                    else if (i == CurrentIndex)
                        color = Color.White;
                    else if (i == SelectedIndex)
                        color = Color.Goldenrod;
                    

                    Drawing.Font.DrawOutline(i + 1 +": " + presets[i].Name, drawPos, color);
                }
                drawPos.Y += 12;
            }

            presets[CurrentIndex].Portrait?.Draw(new Vector2(Scene.Camera.Width - 50, Scene.Camera.Height / 2), DrawAlignment.Center);
        }

        private readonly Comparison<HeroNew> sortUnlocked = (x, y) =>
        {
            if (x.IsUnlocked && !y.IsUnlocked)
                return -1;
            else if (!x.IsUnlocked && y.IsUnlocked)
                return 1;
            else
                return 0;
        };


    }
}
