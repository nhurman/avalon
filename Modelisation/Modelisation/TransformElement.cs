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
        public float Ratio;
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
                movement = true;
            }
        }
        public override void setOff()
        {
            bool auth = notifyGameManager();
            if (auth)
            {
                target = OffValue.position;
                movement = true;
                //Vector3 ratioVect = new Vector3(Ratio, Ratio, Ratio);
                //target = Vector3.Scale(ratioVect, (OnValue.position - OffValue.position)) + gameObject.transform.position;
            }
        }

        private void Start () {
            movement = false;
            OnValue = transform.Find (OnValueName);
            OffValue = transform.Find (OffValueName);
            target = OffValue.position;
        }  
        private void Update () {
            //Che
            if (movement)
            {
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target, Time.deltaTime);
                if (gameObject.transform.position == target)
                {
                    movement = false;
                }
            }    
        }

        public override bool isOn()
        {
            return gameObject.transform == OnValue;
        }
        public override bool isOff()
        {
            return gameObject.transform == OffValue;
        }

        
    }
}
