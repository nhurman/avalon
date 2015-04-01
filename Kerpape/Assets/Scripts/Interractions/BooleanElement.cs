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
        public override void setOn()
        {
            gameObject.SetActive(true);
        }

        public override void setOff()
        {
            gameObject.SetActive(false);
        }
        public override bool isOn()
        {
            return gameObject.activeSelf;
        }
        public override bool isOff()
        {
            return !isOn();
        }
    }
}
