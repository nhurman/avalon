using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;
using System;

[RequireComponent(typeof(CharacterMotor))]
public class MouseLeftRight : MonoBehaviour
{
	public string ReferenceNode = "WandNode";
	public float sensibility = 15.0f;

	void Start()
	{
		
		GameObject Wand = GameObject.Find("VRWand");
		
		if (Wand != null)
		{
			Wand.GetComponent<VRWandNavigation>().enabled = false;
			MiddleVRTools.Log("[ ] VRFPSInputController deactivated VRWandNavigation. Make sure you set the VR Root Node to the First Person Controller.");
		}
	}

	void FixedUpdate()
	{
		vrMouse mouse = null;
		float rotation = 0.0f;
		if (MiddleVR.VRDeviceMgr.GetMouse() != null)
		{
			mouse = MiddleVR.VRDeviceMgr.GetMouse();
		}

		float wandHorizontal = MiddleVR.VRDeviceMgr.GetWandHorizontalAxisValue();
		if (Math.Abs(wandHorizontal) > 0.1f)
		{
			rotation = wandHorizontal;
		}
		else if(Math.Abs(Input.GetAxis("MouseX")) > 0)
		{
			rotation = Input.GetAxis("MouseX");
		}

		transform.Rotate(0, rotation*sensibility, 0);
	}
}

