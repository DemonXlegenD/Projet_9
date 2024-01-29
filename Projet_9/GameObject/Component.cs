using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using NGameObject;

namespace NComponents
{
    public class Component : IDisposable
    {
        protected string _name = string.Empty;
        protected GameObject _owner;

        public Component()
        {
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Component(GameObject owner)
        {
            this._owner = owner;
        }

        public void Dispose()
        {
            // Dispose logic if needed
        }

        public virtual void Update(float deltaTime)
        {
        }

        public virtual void Render()
        {
        }

        public GameObject GetOwner()
        {
            return _owner;
        }

        public void SetOwner(GameObject owner)
        {
            this._owner = owner;
        }
    }

}
