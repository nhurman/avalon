using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    public abstract class Element : SceneObject
    {
        public int ObjectProperty
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public abstract void setOn();

        public abstract void setOff();

        public void autonomousAction()
        {
            throw new System.NotImplementedException();
        }

        public void assistedAction()
        {
            throw new System.NotImplementedException();
        }

        public void symbolicAction()
        {
            throw new System.NotImplementedException();
        }
    }
}
