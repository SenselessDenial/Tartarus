using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;  
using static Tartarus.ActorPresets;

namespace Tartarus
{
    public class CharacterPicker : Entity
    {
        private List<HeroPreset> presets;

        private int Count => presets.Count;

        private int CurrentIndex = 0;
        private int SelectedIndex = -1;
        public bool IsDone = false;

        public CharacterPicker(Scene scene) : base(scene)
        {
            presets = new List<HeroPreset>();
        }

        public void Swap(int index1, int index2)
        {
            if (index1 < 0 || index1 >= Count || index2 < 0 || index2 >= Count)
            {
                Logger.Log("index is out of bounds. Cannot swap.");
                return;
            }
            HeroPreset temp = presets[index1];
            presets[index1] = presets[index2];
            presets[index2] = temp;
        }

        public void Add(HeroPreset preset)
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
                    return;
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
                    return;
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
            Actor a = presets[0].Actor.Copy();
            Actor b = presets[1].Actor.Copy();
            Actor c = presets[2].Actor.Copy();
            Actor d = presets[3].Actor.Copy();

            RunData.PlayerParty = new Party(a, b, c, d);
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
                    

                    Drawing.Font.DrawOutline(i + 1 +": " + presets[i].Actor.Name, drawPos, color);
                }
                drawPos.Y += 12;
            }

            presets[CurrentIndex].Portrait?.Draw(new Vector2(120, 55));
        }

        private Comparison<HeroPreset> sortUnlocked = (x, y) =>
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
