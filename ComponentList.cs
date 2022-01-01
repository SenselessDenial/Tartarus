using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class ComponentList : IEnumerable<Component>
    {
        public Entity Entity { get; private set; }

        private List<Component> components;
        private List<Component> toAdd;
        private List<Component> toRemove;

        public bool IsLocked
        {
            get => isLocked;
            private set
            {
                isLocked = false;
                UpdateLists();
            }
        }
        private bool isLocked;

        public int Count => components.Count;
        public Component this[int index] => components[index];

        public ComponentList(Entity entity)
        {
            Entity = entity;

            components = new List<Component>();
            toAdd = new List<Component>();
            toRemove = new List<Component>();
        }

        private void UpdateLists()
        {
            if (toAdd.Count > 0)
            {
                foreach (var component in toAdd)
                {
                    components.Add(component);
                    component.Added(Entity);
                }
                toAdd.Clear();
            }

            if (toRemove.Count > 0)
            {
                foreach (var component in toRemove)
                {
                    components.Remove(component);
                    component.Removed(Entity);
                }
                toRemove.Clear();
            }
        }

        public void Add(Component component)
        {
            if (!IsLocked)
            {
                if (!components.Contains(component))
                {
                    components.Add(component);
                    component.Added(Entity);
                }
                else
                    Logger.Log("ComponentList already contains this component.");
            }
            else
            {
                if (!toAdd.Contains(component) && !components.Contains(component))
                    toAdd.Add(component);
                else
                    Logger.Log("ComponentList already contains this component.");
            }
        }

        public void Remove(Component component)
        {
            if (!IsLocked)
            {
                if (components.Contains(component))
                {
                    components.Remove(component);
                    component.Removed(Entity);
                }
                else
                    Logger.Log("ComponentList does not contain this component.");
            }
            else
            {
                if (!toRemove.Contains(component) && components.Contains(component))
                    toRemove.Add(component);
                else
                    Logger.Log("ComponentList does not contain this component.");
            }
        }

        public void Add(IEnumerable<Component> components)
        {
            foreach (var component in components)
                Add(component);
        }

        public void Add(params Component[] components)
        {
            foreach (var component in components)
                Add(component);
        }

        public void Remove(IEnumerable<Component> components)
        {
            foreach (var component in components)
                Remove(component);
        }

        public void Remove(params Component[] components)
        {
            foreach (var component in components)
                Remove(component);
        }

       internal void Update()
        {
            IsLocked = true;
            foreach (var item in components)
                if (item.IsActive)
                    item.Update();
            IsLocked = false;
        }
        internal void Render()
        {
            IsLocked = true;
            foreach (var item in components)
                if (item.IsVisible)
                    item.Render();
            IsLocked = false;
        }

        public IEnumerator<Component> GetEnumerator()
        {
            return components.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
