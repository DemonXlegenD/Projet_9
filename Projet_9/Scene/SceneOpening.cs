using NEngine;
using NGlobal;
using NModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NScene
{
    internal class SceneOpening : SceneAbstract
    {
        public SceneOpening() : base("Scene Opening") { }

        public override void Launch()
        {
            base.Launch();
            System.Threading.Thread.Sleep(3000);
            Global.WriteSprites(new List<string> { "███████╗██╗   ██╗ ██████╗  █████╗ ███████╗ ██████╗██╗██╗","██╔════╝██║   ██║██╔═══██╗██╔══██╗██╔════╝██╔════╝██║██║","█████╗  ██║   ██║██║   ██║███████║███████╗██║     ██║██║","██╔══╝  ╚██╗ ██╔╝██║   ██║██╔══██║╚════██║██║     ██║██║","███████╗ ╚████╔╝ ╚██████╔╝██║  ██║███████║╚██████╗██║██║","╚══════╝  ╚═══╝   ╚═════╝ ╚═╝  ╚═╝╚══════╝ ╚═════╝╚═╝╚═╝","                                                        " }, 3,2);
            Global.WriteSprites(Global.ReadFilesText(Global.TXTGeneralPath + "\\Pokeball.txt"), 3);
            System.Threading.Thread.Sleep(3000);
            Console.Clear();

            Global.WriteSprites(new List<string> { "██╗  ██╗███████╗██╗     ██╗         ███████╗███╗   ██╗ ██████╗ ██╗███╗   ██╗███████╗","██║  ██║██╔════╝██║     ██║         ██╔════╝████╗  ██║██╔════╝ ██║████╗  ██║██╔════╝","███████║█████╗  ██║     ██║         █████╗  ██╔██╗ ██║██║  ███╗██║██╔██╗ ██║█████╗  ","██╔══██║██╔══╝  ██║     ██║         ██╔══╝  ██║╚██╗██║██║   ██║██║██║╚██╗██║██╔══╝  ","██║  ██║███████╗███████╗███████╗    ███████╗██║ ╚████║╚██████╔╝██║██║ ╚████║███████╗","╚═╝  ╚═╝╚══════╝╚══════╝╚══════╝    ╚══════╝╚═╝  ╚═══╝ ╚═════╝ ╚═╝╚═╝  ╚═══╝╚══════╝","                                                                                    " }, 3);
            Global.WriteSprites(Global.ReadFilesText(Global.TXTGeneralPath + "\\Dracaufeu.txt"), 3);
            System.Threading.Thread.Sleep(3000);
            Console.Clear();

            Engine.GetInstance().ModuleManager.GetModule<SceneModule>().SetScene<MenuScene>(true);
        }
    }
}
