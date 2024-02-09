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
            Engine.GetInstance().ModuleManager.GetModule<SoundModule>().Play("Intro");
            base.Launch();
            System.Threading.Thread.Sleep(1000);
            Global.WriteSprites(new List<string> { "███████╗██╗   ██╗ ██████╗  █████╗ ███████╗ ██████╗██╗██╗","██╔════╝██║   ██║██╔═══██╗██╔══██╗██╔════╝██╔════╝██║██║","█████╗  ██║   ██║██║   ██║███████║███████╗██║     ██║██║","██╔══╝  ╚██╗ ██╔╝██║   ██║██╔══██║╚════██║██║     ██║██║","███████╗ ╚████╔╝ ╚██████╔╝██║  ██║███████║╚██████╗██║██║","╚══════╝  ╚═══╝   ╚═════╝ ╚═╝  ╚═╝╚══════╝ ╚═════╝╚═╝╚═╝","                                                        " }, 3,2);
            Global.WriteSprites(Global.ReadFilesText(Global.TXTGeneralPath + "\\Pokeball.txt"), 3);
            System.Threading.Thread.Sleep(4000);
            Console.Clear();

            Global.WriteSprites(new List<string> { "██╗  ██╗███████╗██╗     ██╗         ███████╗███╗   ██╗ ██████╗ ██╗███╗   ██╗███████╗","██║  ██║██╔════╝██║     ██║         ██╔════╝████╗  ██║██╔════╝ ██║████╗  ██║██╔════╝","███████║█████╗  ██║     ██║         █████╗  ██╔██╗ ██║██║  ███╗██║██╔██╗ ██║█████╗  ","██╔══██║██╔══╝  ██║     ██║         ██╔══╝  ██║╚██╗██║██║   ██║██║██║╚██╗██║██╔══╝  ","██║  ██║███████╗███████╗███████╗    ███████╗██║ ╚████║╚██████╔╝██║██║ ╚████║███████╗","╚═╝  ╚═╝╚══════╝╚══════╝╚══════╝    ╚══════╝╚═╝  ╚═══╝ ╚═════╝ ╚═╝╚═╝  ╚═══╝╚══════╝","                                                                                    " }, 3);
            Global.WriteSprites(Global.ReadFilesText(Global.TXTGeneralPath + "\\Dracaufeu.txt"), 3);
            System.Threading.Thread.Sleep(4000);
            Console.Clear();
            System.Threading.Thread.Sleep(4000);
            Engine.GetInstance().ModuleManager.GetModule<SoundModule>().Stop("Intro");
            Engine.GetInstance().ModuleManager.GetModule<SceneModule>().SetScene<MenuScene>(true);
        }
    }
}
