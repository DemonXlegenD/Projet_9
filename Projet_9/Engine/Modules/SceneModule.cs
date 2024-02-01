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
        private List<SceneAbstract> scenes = new List<SceneAbstract>();
        private SceneAbstract mainScene = null;

        private TimeModule timeModule = null;
/*        private TimeModule timeModule = null;*/

        public SceneModule()
        {
        }

        public override void Init()
        {
            base.Init();
            timeModule = base.ModuleManager.GetModule<TimeModule>();
            this.SetScene<FightScene>();
            Console.WriteLine("salut");
        }
        public override void Start()
        {
            base.Start();
        }

        public override void Render()
        {
            base.Render();
            mainScene.Render();
        }

        public override void Update()
        {
            base.Update();
            //Console.WriteLine("Update");
            mainScene.Update(timeModule.GetDeltaTime());
        }

        public SceneAbstract SetScene<T>(bool replaceScenes = true) where T : SceneAbstract, new()
        {
            if (replaceScenes)
            {
               /* foreach (var scene in scenes)
                {
                    scene.Dispose(); // Assurez-vous d'implémenter IDisposable dans votre classe Scene
                }
                scenes.Clear();*/
            }

            SceneAbstract scene = new T();
            scenes.Add(scene);

            if (replaceScenes)
            {
                mainScene = scene;
            }
            return scene;
        }

        public SceneAbstract GetMainScene()
        {
            return mainScene;
        }

        public SceneAbstract GetScene(string sceneName)
        {
            return scenes.Find(scene => scene.Name == sceneName);
        }
    }
}
