/* VRFPSInputController
 * (c) i'm in VR
 */

using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;
using System;

///<summary>
///Handles user movments with keyboard and mouse. 
///</summary>
public class VRFPSInputController : MonoBehaviour
{
    public string ReferenceNode   				= "HandNode";
	public bool BlockInputs 					= false;		//set to true when the user is not allowed to move
	public float Sensibility 					= 3.0f;		//float used to modify the input's values and thus movment speed
	public float lookSensibility 				= 1.0f;

    private bool  m_SearchedRefNode				= false;
    private GameObject m_RefNode   				= null;
    	
	public float verticalAngle					= 0.0f;			//verticalAngle will contain the vertical orientation of the wand

	private GameObject head 					= null;			//embodies the user's head, used for camera movments 
	private CharacterController charController 	= null;			//object attached to the user avatar "Utilisateur" that provides movment functions
	private GameObject wand 					= null;

	private vrKeyboard keyb 					= null;			//keyb will reference the keyboard and be used to check if movments keys are pressed

	private WandOnlyController wandController	= null;			//a script that controls the wand, i.e the cursor, movments when the inputs are blocked

    void Start()
    {
		Cursor.visible = false;
		head = GameObject.Find("HeadNode");
		wand = GameObject.Find("VRWand");
		charController = GetComponent<CharacterController>();
		wandController = GetComponent<WandOnlyController>();
		if (m_RefNode == null)
			m_RefNode = GameObject.Find(ReferenceNode);
	}

    void Update()
    {
		//Affect keyb to the MiddleVR Keyboard
		if (keyb == null)
			keyb = MiddleVR.VRDeviceMgr.GetKeyboard();
		lookingUpDown();
		lookingLeftRight();
		moving(keyb);
    }

    ///<summary>
	///Allows the user to rotate the head, embodied by HeadNode, on a vertical axis
	///</summary>
	void lookingUpDown()
	{
		if (!BlockInputs) {
			//Get the wand vertical offset
			//float wandVertical = MiddleVR.VRDeviceMgr.GetWandVerticalAxisValue ();
			float wandVertical = MiddleVR.VRDeviceMgr.GetMouseAxisValue(1);

			//If the wand orientation is not in a neutral position (i.e. poiting perfectly toward us)
			// Modifying the last registered vertical angle according to the direction the Wand is pointing 
			if (Math.Abs (wandVertical) > 0.1f) {
				//Modifying verticalAngle value ; we consider that wandVertical is an angle even though it's an offset
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

	///<summary>
	///Allows the user to rotate the body on a horizontal axis
	///</summary>
	void lookingLeftRight()
	{
		//horizontalAngle will contain the horizontal offset of the wand
		float horizontalAngle = 0.0f;
		if(!BlockInputs) {
			//Get the wand horizontal orientation
			//float wandHorizontal = MiddleVR.VRDeviceMgr.GetWandHorizontalAxisValue();
			float wandHorizontal = MiddleVR.VRDeviceMgr.GetMouseAxisValue(0);
			
			//If the wand orientation is not in a neutral position (i.e. poiting perfectly toward us)
			if (Math.Abs(wandHorizontal) > 0.1f)
			{
				//Modifying the horizontal angle according to the direction the Wand is pointing 
				horizontalAngle = wandHorizontal;
			}
		}
		//Applying the computed rotation to the player
		transform.Rotate(Vector3.up, horizontalAngle*lookSensibility);
	}

	///<summary>
	///Allows the user to move his avatar forward, backward, left and right. 
	///</summary>	
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
		}
		if (keyb.IsKeyPressed(MiddleVR.VRK_DOWN) || keyb.IsKeyPressed(MiddleVR.VRK_S))
		{
			forward = -1.0f;
		}

		//Get MiddleVR's keyboard inputs
		if (keyb.IsKeyPressed(MiddleVR.VRK_RIGHT) || keyb.IsKeyPressed(MiddleVR.VRK_D))
		{
			strafe = 1.0f;
		}
		
		if (keyb.IsKeyPressed(MiddleVR.VRK_LEFT) || keyb.IsKeyPressed(MiddleVR.VRK_A))
		{
			strafe = -1.0f;
		}
		Vector3 directionVector = new Vector3(strafe, 0, forward)*Sensibility;
		charController.SimpleMove(m_RefNode.transform.TransformDirection(directionVector));
	}

	///<summary>
	///Blocks all movments but allows user to move the wand (i.e. the cursor)
	///</summary>	
	public void lockCamera(){
		BlockInputs = true;
		if (wandController != null) {
			wandController.UnlockWand();
		}
	}

	///<summary>
	///Unblocks movments
	///</summary>	
	public void unLockCamera(){
		BlockInputs = false;
		if (wandController != null) {
			wandController.LockWand ();
		}
	}

	///<summary>
	///If movments are blocked, unblocks them <br>
	///If movments are not blocked, blocks them but allows wand movments
	///</summary>	
	public void invertBlockInput(){
		BlockInputs = ! BlockInputs;

		if (wandController != null) {
			wandController.invertWandLock();
		}
	}
}
