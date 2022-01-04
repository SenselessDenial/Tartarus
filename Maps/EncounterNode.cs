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
        public static new GTexture NodeTexture => RunData.Map.NodeTextures[1];

        public override GTexture Texture => NodeTexture;

        public EncounterNode(Party party) : base() 
        {
            Party = party;
        }

        public EncounterNode(params Actor[] actors) 
            : this(new Party(actors)) { }

        public override void Invoke()
        {
            RunData.CurrentEnemyParty = Party;
            TartarusGame.Instance.Scene = SceneManager.EncounterScene;
        }



    }
}
