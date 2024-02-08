using System;
using System.Collections.Generic;
using NGlobal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace NUIElements
{
    public class UIPanel
    {
        public int selected { get; set; } = 0;
        public List<UIButton> Buttons { get; set; } = new List<UIButton>();

        public UIPanel()
        {
        }
        public UIPanel(List<UIButton> buttons) {
            Buttons = buttons;
        }

        public void ClearPanel()
        {
            Buttons.Clear();
        }

        public void AddButton(UIButton button)
        {
            if(Buttons.Count == 0)
            {
                button.IsHovered = true;
            }
            Buttons.Add(button);
        }

        public void SelectButton()
        {
            foreach (UIButton button in Buttons)
            {
                button.Display();
            }

            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.DownArrow)
            {
                selected++;
                if (selected == Buttons.Count)
                {
                    selected = 0;
                }
                foreach (UIButton button in Buttons)
                {
                    button.IsHovered = false;
                }
                Buttons[selected].IsHovered = true;
            }
            else if (key.Key == ConsoleKey.UpArrow)
            {
                selected--;
                if (selected < 0)
                {
                    selected = Buttons.Count - 1;
                }
                foreach (UIButton button in Buttons)
                {
                    button.IsHovered = false;
                }
                Buttons[selected].IsHovered = true;
            }
            else if (key.Key == ConsoleKey.Spacebar)
            {
                Buttons[selected].Click();
            }
            Global.ClearLines(Console.CursorTop - Buttons.Count);
        }

        public void SelectButton(bool clear = true)
        {
            foreach (UIButton button in Buttons)
            {
                button.Display();
            }

            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.DownArrow)
            {
                selected++;
                if (selected == Buttons.Count)
                {
                    selected = 0;
                }
                foreach (UIButton button in Buttons)
                {
                    button.IsHovered = false;
                }
                Buttons[selected].IsHovered = true;
            }
            else if (key.Key == ConsoleKey.UpArrow)
            {
                selected--;
                if (selected < 0)
                {
                    selected = Buttons.Count - 1;
                }
                foreach (UIButton button in Buttons)
                {
                    button.IsHovered = false;
                }
                Buttons[selected].IsHovered = true;
            }
            else if (key.Key == ConsoleKey.Spacebar)
            {
                Buttons[selected].Click();
            }
            if(clear) Global.ClearLines(Console.CursorTop - Buttons.Count);
        }
    }
}
