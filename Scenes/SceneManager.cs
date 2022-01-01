using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public static class SceneManager
    {
        public static Scene Default => MainMenu;

        // Game Scenes
        public static MainMenu MainMenu { get; private set; }
        public static CharacterSelect CharacterSelect { get; private set; }

        // Testing scenes
        public static TestScene TestScene { get; private set; }

        public static void Initialize()
        {
            MainMenu = new MainMenu();
            CharacterSelect = new CharacterSelect();

            TestScene = new TestScene();
        }



    }
}
