using NEngine;
using NModules;
using System;
using System.Collections.Generic;

namespace NScene
{
    internal class SceneCredit : SceneAbstract
    {

        private int _indexPerson = 0;
        private int maxNumberCharShow = 50;
        private int indexChar = 0;
        private int MaxIndexChar = 0;
        private List<string> ResultJarod = new List<string>();
        private List<string> ResultMaurad = new List<string>();
        private List<string> ResultFrancois = new List<string>();
        public SceneCredit() : base("Scene Credit")
        {
        }

        public override void Init()
        {
            Engine.GetInstance().ModuleManager.GetModule<SoundModule>().Play("Credit", true);
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            ResultJarod.Add("       _|                                      _|        _|_|_|                                _|                                ");
            ResultJarod.Add("       _|    _|_|_|  _|  _|_|    _|_|      _|_|_|      _|        _|    _|  _|  _|_|  _|  _|_|        _|_|    _|  _|_|    _|_|    ");
            ResultJarod.Add("       _|  _|    _|  _|_|      _|    _|  _|    _|      _|        _|    _|  _|_|      _|_|      _|  _|_|_|_|  _|_|      _|_|_|_|  ");
            ResultJarod.Add(" _|    _|  _|    _|  _|        _|    _|  _|    _|      _|        _|    _|  _|        _|        _|  _|        _|        _|        ");
            ResultJarod.Add("   _|_|      _|_|_|  _|          _|_|      _|_|_|        _|_|_|    _|_|_|  _|        _|        _|    _|_|_|  _|          _|_|_|  ");

            MaxIndexChar = ResultJarod[0].Length - 1;
            indexChar = MaxIndexChar;

            ResultMaurad.Add(" _|      _|                                                _|      _|_|_|              _|                  _|  ");
            ResultMaurad.Add(" _|_|  _|_|    _|_|_|  _|    _|  _|  _|_|    _|_|_|    _|_|_|      _|    _|    _|_|_|  _|_|_|    _|  _|_|      ");
            ResultMaurad.Add(" _|  _|  _|  _|    _|  _|    _|  _|_|      _|    _|  _|    _|      _|_|_|    _|    _|  _|    _|  _|_|      _|  ");
            ResultMaurad.Add(" _|      _|  _|    _|  _|    _|  _|        _|    _|  _|    _|      _|    _|  _|    _|  _|    _|  _|        _|  ");
            ResultMaurad.Add(" _|      _|    _|_|_|    _|_|_|  _|          _|_|_|    _|_|_|      _|_|_|      _|_|_|  _|    _|  _|        _|  ");

            ResultFrancois.Add(" _|_|_|_|                                                    _|                _|                  _|                  _|  _|            ");
            ResultFrancois.Add(" _|        _|  _|_|    _|_|_|  _|_|_|      _|_|_|    _|_|          _|_|_|      _|          _|_|_|  _|_|_|      _|_|_|  _|  _|    _|_|    ");
            ResultFrancois.Add(" _|_|_|    _|_|      _|    _|  _|    _|  _|        _|    _|  _|  _|_|          _|        _|    _|  _|    _|  _|    _|  _|  _|  _|_|_|_|  ");
            ResultFrancois.Add(" _|        _|        _|    _|  _|    _|  _|        _|    _|  _|      _|_|      _|        _|    _|  _|    _|  _|    _|  _|  _|  _|        ");
            ResultFrancois.Add(" _|        _|          _|_|_|  _|    _|    _|_|_|    _|_|    _|  _|_|_|        _|_|_|_|    _|_|_|  _|    _|    _|_|_|  _|  _|    _|_|_|  ");
        }
        public override void Launch()
        {
            Console.Clear();
            base.Launch();
            switch (_indexPerson)
            {
                case 0:
                    WriteLine(ResultJarod);
                    break;
                case 1:
                    WriteLine(ResultMaurad);
                    break;
                case 2:
                    WriteLine(ResultFrancois);
                    break;
                default:
                    Console.WriteLine("Ok");
                    break;
            }
            ChangeIndex();
            System.Threading.Thread.Sleep(20);
            HandleInput();

        }

        public void HandleInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Spacebar || key.Key == ConsoleKey.Escape || key.Key == ConsoleKey.Enter)
                {
                    Engine.GetInstance().ModuleManager.GetModule<SoundModule>().StopAll();
                    Engine.GetInstance().ModuleManager.GetModule<SceneModule>().SetScene<MenuScene>();
                }
            }

        }
        public void WriteLine(List<string> name)
        {

            foreach (string line in name)
            {
                Console.WriteLine(line.Substring(indexChar, MaxIndexChar - indexChar + 1));
            }
        }

        public void ChangeIndex()
        {
            if (MaxIndexChar - indexChar == maxNumberCharShow)
            {
                MaxIndexChar--;
                indexChar--;
            }
            else if (indexChar == 0)
            {
                MaxIndexChar--;
            }
            else
            {
                indexChar--;
            }

            if (MaxIndexChar < 0)
            {
                MaxIndexChar = 0;
            }
            if (indexChar < 0)
            {
                indexChar = 0;
            }

            if (MaxIndexChar == 0 && indexChar == 0)
            {
                _indexPerson++;
                switch (_indexPerson)
                {
                    case 0:
                        MaxIndexChar = ResultJarod[0].Length - 1;

                        break;
                    case 1:
                        MaxIndexChar = ResultMaurad[0].Length - 1;
                        break;
                    case 2:
                        MaxIndexChar = ResultFrancois[0].Length - 1;
                        break;
                    default:
                        _indexPerson = 0;
                        break;
                }
                indexChar = MaxIndexChar;
            }
        }
    }
}
