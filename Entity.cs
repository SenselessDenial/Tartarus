using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class Entity : IEnumerable<Component>
    {
        public Scene Scene { get; private set; }
        public ComponentList Components { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsVisible { get; private set; }

        public Vector2 Position;

        public float X
        {
            get => Position.X;
            set => Position.X = value;
        }

        public float Y
        {
            get => Position.Y;
            set => Position.Y = value;
        }

        public int Layer
        {
            get => layer;
            set
            {
                layer = value;
                if (Scene != null)
                    Scene.SetDepth(this);
            }
        }
        internal int layer;
        internal double depth;

        public Entity(Scene scene, Vector2 position)
        {
            if (scene != null)
                scene.Add(this);
            Position = position;
            Components = new ComponentList(this);

            IsActive = true;
            IsVisible = true;

            layer = 0;
            depth = 0;
        }

        public Entity(Scene scene) 
            : this(scene, Vector2.Zero) { }

        public Entity()
            : this(null, Vector2.Zero) { }

        public void Add(Component component)
        {
            Components.Add(component);
        }

        public void Remove(Component component)
        {
            Components.Remove(component);
        }
        
        public void Added(Scene scene)
        {
            Scene = scene;
        }

        public void Removed(Scene scene)
        {
            Scene = null;
        }

        public virtual void Update()
        {
            Components.Update();
        }

        public virtual void Draw()
        {
            Components.Draw();
        }

        public IEnumerator<Component> GetEnumerator()
        {
            return Components.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


    }
}
