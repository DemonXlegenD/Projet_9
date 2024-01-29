using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEngine
{

    public abstract class Module
    {
        public ModuleManager ModuleManager { get; set; }

        public virtual void Init()
        {
            // Implement Init logic if needed
        }

        public virtual void Start()
        {
            // Implement Start logic if needed
        }

        public virtual void Update()
        {
            // Implement Update logic if needed
        }

        public virtual void PreRender()
        {
            // Implement PreRender logic if needed
        }

        public virtual void Render()
        {
            // Implement Render logic if needed
        }

        public virtual void PostRender()
        {
            // Implement PostRender logic if needed
        }

        public virtual void Release()
        {
            // Implement Release logic if needed
        }

        public virtual void End()
        {
            // Implement Finalize logic if needed
        }
    }
}
