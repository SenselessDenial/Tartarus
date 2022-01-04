using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class EncounterEntity : Entity
    {
        private EncounterManager em;
        public bool IsOver => em.IsOver;

        public EncounterEntity(Scene scene, Party alpha, Party beta)
            : base(scene) 
        {
            em = new EncounterManager(alpha, beta);
            Add(em);
        }

        public override void Update()
        {
            base.Update();

            if (Input.Pressed(MappedKeys.Up))
                em.MoveNext();
            if (Input.Pressed(MappedKeys.Down))
                em.MovePrevious();
            if (Input.Pressed(MappedKeys.B))
                em.GoBack();
            if (Input.Pressed(MappedKeys.A))
                em.Invoke();

        }










    }
}
