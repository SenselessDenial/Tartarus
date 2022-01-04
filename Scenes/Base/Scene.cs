using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class Scene : IEnumerable<Entity>
    {
        public EntityList Entities { get; private set; }
        public Renderer Renderer;
        private Dictionary<int, double> depthLookup;
        internal Camera Camera => Renderer.Camera;
        internal Color FillColor = Color.White;

        public Scene()
        {
            Entities = new EntityList(this);
            Renderer = new Renderer(this);
            depthLookup = new Dictionary<int, double>();
        }

        public void Add(Entity entity)
        {
            Entities.Add(entity);
            SetDepth(entity);
        }

        public void Remove(Entity entity)
        {
            Entities.Remove(entity);
        }

        public virtual void Begin()
        {

        }

        public virtual void End()
        {
            Entities.Clear();
        }

        public virtual void Update()
        {
            Entities.Update();
        }

        public virtual void BeforeRender()
        {

        }

        public virtual void Render()
        {
            Entities.Render();
        }

        public virtual void AfterRender()
        {

        }

        protected void SetNextScene(Scene scene)
        {
            TartarusGame.Instance.Scene = scene;
        }

        internal void SetDepth(Entity entity)
        {
            const double increment = 0.00001;

            double add = 0;
            if (depthLookup.TryGetValue(entity.layer, out add))
                depthLookup[entity.layer] += increment;
            else
                depthLookup.Add(entity.layer, increment);

            entity.depth = entity.layer - add;
            Entities.MarkUnsorted();
        }

        public IEnumerator<Entity> GetEnumerator()
        {
            return Entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Entities.GetEnumerator();
        }

        protected void Exit()
        {
            TartarusGame.Instance.Exit();
        }
    }
}