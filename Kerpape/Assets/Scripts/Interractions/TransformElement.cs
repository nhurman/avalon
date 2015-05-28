using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

namespace Modelisation
{
    //does not handle rotations ... yet.
	/// <summary>
	/// Class that handle object transformations.
	/// The gameobject must have this structure :
	/// -- gameobject directory with this script attached named correctly.
	/// -- -- the actual object named "object".
	/// -- -- a transform object named "position_on". It define the state the object should have when activated.
	/// -- -- a transform object named "position_off". It define the state the object should have when desactivated.
	/// </summary>
    public class TransformElement : Element
    {
		//private AffichageSymbolique affichageSymbolique;

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
		//public float partMovement = 100;
        
        public override void autonomous_setOn()
        {	
			Debug.Log("on " +  isOn() + " " + isOff());
            target = OnValue;
            movement = true;
			/*Debug.Log (OnValue.position);
			Debug.Log (OnValue.localPosition);
			Debug.Log (OnValue.rotation);
			Debug.Log (OnValue.localRotation);*/
        }
		public override void autonomous_setOff()
   		{
			Debug.Log("off " +  isOn() + " " + isOff());
            target = OffValue;
            movement = true;
            //Vector3 ratioVect = new Vector3(Ratio, Ratio, Ratio);
            //target = Vector3.Scale(ratioVect, (OnValue.position - OffValue.position)) + gameObject.transform.position;
        }
		public override void symbolic_setOff (){
			//affichageSymbolique.activer ();
		}
		
		public override void symbolic_setOn (){
			//affichageSymbolique.activer ();
		}

		protected override void Start () {
			base.Start();
			//affichageSymbolique = gameObject.AddComponent<AffichageSymbolique> ();
            movement = false;
			OnValue = transform.Find ("position_on");
			OffValue = transform.Find ("position_off");
			objectToMove = transform.Find ("object");
			//orig = transform.lo

            //OnValue = transform.Find (OnValueName);
            //OffValue = transform.Find (OffValueName);
            target = OffValue;
        }  

		// protected might not work, bc private by default
        protected override void Update () {
			//Debug.Log (objectToMove.localPosition == OnValue.localPosition);
            if (movement)
            {
				if (objectToMove.localPosition != target.localPosition)
				{
					objectToMove.localPosition = Vector3.Lerp(objectToMove.localPosition, target.localPosition, Time.deltaTime * smoothFactor);
				}
				if (objectToMove.localRotation != target.localRotation)
				{
					objectToMove.localRotation = Quaternion.Slerp(objectToMove.localRotation, target.localRotation, Time.deltaTime * smoothFactor);
				}
				if (objectToMove.localScale != target.localScale)
				{
					objectToMove.localScale = Vector3.Lerp (objectToMove.localScale, target.localScale, Time.deltaTime * smoothFactor);
				}

				if (objectToMove == target)
                {
                    movement = false;
                }
            }    
        }

		/// <summary>
		/// Test if 2 transformations are equals.
		/// </summary>
		/// <param name="t1">a transform object</param>
		/// <param name="t2">another transform object</param>
		/// <returns>Bool : true if equals</returns>
		private static bool equalTransform(Transform t1, Transform t2)
		{
			return t1.localPosition == t2.localPosition && t1.localRotation == t2.localRotation && t1.localScale == t2.localScale;
				//Vector3.Distance(t1.localPosition, t2.localPosition) < 0.0
		}

        public override bool isOn()
        {
			return equalTransform (objectToMove, OnValue);
        }

        public override bool isOff()
        {
			return equalTransform (objectToMove, OffValue);
        }

        
    }
}
