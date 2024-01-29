using NEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NModules
{

    public sealed class WindowModule : Module
    {
        public int WindowWidth => Console.WindowWidth;
        public int WindowHeight => Console.WindowHeight;

        public override void Init()
        {
            base.Init();    
        }

        public override void Start()
        {
            base.Start();   
        }

        public override void Update()
        {
            base.Update();
        }

        public override void PreRender()
        {
            base.PreRender();   
            Console.Clear();
        }

        public override void Render()
        {
            base.Render();
        }

        public override void PostRender()
        {
            base.PostRender();
        }

        public override void Release()
        {
            base.Release();
        }
    }
}
