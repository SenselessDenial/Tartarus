using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class Component
    {
        public Entity Entity { get; private set; }
        public bool IsActive { get; set; }
        public bool IsVisible { get; set; }

        public Component(Entity entity, bool isActive, bool isVisible)
        {
            if (entity != null)
                entity.Add(this);
            IsActive = isActive;
            IsVisible = isVisible;
        }

        public Component(Entity entity) 
            : this(entity, true, true) { }

        public Component() : this(null, true, true) { }

        public void Added(Entity entity)
        {
            Entity = entity;
        }

        public void Removed(Entity entity)
        {
            Entity = null;
        }

        public virtual void Update() { }

        public virtual void Render() { }
    }
}
