using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;
using System;

public class MouseLeftRight : MonoBehaviour
{
	public float sensibility = 1.0f;

	/*void Start()
	{
		
		GameObject Wand = GameObject.Find("VRWand");
		
		if (Wand != null)
		{
			Wand.GetComponent<VRWandNavigation>().enabled = false;
			MiddleVRTools.Log("[ ] VRFPSInputController deactivated VRWandNavigation. Make sure you set the VR Root Node to the First Person Controller.");
		}
	}*/

	void FixedUpdate()
	{
		vrMouse mouse = null;
		float rotation = 0.0f;
		//Checking if MiddleVR is tracking the mouse
		if (MiddleVR.VRDeviceMgr.GetMouse() != null)
		{
			mouse = MiddleVR.VRDeviceMgr.GetMouse();
		}

		//we don't want to rely on the wand here
		/*float wandHorizontal = MiddleVR.VRDeviceMgr.GetWandHorizontalAxisValue();
		if (Math.Abs(wandHorizontal) > 0.1f)
		{
			rotation = wandHorizontal;
		}*/
		//GetAxisValue gives the position offset on the given axis since last update
		//0 means axis X
		if(Math.Abs(mouse.GetAxisValue(0)) > 0)
		{
			rotation = mouse.GetAxisValue(0);
		}
		else if(Math.Abs(Input.GetAxis("Mouse X")) > 0)
		{
			rotation = Input.GetAxis("Mouse X");
		}

		transform.Rotate(0, rotation*sensibility, 0);
	}
}

