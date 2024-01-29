using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEngine
{
    public class Engine
    {
        private static Engine instance;

        private ModuleManager moduleManager = new ModuleManager();

        private bool shouldQuit = false;
        
        public Engine() {
            moduleManager.CreateDefaultModules();
            moduleManager.Init();
        }           
        
        public static Engine GetInstance()
        {
            if (instance == null)
            {
                instance = new Engine();
            }
            return instance;
        }

        public void Run()
        {
            moduleManager.Start();
            while (!shouldQuit)
            {
                moduleManager.Update();
                moduleManager.PreRender();
                moduleManager.Render();
                moduleManager.PostRender();
            }

            moduleManager.Release();
            moduleManager.End();

        }

        public void Quit()
        {
            shouldQuit = true;
            Console.WriteLine("Application fermée.");
        }

        public ModuleManager ModuleManager { get { return moduleManager; } }
    }
}
