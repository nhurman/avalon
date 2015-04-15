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
        //public float Ratio;
        //public string OnValueName;
        //public string OffValueName;
		protected Transform OnValue;
		protected Transform OffValue;
		protected Transform objectToMove;
        private Transform target;

        private bool movement;
		public float smoothFactor = 1;
        
        //Generic would be better
        public override void setOn()
        {
            bool auth = notifyGameManager();
            if (auth)
            {
                target = OnValue;
                movement = true;
				Debug.Log (OnValue.position);
				Debug.Log (OnValue.localPosition);
				Debug.Log (OnValue.rotation);
				Debug.Log (OnValue.localRotation);
            }
        }
        public override void setOff()
        {
            bool auth = notifyGameManager();
            if (auth)
            {
                target = OffValue;
                movement = true;
                //Vector3 ratioVect = new Vector3(Ratio, Ratio, Ratio);
                //target = Vector3.Scale(ratioVect, (OnValue.position - OffValue.position)) + gameObject.transform.position;
            }
        }

        private void Start () {
            movement = false;
			OnValue = transform.Find ("position_on");
			OffValue = transform.Find ("position_off");
			objectToMove = transform.Find ("object");
			//orig = transform.lo

            //OnValue = transform.Find (OnValueName);
            //OffValue = transform.Find (OffValueName);
            target = OffValue;
        }  
        private void Update () {
            //Che
            if (movement)
            {
				if (gameObject.transform.position != target.position)
				{
					//Debug.Log("PIKA");
					objectToMove.position = Vector3.Lerp(objectToMove.position, target.position, Time.deltaTime * smoothFactor);
				}
				if (gameObject.transform.rotation != target.rotation)
				{
					//Debug.Log("CHU");
					objectToMove.rotation = Quaternion.Slerp(objectToMove.rotation, target.rotation, Time.deltaTime * smoothFactor);
				}
				if (objectToMove == target)
                {
                    movement = false;
                }
            }    
        }

        public override bool isOn()
        {
			return objectToMove == OnValue;
        }
        public override bool isOff()
        {
			return objectToMove == OffValue;
        }

        
    }
}
