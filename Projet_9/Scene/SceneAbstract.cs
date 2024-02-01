using NGameObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NScene
{
    public abstract class SceneAbstract
    {
        protected string _name = string.Empty;
        protected List<GameObject> _gameObjects = new List<GameObject>();

        public SceneAbstract(string name)
        {
            this._name = name;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public virtual void Init()
        {
        }

        public virtual void Update(float deltaTime)
        {
            foreach (var gameObject in _gameObjects)
            {
                gameObject.Update(deltaTime);
            }
        }

        public virtual void Render()
        {
            foreach (var gameObject in _gameObjects)
            {
                gameObject.Render();
            }
        }

        public GameObject CreateGameObject(string object_name)
        {
            GameObject gameObject = new GameObject();
            gameObject.Name  = object_name;
            _gameObjects.Add(gameObject);
            return gameObject;
        }

        public void DestroyGameObject(GameObject gameObject)
        {
            _gameObjects.Remove(gameObject);
            gameObject.Dispose(); // Assuming you have implemented IDisposable in your GameObject class
        }

        public GameObject FindGameObject(string object_name)
        {
            return _gameObjects.Find(obj => obj.Name == object_name);
        }
    }
}
