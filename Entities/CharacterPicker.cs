using System;
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
        private List<Hero> presets;
        private SoundEffect movingSFX;
        private SoundEffect swapSFX;
        private TimingDiagram bookmarkTimer;

        private int Count => presets.Count;
        

        private int CurrentIndex
        {
            get
            {
                return currentIndex;
            }
            set
            {
                renderIndex = currentIndex;
                currentIndex = value;
            }
        }

        private int currentIndex = 0;
        private int renderIndex = 0;


        private int SelectedIndex = -1;
        public bool IsDone = false;

        private bool IsBookmarkOnScreen => bookmarkTimer.Value < Scene.Camera.Width;

        public Hero CurrentHero => presets[CurrentIndex];

        public CharacterPicker(Scene scene, SoundEffect movingSFX, SoundEffect swapSFX) 
            : base(scene)
        {
            presets = new List<Hero>();
            this.movingSFX = movingSFX;
            this.swapSFX = swapSFX;
            bookmarkTimer = new TimingDiagram(this, Scene.Camera.Width + 5);
            bookmarkTimer.Add(Scene.Camera.Width + 5, Scene.Camera.Width - 71, 0.6f, Ease.SineOut);
            bookmarkTimer.Start();
        }

        public CharacterPicker(Scene scene)
            : this(scene, null, null) { }
        

        public void Swap(int index1, int index2)
        {
            if (index1 < 0 || index1 >= Count || index2 < 0 || index2 >= Count)
            {
                Logger.Log("index is out of bounds. Cannot swap.");
                return;
            }
            Hero temp = presets[index1];
            presets[index1] = presets[index2];
            presets[index2] = temp;
            swapSFX.Play();
        }

        public void Add(Hero preset)
        {
            if (!presets.Contains(preset))
                presets.Add(preset);
            presets.Sort(sortUnlocked);
        }

        public void MoveNext()
        {
            for (int i = 0; i < Count; i++)
            {

                if (CurrentIndex + 1 >= Count)
                    CurrentIndex = 0;
                else
                    CurrentIndex += 1;

                if (presets[CurrentIndex].IsUnlocked == true)
                {
                    bookmarkTimer.Reset();
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

                if (CurrentIndex - 1 < 0)
                    CurrentIndex = Count - 1;
                else
                    CurrentIndex -= 1;

                if (presets[CurrentIndex].IsUnlocked == true)
                {
                    bookmarkTimer.Reset();
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
            Hero a = new Hero(presets[0]);
            Hero b = new Hero(presets[1]);
            Hero c = new Hero(presets[2]);
            Hero d = new Hero(presets[3]);

            RunData.PlayerParty = new HeroParty(a, b, c, d);
            Logger.Log("Party Created!");
            IsDone = true;
        }

        public override void Update()
        {
            if (IsBookmarkOnScreen)
                renderIndex = currentIndex;

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

            presets[renderIndex]?.DrawBookmark(new Vector2(bookmarkTimer.Value, 20));
        }

        private readonly Comparison<Hero> sortUnlocked = (x, y) =>
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
