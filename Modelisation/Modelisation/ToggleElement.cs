using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    public abstract class ToggleElement : Element
    {

        public double OnValue { get; protected set; }

        public double OffValue { get; protected set; }
        
        //Generic would be better
        public ToggleElement(double on, double off)
        {
            OnValue = on;
            OffValue = off;
        }
    
       

        public override void setOn()
        {
            bool auth = notifyGameManager();
            if (auth)
            {
                ObjectProperty = OnValue;
            }
        }
        public override void setOff()
        {
            bool auth = notifyGameManager();
            if (auth)
            {
                ObjectProperty = OffValue;
            }
        }
    }
}
