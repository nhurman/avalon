using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    public class Switch : SceneObject
    {
        public Element Elem
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    
        public void switchOn()
        {
            //this need to notify game manager
            Elem.setOn();
        }

        public void switchOff()
        {
            //this need to notify game manager
            Elem.setOff();
        }

        public void toggle()
        {
            //Elem.ObjectProperty
            throw new System.NotImplementedException();
        }
    }
}
