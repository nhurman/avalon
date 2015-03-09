using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

namespace Modelisation
{
    //does not handle rotations ... yet.
    public class TransformElement : Element
    {
        //do nothing, will be used for partial movement
        public double Ratio;
        public string OnValueName;
        public string OffValueName;
        public Transform OnValue { get; set; }
        public Transform OffValue { get; set; }
        private Vector3 target;
        private bool movement;
        
        //Generic would be better
        public override void setOn()
        {
            bool auth = notifyGameManager();
            if (auth)
            {
                target = OnValue.position;
            }
        }
        public override void setOff()
        {
            bool auth = notifyGameManager();
            if (auth)
            {
                target = OffValue.position;
            }
        }

        void Start () {
            movement = false;
            OnValue = transform.Find (OnValueName);
            OffValue = transform.Find (OffValueName);
            target = OffValue.position;
        }  
        void Update () {
            if (movement)
            {
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target, Time.deltaTime);
            }

            
        }

        
    }
}
