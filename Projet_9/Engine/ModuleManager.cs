using NModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEngine
{ 

    public class ModuleManager
    {
        private List<Module> modules = new List<Module>();

        ~ModuleManager()
        {
            Release();
            modules.Clear();
        }

        public void CreateDefaultModules()
        {
            CreateModule<TimeModule>();
            CreateModule<InputModule>();
            CreateModule<WindowModule>();
            CreateModule<SceneModule>();
        }

        public void Init()
        {
            foreach (var module in modules)
            {
                module.Init();
            }
        }

        public void Start()
        {
            foreach (var module in modules)
            {
                module.Start();
            }
        }

        public void Update()
        {
            foreach (var module in modules)
            {
                module.Update();
            }
        }

        public void PreRender()
        {
            foreach (var module in modules)
            {
                module.PreRender();
            }
        }

        public void Render()
        {
            foreach (var module in modules)
            {
                module.Render();
            }
        }

        public void PostRender()
        {
            foreach (var module in modules)
            {
                module.PostRender();
            }
        }

        public void Release()
        {
            foreach (var module in modules)
            {
                module.Release();
            }
        }

        public void End()
        {
            foreach (var module in modules)
            {
                module.End();
            }
        }

        public T CreateModule<T>() where T : Module, new()
        {
            T module = new T();
            module.ModuleManager = this;
            modules.Add(module);
            return module;
        }

        public T GetModule<T>() where T : Module
        {
            foreach (var module in modules)
            {
                if (module is T moduleOfType)
                {
                    return moduleOfType;
                }
            }
            return null;
        }
    }
}
