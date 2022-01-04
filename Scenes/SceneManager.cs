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
        public static CharacterSelectScene CharacterSelect { get; private set; }
        public static MapScene MapScene { get; private set; }
        public static EncounterScene EncounterScene { get; private set; }
        public static WinScene WinScene { get; private set; }

        // Testing scenes

        public static void Initialize()
        {
            MainMenu = new MainMenu();
            CharacterSelect = new CharacterSelectScene();
            MapScene = new MapScene();
            EncounterScene = new EncounterScene();
            WinScene = new WinScene();

        }



    }
}
