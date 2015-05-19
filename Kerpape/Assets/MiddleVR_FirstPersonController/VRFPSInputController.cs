/* VRFPSInputController
 * (c) i'm in VR
 */

using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;
using System;


public class VRFPSInputController : MonoBehaviour
{
    public string ReferenceNode     = "HandNode";
	public bool BlockInputs 		= false;
	public float forwardSensibility = 75.0f;
	public float strafeSensibility  = 75.0f;

    private bool  m_SearchedRefNode = false;
    private GameObject m_RefNode    = null;
    
	//verticalAngle will contain the vertical orientation of the wand
	private float verticalAngle		= 0.0f;

	private CharacterMotor motor	= null;
	private GameObject wand			= null;
	private GameObject head 		= null;

	//keyb will reference the keyboard and be used to check if movments keys are pressed
	private vrKeyboard keyb 		= null;

    // Use this for initialization
    void Start()
    {
		GameObject Wand = GameObject.Find("VRWand");
		head = GameObject.Find("HeadNode");

		if (m_RefNode == null)
			m_RefNode = GameObject.Find(ReferenceNode);

		if (Wand != null && Wand.GetComponent<VRWandNavigation>() != null)
        {
            //Wand.GetComponent<VRWandNavigation>().enabled = false;
            MVRTools.Log("[ ] VRFPSInputController deactivated VRWandNavigation. Make sure you set the VR Root Node to the First Person Controller.");
        }

	}
	
	// Update is called once per frame
    void Update()
    {
		//Affect keyb to the MiddleVR Keyboard
		keyb = MiddleVR.VRDeviceMgr.GetKeyboard();
		motor = GetComponent<CharacterMotor>();

		//if BlockInputs == true, we don't want the user to be able to move the camera by himself
		if(!BlockInputs){
			lookingUpDown();
			lookingLeftRight();
			moving(keyb, motor);
		}
    }

	void lookingUpDown()
	{
		//Apply the last registered verticalAngle to the head to whom the camera is linked
		head.transform.Rotate(Vector3.right, verticalAngle);
		//Get the wand vertical orientation
		float wandVertical = MiddleVR.VRDeviceMgr.GetWandVerticalAxisValue();
		
		//If the wand orientation is not in a neutral position (i.e. poiting perfectly toward us)
		// Modifying the last registered vertical angle according to the direction the Wand is pointing 
		if (Math.Abs( wandVertical ) > 0.1f)
		{
			//Modifying verticalAngle value 
			verticalAngle += wandVertical;
			//Putting it back to a threshold value in case it exceeded said threshold
			if(verticalAngle > 60) verticalAngle = 60;
			if(verticalAngle < -60) verticalAngle = -60;
		}
	}

	void lookingLeftRight()
	{
		//horizontalAngle will contain the horizontal orientation of the wand
		float horizontalAngle = 0.0f;
		//speed will be a modified value of horizontalAngle, depending of a given sensibility 
		float speedRotation = 0.0f;

		//Get the wand horizontal orientation
		float wandHorizontal = MiddleVR.VRDeviceMgr.GetWandHorizontalAxisValue();
		
		//If the wand orientation is not in a neutral position (i.e. poiting perfectly toward us)
		if (Math.Abs(wandHorizontal) > 0.1f)
		{
			//Modifying the horizontal angle according to the direction the Wand is pointing 
			horizontalAngle = wandHorizontal;
			//Modifying speedRotation to have a smoother rotation
			speedRotation = horizontalAngle * (float)MiddleVR.VRKernel.GetDeltaTime() * 50;
		}
		//Applying the computed rotation to the player
		transform.Rotate(Vector3.up, horizontalAngle);
	}

	void moving(vrKeyboard keyb, CharacterMotor motor)
	{
		//forward will be modified in case we push the up/down key of the keyboard
		float forward = 0.0f;
		//speed will be a modified value of forward, depending of a given sensibility 
		float forwardSpeed = 0.0f;
		float strafe = 0.0f;
		float strafeSpeed = 0.0f;

		//Get MiddleVR's keyboard inputs
		if (keyb.IsKeyPressed(MiddleVR.VRK_UP) || keyb.IsKeyPressed(MiddleVR.VRK_W))
		{
			forward = 1.0f;
		}
		if (keyb.IsKeyPressed(MiddleVR.VRK_DOWN) || keyb.IsKeyPressed(MiddleVR.VRK_S))
		{
			forward = -1.0f;
		}
		// Computing speed
		if (Math.Abs(forward) > 0.1) forwardSpeed = forward * (float)MiddleVR.VRKernel.GetDeltaTime() * forwardSensibility;

		//Get MiddleVR's keyboard inputs
		if (keyb.IsKeyPressed(MiddleVR.VRK_RIGHT) || keyb.IsKeyPressed(MiddleVR.VRK_D))
		{
			strafe = 1.0f;
		}
		
		if (keyb.IsKeyPressed(MiddleVR.VRK_LEFT) || keyb.IsKeyPressed(MiddleVR.VRK_A))
		{
			strafe = -1.0f;
		}
		// Computing speed
		if (Math.Abs(strafe) > 0.1) strafeSpeed = strafe * (float)MiddleVR.VRKernel.GetDeltaTime() * strafeSensibility;

		Vector3 directionVector = new Vector3(strafeSpeed, 0, forwardSpeed);
		motor.inputMoveDirection = m_RefNode.transform.TransformDirection(directionVector);
	}
}
