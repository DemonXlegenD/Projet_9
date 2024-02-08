using System;
using System.Collections.Generic;
using NGlobal;

namespace NUIElements
{
    public enum Direction {  Left, Right }
    public class UISlider
    {
        private string _name;
        private float valueslider;
        private float increment;


        public UISlider(string name, float initialValueSlider = 0.5f, float increment = 0.1f)
        {
            _name = name;
            if (initialValueSlider < 0.0f || initialValueSlider > 1.0f)
            {
                throw new ArgumentOutOfRangeException(nameof(initialValueSlider), "Le valueslider initial doit être compris entre 0.0 et 1.0.");
            }

            if (increment <= 0.0f || increment >= 1.0f)
            {
                throw new ArgumentOutOfRangeException(nameof(increment), "L'incrément de valueslider doit être compris entre 0.0 et 1.0.");
            }

            this.valueslider = initialValueSlider;
            this.increment = increment;
        }

        public float ValueSlider
        {
            get { return valueslider; }
            set
            {
                if (value < 0.0f)
                {
                    valueslider = 0.0f;
                }
                else if (value > 1.0f)
                {
                    valueslider = 1.0f;
                }
                else
                {
                    valueslider = value;
                }
            }
        }

        public void AdjustValueSlider(Direction direction = Direction.Left)
        {
            if (direction == Direction.Left)
            {
                ValueSlider -= increment;
            }
            else
            {
                ValueSlider += increment;
            }
        }

        public void DisplayValueSlider()
        {
            int barLength = 20;
            int filledLength = (int)Math.Round(valueslider * barLength);
            string valuesliderBar = new string('=', filledLength) + new string('-', barLength - filledLength);
            string space  = valueslider == 0 ? " " : string.Empty;
            Global.WriteSprites(new List<string> { $"[{_name}] |{valuesliderBar}| {space}{valueslider:P}" }, 3);
        }
    }
}
