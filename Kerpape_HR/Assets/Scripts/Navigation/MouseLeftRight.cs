using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;
using System;

public class MouseLeftRight : MonoBehaviour
{
	public float sensibility = 1.0f;


	void FixedUpdate()
	{
		vrMouse mouse = null;
		float rotation = 0.0f; float rotVert = 0;
		//Checking if MiddleVR is tracking the mouse
		if (MiddleVR.VRDeviceMgr.GetMouse() != null)
		{
			mouse = MiddleVR.VRDeviceMgr.GetMouse();
		}
		GameObject cam = GameObject.Find ("Tete");

		//we don't want to rely on the wand here
		/*float wandHorizontal = MiddleVR.VRDeviceMgr.GetWandHorizontalAxisValue();
		if (Math.Abs(wandHorizontal) > 0.1f)
		{
			rotation = wandHorizontal;
		}*/
		//GetAxisValue gives the position offset on the given axis since last update
		//0 means axis X
		if(Math.Abs(mouse.GetAxisValue(0)) > 0.001f)
		{
			rotation = mouse.GetAxisValue(0);
		}
		else if(Math.Abs(Input.GetAxis("Mouse X")) > 0)
		{
			rotation = Input.GetAxis("Mouse X");
		}

		if (Math.Abs(mouse.GetAxisValue(1)) > 0.001f)
		{
			rotVert = mouse.GetAxisValue(1);
		}
		else if(Math.Abs(Input.GetAxis("Mouse Y")) > 0)
		{
			rotVert = Input.GetAxis("Mouse Y");
		}

		transform.Rotate(0, rotation*sensibility, 0, Space.World);
		transform.Rotate(rotVert*sensibility, 0, 0, Space.Self);

	}
}

