using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

namespace Modelisation
{
    public abstract class Element : SceneObject
    {
        public double ObjectProperty { get; set; }
        //Should be between 0..1


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

        public abstract bool isOn();
        public abstract bool isOff();
    }
}
