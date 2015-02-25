using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    public abstract class ToggleElement : Element
    {
        //Generic would be better
        public ToggleElement(double on, double off)
        {
            OnValue = on;
            OffValue = off;
        }
    
        public double OnValue
        {
            get;
            protected set;
        }

        public double OffValue
        {
            get;
            protected set;
        }

        public override void setOn()
        {
            //notifyGameManager()
            //ObjectProperty = OnValue;
        }
        public override void setOff()
        {
            //notifyGameManager()
            //ObjectProperty = OffValue;
        }
    }
}
