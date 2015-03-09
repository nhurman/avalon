using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    //A class for on/off object
    public class BooleanElement : Element
    {
        public override void setOn()
        {
            gameObject.SetActive(true);
        }

        public override void setOff()
        {
            gameObject.SetActive(false);
        }
    }
}
