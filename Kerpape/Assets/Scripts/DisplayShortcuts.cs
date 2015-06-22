using UnityEngine;
using System.Collections;

/// <summary>
/// Controls the appearance/disappearance a non-interactive menu that displays keyboard shortcuts
/// </summary>
public class DisplayShortcuts : MonoBehaviour {
	private GameObject menu						= null;			//menu is the plane to be displayed when pressing escape
	private GameObject headNode					= null;
	private vrKeyboard keyb						= null;			//MiddleVR representation of a keyboard
	private double timeInCurrentState			= 0.0;			//number of seconds since the last time the menu state changed (from hidden to displayed or the opposite)
	private VRFPSInputController controller		= null;			//a script that allows, among other things, to block inputs from the user


	void Start () {
		menu = GameObject.Find("KeyBindingMenu");
		menu.SetActive(false);									//at the begining of the simulation, the menu is hidden
		headNode = GameObject.Find("HeadNode");
		keyb = MiddleVR.VRDeviceMgr.GetKeyboard();
		controller = GetComponent<VRFPSInputController>();

	}

	/// <summary>
	/// Checks if the menu is in its current state from more than 0.5s and, eventually, changes its state if the user is pressing F12
	/// The user can't move when menu is displayed
	/// </summary>
	void Update()
	{
		timeInCurrentState += MiddleVR.VRKernel.GetDeltaTime();
		if(timeInCurrentState > 0.5)
		{
			if (keyb.IsKeyPressed(MiddleVR.VRK_F12) && menu.activeSelf == false)
			{
				menu.SetActive(true);
				timeInCurrentState = 0.0;
				controller.lockCamera();
			}
			else if (keyb.IsKeyPressed(MiddleVR.VRK_F12) && menu.activeSelf == true)
			{
				menu.SetActive(false);
				timeInCurrentState = 0.0;
				controller.unLockCamera();
			}
			menu.transform.position = headNode.transform.position;
			menu.transform.rotation = headNode.transform.rotation;
			menu.transform.Rotate(90f, 180f, 0);
			menu.transform.Translate(0, -2f, 0f);
		}
	}
}
