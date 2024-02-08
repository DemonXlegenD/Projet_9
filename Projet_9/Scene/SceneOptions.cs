using NEngine;
using NModules;
using NUIElements;
using System;
using NGlobal;
using System.Collections.Generic;

namespace NScene
{
    public class SceneOptions : SceneAbstract
    {

        private UISlider _slider = new UISlider("Volume");
        private SoundModule _soundModule;
        private bool _quit = false;
        public SceneOptions() : base("Scene Options") { }

        public override void Init() {
            base.Init(); 
            _soundModule = Engine.GetInstance().ModuleManager.GetModule<SoundModule>();
        }
        public override void Launch()
        {
            _quit = false;
            Global.WriteSprites(new List<string> { "██████  ██ ███████ ███    ██ ██    ██ ███████ ███    ██ ██    ██ ███████     ███████ ██    ██ ██████ ", "██   ██ ██ ██      ████   ██ ██    ██ ██      ████   ██ ██    ██ ██          ██      ██    ██ ██   ██", "██████  ██ █████   ██ ██  ██ ██    ██ █████   ██ ██  ██ ██    ██ █████       ███████ ██    ██ ██████ ", "██   ██ ██ ██      ██  ██ ██  ██  ██  ██      ██  ██ ██ ██    ██ ██               ██ ██    ██ ██   ██", "██████  ██ ███████ ██   ████   ████   ███████ ██   ████  ██████  ███████     ███████  ██████  ██   ██", "                                                                                                     " }, 3);
            Global.WriteSprites(new List<string> { "███████╗██╗   ██╗ ██████╗  █████╗ ███████╗ ██████╗██╗██╗", "██╔════╝██║   ██║██╔═══██╗██╔══██╗██╔════╝██╔════╝██║██║", "█████╗  ██║   ██║██║   ██║███████║███████╗██║     ██║██║", "██╔══╝  ╚██╗ ██╔╝██║   ██║██╔══██║╚════██║██║     ██║██║", "███████╗ ╚████╔╝ ╚██████╔╝██║  ██║███████║╚██████╗██║██║", "╚══════╝  ╚═══╝   ╚═════╝ ╚═╝  ╚═╝╚══════╝ ╚═════╝╚═╝╚═╝", "                                                        " }, 3, 2);
            Console.WriteLine("");
            Console.WriteLine("");
            Global.WriteSprites(new List<string> { "Options", "=======" }, 3);
            Console.WriteLine("");
            Console.WriteLine("");
            do
            {

                _slider.DisplayValueSlider();
                _soundModule.SetMainVolume(_slider.ValueSlider);
                HandleInput();
                Global.ClearLine();
            } while (!_quit);
            Console.Clear();
            Engine.GetInstance().ModuleManager.GetModule<SceneModule>().SetScene<MenuScene>();
        }
        private void HandleInput()
        {
            ConsoleKeyInfo key = Console.ReadKey();

            
            if (key.Key == ConsoleKey.LeftArrow)
            {
                _slider.AdjustValueSlider(Direction.Left);
            }
            else if (key.Key == ConsoleKey.RightArrow)
            {
                _slider.AdjustValueSlider(Direction.Right);
            }

            if (key.Key == ConsoleKey.Escape)
            {
                _quit = true;
            }
        }
    }
}
