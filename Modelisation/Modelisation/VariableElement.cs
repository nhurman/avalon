using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    public abstract class VariableElement : Element
    {
        public double Min { get; protected set; }

        public double Max { get; protected set; }

        public double Step { get; set; }

        public VariableElement(double min, double max)
        {
            Min = min;
            Max = max;
            Step = (max - min) / 10;
        }

        public VariableElement(double min, double max, double step)
        {
            Min = min;
            Max = max;
            Step = step;
        }

        public void setValue(double value)
        {
            if (value >= Min && value <= Max)
            {
                bool auth = notifyGameManager();
                if (auth)
                {
                    ObjectProperty = value;
                }
                
            }
        }
        public override void setOn()
        {
            if (ObjectProperty <= Max)
            {
                bool auth = notifyGameManager();
                if (auth)
                {
                    ObjectProperty += Step;
                }
            }
        }
        public override void setOff()
        {
            if (ObjectProperty >= Min)
            {
                bool auth = notifyGameManager();
                if (auth)
                {
                    ObjectProperty -= Step;
                }
            }
        }
        
    }
}
