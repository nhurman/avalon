using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

namespace Modelisation
{
    //A class for on/off object
    public class BooleanElement : Element
    {
        public string toDesactivate;
        public override void setOn()
        {
            gameObject.getComponent(toDesactivate).SetActive(true);
            //gameObject.SetActive(true);
        }

        public override void setOff()
        {
            gameObject.getComponent(toDesactivate).SetActive(false);
            //gameObject.SetActive(false);
        }
        public override bool isOn()
        {
            return gameObject.getComponent(toDesactivate).activeSelf;
            //return gameObject.activeSelf;
        }
        public override bool isOff()
        {
            return !isOn();
        }
    }
}
