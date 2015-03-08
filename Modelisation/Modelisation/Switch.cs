using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

namespace Modelisation
{
    public class Switch : SceneObject
    {
        public GameObject Target { get; set; }
        public Element Elem
        {
            get
            {
                return (Element)Target.GetComponent(typeof(Element));
            }
        }
    
        public void switchOn()
        {
            bool auth = notifyGameManager();
            if (auth)
            {
                Elem.setOn();
            } 
        }

        public void switchOff()
        {
            bool auth = notifyGameManager();
            if (auth)
            {
                Elem.setOff();
            } 
        }

        public void toggle()
        {
            //Elem.ObjectProperty
            throw new System.NotImplementedException();
        }
    }
}
