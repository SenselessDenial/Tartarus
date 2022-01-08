using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    class EncounterNode : RoomNode
    {
        public EnemyParty Party { get; private set; }
        public static new GTexture NodeTexture => RunData.Map.NodeTextures[1];

        public override GTexture Texture => NodeTexture;

        public EncounterNode(EnemyParty party) : base() 
        {
            Party = party;
        }

        public EncounterNode(params EnemyNew[] actors) 
            : this(new EnemyParty(actors)) { }

        public override void Invoke()
        {
            RunData.CurrentEnemyParty = Party;
            TartarusGame.Instance.Scene = SceneManager.EncounterScene;
        }



    }
}
