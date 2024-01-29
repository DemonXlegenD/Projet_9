using NEngine;
using NScene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NModules
{

    public sealed class SceneModule : Module
    {
        private List<Scene> scenes = new List<Scene>();
        private Scene mainScene = null;

        private WindowModule windowModule = null;
/*        private TimeModule timeModule = null;*/

        public SceneModule()
        {
            // Initialisation du module de scène
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Render()
        {
            base.Render();
        }

        public override void Update()
        {
            base.Update();
        }

        public Scene SetScene<T>(bool replaceScenes = true) where T : Scene, new()
        {
            if (replaceScenes)
            {
               /* foreach (var scene in scenes)
                {
                    scene.Dispose(); // Assurez-vous d'implémenter IDisposable dans votre classe Scene
                }
                scenes.Clear();*/
            }

            Scene scene = new T();
            scenes.Add(scene);

            if (replaceScenes)
            {
                mainScene = scene;
            }

            return scene;
        }

        public Scene GetMainScene()
        {
            return mainScene;
        }

        public Scene GetScene(string sceneName)
        {
            return scenes.Find(scene => scene.Name == sceneName);
        }
    }
}
