using Map;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSave
{
    public class Save
    {
        static Save instance;

        private string _name = "Save";

        public string Name {  
            get { return _name; } 
            set { _name = value; }
        }

        public Save() { }

        public static Save GetInstance()
        {
            if(instance == null)
            {
                instance = new Save();
            }
            return instance;
        }

        public void ApplySave(Save save)
        {
            
        }
    }
}
