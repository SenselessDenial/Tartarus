using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public static class RunData
    {
        public static Party PlayerParty;
        public static int Money;
        public static int Floor;

        public static bool IsGameOver => PlayerParty.IsAllDead;

        public static void Reset()
        {
            PlayerParty = null;
            Money = 0;
            Floor = 1;
        }






















    }
}
