using NGlobal;
using System;
using System.Collections.Generic;

namespace NUIElements
{
    public class UIButton
    {
        public string Name { get; set; }
        private event Action OnClear;
        private event Action OnClick;
        public bool IsHovered { get; set; } = false;

        public void AddEvent(Action newAction) { OnClick += newAction; }
        public UIButton(string name)
        {
            Name = name;
            OnClear += () => { Console.Clear(); };
        }

        public void Display()
        {
            if (IsHovered)
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Global.WriteSprites(new List<string> { "> " + Name + " <" }, 3);
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else { 
                Console.BackgroundColor = ConsoleColor.Black;
                Global.WriteSprites(new List<string> { "  " + Name + "  " }, 3);
            }
        }


        public void Click()
        {
            if (OnClick != null)
            {
                // Déclencher l'événement
                OnClick.Invoke();
            }
        }
        public void Clear()
        {
            if (OnClear != null)
            {
                // Déclencher l'événement
                OnClear.Invoke();
            }
        }
    }
}
