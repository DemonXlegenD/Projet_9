using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NComponents
{
    internal abstract class Components
    {
        private string _name = "components";

        public abstract void Update();

        public abstract void Render();

        public string Name { get { return _name; } set { _name = value; } }
    }
}
