using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;
using System;

[RequireComponent(typeof(CharacterMotor))]
public class KeyboardNavigation : MonoBehaviour
{
	public string ReferenceNode = "WandNode";

	private bool  m_SearchedRefNode = false;

	// Use this for initialization
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
		CharacterMotor motor = GetComponent<CharacterMotor>();
		
		vrKeyboard keyb = null;
		
		if (MiddleVR.VRDeviceMgr.GetKeyboard() != null)
		{
			keyb = MiddleVR.VRDeviceMgr.GetKeyboard();
		}
		
		GameObject refNode = GameObject.Find(ReferenceNode);
		
		float speed = 0.0f;
		float speedR = 0.0f;
		float forward = 0.0f;
		
		// Choosing active vertical axis
		
		// First test Unity's inputs
		if (Math.Abs(Input.GetAxis("Vertical")) > 0)
		{
			forward = Input.GetAxis("Vertical");
		}
		
		// Then test MiddleVR's keyboard
		if (keyb != null)
		{
			if (keyb.IsKeyPressed(MiddleVR.VRK_UP) || keyb.IsKeyPressed(MiddleVR.VRK_Z))
			{
				forward = 1.0f;
			}
			
			if (keyb.IsKeyPressed(MiddleVR.VRK_DOWN) || keyb.IsKeyPressed(MiddleVR.VRK_S))
			{
				forward = -1.0f;
			}
		}
		
		// Computing speed
		if (Math.Abs(forward) > 0.1) speed = forward * Time.deltaTime * 30;
		
		//print("Speed: " + speed);
		
		
		// Choosing active horizontal axis
		float rotation = 0.0f;
		
		// First test Unity's inputs
		if (Math.Abs(Input.GetAxis("Horizontal")) > 0)
		{
			rotation = Input.GetAxis("Horizontal");
		}
		
		// Then test MiddleVR's keyboard
		if (keyb != null)
		{
			if (keyb.IsKeyPressed(MiddleVR.VRK_LEFT) || keyb.IsKeyPressed(MiddleVR.VRK_Q))
			{
				rotation = -1.0f;
			}
			
			if (keyb.IsKeyPressed(MiddleVR.VRK_RIGHT) || keyb.IsKeyPressed(MiddleVR.VRK_D))
			{
				rotation = 1.0f;
			}
		}

		if (Math.Abs(rotation) > 0.1) speedR = rotation * Time.deltaTime * 50;
		
		Vector3 directionVector = new Vector3(speedR, 0, speed);

		if (refNode == null)
		{
			if (m_SearchedRefNode == false)
			{
				MiddleVRTools.Log("[ ] Didn't find reference node " + ReferenceNode);
				m_SearchedRefNode = true;
			}
			
			motor.inputMoveDirection = directionVector;
		}
		else
		{
			motor.inputMoveDirection = refNode.transform.TransformDirection(directionVector);
			
			//print(motor.inputMoveDirection);
		}
		
		bool jump = false;
		
		if (Input.GetButton("Jump") == true)
		{
			jump = true;
		}
		
		if (keyb != null)
		{
			if (keyb.IsKeyPressed(MiddleVR.VRK_SPACE))
			{
				jump = true;
			}
		}
		
		/*if (MiddleVR.VRDeviceMgr.IsWandButtonToggled(1) == true)
		{
			jump = true;
		}*/
		
		motor.inputJump = jump;
	}
	
	
}