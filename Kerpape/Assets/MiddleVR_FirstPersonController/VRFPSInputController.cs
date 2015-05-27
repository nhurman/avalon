/* VRFPSInputController
 * (c) i'm in VR
 */

using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;
using System;


public class VRFPSInputController : MonoBehaviour
{
    public string ReferenceNode   			= "HandNode";
	public bool BlockInputs 				= false;
	public float Sensibility 				= 2.0f;

    private bool  m_SearchedRefNode			= false;
    private GameObject m_RefNode   			= null;
    	
	//verticalAngle will contain the vertical orientation of the wand
	public float verticalAngle				= 0.0f;

	private GameObject head 				= null;
	private CharacterController controller 	= null;
	private GameObject wand 				= null;

	//keyb will reference the keyboard and be used to check if movments keys are pressed
	private vrKeyboard keyb 				= null;

	private WandOnlyController wController	= null;

    // Use this for initialization
    void Start()
    {
		head = GameObject.Find("HeadNode");
		wand = GameObject.Find("VRWand");
		controller = GetComponent<CharacterController>();
		wController = GetComponent<WandOnlyController>();
		if (m_RefNode == null)
			m_RefNode = GameObject.Find(ReferenceNode);
	}

	// Update is called once per frame
    void Update(){


		//Affect keyb to the MiddleVR Keyboard
		if (keyb == null)
			keyb = MiddleVR.VRDeviceMgr.GetKeyboard();

		if (keyb.IsKeyToggled(MiddleVR.VRK_R)){
			invertBlockInput();
		}

		//if BlockInputs == true, we don't want the user to be able to move the camera by himself
		//if(!BlockInputs){
			lookingUpDown();
			lookingLeftRight();
		//}
		moving(keyb);
    }

	void lookingUpDown()
	{
		if (!BlockInputs) {
			//Get the wand vertical orientation
			float wandVertical = MiddleVR.VRDeviceMgr.GetWandVerticalAxisValue ();

			//If the wand orientation is not in a neutral position (i.e. poiting perfectly toward us)
			// Modifying the last registered vertical angle according to the direction the Wand is pointing 
			if (Math.Abs (wandVertical) > 0.1f) {
				//Modifying verticalAngle value 
				verticalAngle += wandVertical;
				//Putting it back to a threshold value in case it exceeded said threshold
				if (verticalAngle > 60f)
					verticalAngle = 60f;
				if (verticalAngle < -60f)
					verticalAngle = -60f;
			}
		}
		//Applying the computed rotation to the player
		head.transform.Rotate(Vector3.right, verticalAngle);
		
		wand.transform.position = head.transform.position;
		wand.transform.rotation = head.transform.rotation;
		wand.transform.Translate (0, 0, -0.5f);
	}

	void lookingLeftRight()
	{
		//horizontalAngle will contain the horizontal orientation of the wand
		float horizontalAngle = 0.0f;
		if(!BlockInputs) {
			//Get the wand horizontal orientation
			float wandHorizontal = MiddleVR.VRDeviceMgr.GetWandHorizontalAxisValue();
			
			//If the wand orientation is not in a neutral position (i.e. poiting perfectly toward us)
			if (Math.Abs(wandHorizontal) > 0.1f)
			{
				//Modifying the horizontal angle according to the direction the Wand is pointing 
				horizontalAngle = wandHorizontal;
			}
		}
		//Applying the computed rotation to the player
		transform.Rotate(Vector3.up, horizontalAngle);
	}

	void moving(vrKeyboard keyb)
	{
		//forward will be modified in case we push the up/down key of the keyboard
		float forward = 0.0f;
		//speed will be a modified value of forward, depending of a given sensibility 
		float strafe = 0.0f;

		//Get MiddleVR's keyboard inputs
		if (keyb.IsKeyPressed(MiddleVR.VRK_UP) || keyb.IsKeyPressed(MiddleVR.VRK_W))
		{
			forward = 1.0f;
			//unLockCamera();
		}
		if (keyb.IsKeyPressed(MiddleVR.VRK_DOWN) || keyb.IsKeyPressed(MiddleVR.VRK_S))
		{
			forward = -1.0f;
			//unLockCamera();
		}

		//Get MiddleVR's keyboard inputs
		if (keyb.IsKeyPressed(MiddleVR.VRK_RIGHT) || keyb.IsKeyPressed(MiddleVR.VRK_D))
		{
			strafe = 1.0f;
			//unLockCamera();
		}
		
		if (keyb.IsKeyPressed(MiddleVR.VRK_LEFT) || keyb.IsKeyPressed(MiddleVR.VRK_A))
		{
			strafe = -1.0f;
			//unLockCamera();
		}

		Vector3 directionVector = new Vector3(strafe, 0, forward)*Sensibility;
		controller.SimpleMove(m_RefNode.transform.TransformDirection(directionVector));
	}


	public void lockCamera(){
		BlockInputs = true;
		if (wController != null) {
			wController.UnlockWand();
		}
	}

	public void unLockCamera(){
		BlockInputs = false;
		if (wController != null) {
			wController.LockWand ();
		}
	}

	public void invertBlockInput(){
		BlockInputs = ! BlockInputs;

		if (wController != null) {
			wController.invertWandLock();
		}
	}
}
