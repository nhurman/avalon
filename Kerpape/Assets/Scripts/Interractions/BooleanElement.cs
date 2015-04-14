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
			gameObject.GetComponent(toDesactivate).gameObject.SetActive(true);
            //gameObject.GetComponent(toDesactivate).SetActive(true);
            //gameObject.SetActive(true);
        }

        public override void setOff()
        {
			//gameObject.GetComponent(toDesactivate).enabled = false;
            gameObject.GetComponent(toDesactivate).gameObject.SetActive(false);
            //gameObject.SetActive(false);
        }
        public override bool isOn()
        {
            return gameObject.GetComponent(toDesactivate).gameObject.activeSelf;
            //return gameObject.activeSelf;
        }
        public override bool isOff()
        {
            return !isOn();
        }
    }
}
