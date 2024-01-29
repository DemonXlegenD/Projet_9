using System;
using NComponents;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGameObject
{
    public class GameObject : IDisposable
    {
        protected string _name = string.Empty;
        protected List<Component> _components = new List<Component>();

        ~GameObject()
        {
            Dispose();
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public void Dispose()
        {
            foreach (var component in _components)
            {
                component.Dispose(); // Assuming you have implemented IDisposable in your Component class
            }
            _components.Clear();
        }

        public void AddComponent(Component component)
        {
            component.SetOwner(this);
            _components.Add(component);
        }

        public void RemoveComponent(Component component)
        {
            _components.Remove(component);
            component.Dispose(); // Assuming you have implemented IDisposable in your Component class
        }

        public void Update(float deltaTime)
        {
            foreach (var component in _components)
            {
                component.Update(deltaTime);
            }
        }

        public void Render()
        {
            foreach (var component in _components)
            {
                component.Render();
            }
        }
    }
}
