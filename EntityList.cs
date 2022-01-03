using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class EntityList : IEnumerable<Entity>
    {
        public Scene Scene { get; private set; }

        private List<Entity> entities;
        private List<Entity> toAdd;
        private List<Entity> toRemove;

        private bool isUnsorted;

        public EntityList(Scene scene)
        {
            Scene = scene;

            entities = new List<Entity>();
            toAdd = new List<Entity>();
            toRemove = new List<Entity>();

            isUnsorted = true;
        }

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

        public int Count => entities.Count;
        public Entity this[int index] => entities[index];

        private void UpdateLists()
        {
            if (toAdd.Count > 0)
            {
                foreach (var entity in toAdd)
                {
                    entities.Add(entity);
                    entity.Added(Scene);
                }
                toAdd.Clear();
            }

            if (toRemove.Count > 0)
            {
                foreach (var entity in toRemove)
                {
                    entities.Remove(entity);
                    entity.Removed(Scene);
                }
                toRemove.Clear();
            }

            if (isUnsorted)
                Sort();

        }

        private void Sort()
        {
            entities.Sort(depthCompare);
            isUnsorted = false;
        }

        public void MarkUnsorted()
        {
            isUnsorted = true;
        }

        public static Comparison<Entity> depthCompare = (a, b) =>
        {
            return Math.Sign(a.depth - b.depth);
        };

        public void Add(Entity entity)
        {
            if (!IsLocked)
            {
                if (!entities.Contains(entity))
                {
                    entities.Add(entity);
                    entity.Added(Scene);
                }
                else
                    Logger.Log("EntityList already contains this entity.");
            }
            else
            {
                if (!toAdd.Contains(entity) && !entities.Contains(entity))
                    toAdd.Add(entity);
                else
                    Logger.Log("EntityList already contains this entity.");
            }
        }

        public void Remove(Entity entity)
        {
            if (!IsLocked)
            {
                if (entities.Contains(entity))
                {
                    entities.Remove(entity);
                    entity.Removed(Scene);
                }
                else
                    Logger.Log("EntityList does not contain this entity.");
            }
            else
            {
                if (!toRemove.Contains(entity) && entities.Contains(entity))
                    toRemove.Add(entity);
                else
                    Logger.Log("EntityList does not contain this entity.");
            }
        }

        public void Add(IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
                Add(entity);
        }

        public void Add(params Entity[] entities)
        {
            foreach (var entity in entities)
                Add(entity);
        }

        public void Remove(IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
                Remove(entity);
        }

        public void Remove(params Entity[] entities)
        {
            foreach (var entity in entities)
                Remove(entity);
        }

        public void Clear()
        {
            entities.Clear();
            toAdd.Clear();
            toRemove.Clear();
        }

        internal void Update()
        {
            IsLocked = true;
            foreach (var item in entities)
                if (item.IsActive)
                    item.Update();
            IsLocked = false;
        }
        internal void Render()
        {
            IsLocked = true;
            foreach (var item in entities)
                if (item.IsVisible)
                    item.Render();
            IsLocked = false;
        }

        public IEnumerator<Entity> GetEnumerator()
        {
            return entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
