using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public static class RunData
    {
        public static HeroParty PlayerParty;
        public static EnemyParty CurrentEnemyParty;
        public static Map Map;
        public static int Money;

        public static int Floor => Map.FloorNum;
        public static bool IsGameOver => PlayerParty.IsAllDead;

        public static void Reset()
        {
            PlayerParty = null;
            CurrentEnemyParty = null;
            Money = 0;
            Map = new Map();
        }






















    }
}
