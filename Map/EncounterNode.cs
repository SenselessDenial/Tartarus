using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    class EncounterNode : RoomNode
    {
        public Party Party { get; private set; }

        public EncounterNode(Party party) : base() 
        {
            Party = party;
        }

        public override void Invoke()
        {
            RunData.CurrentEnemyParty = Party;
            TartarusGame.Instance.Scene = SceneManager.EncounterScene;
        }



    }
}
