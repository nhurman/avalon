using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;

namespace Modelisation
{
    //Handle the blocked door state
    public class Door : TransformElement
    {
		protected Transform BlockedState;
		private bool isBlocked;
		private float countdown;
		public float waitTime = 3;

		protected new void Update () {
			countdown -= Time.deltaTime; 
			if (countdown <= 0.0f) {
				base.Update (); 
			}
		}
		void OnCollisionEnter (Collision col)
		{
			//add name of the character
			if(col.gameObject.name == "prop_powerCube")
			{
				countdown = waitTime;
				//isBlocked = true;
				//do stuff
			}
		}

    }
}
