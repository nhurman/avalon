using UnityEngine;
using System.Collections;

public class DisplayShortcuts : MonoBehaviour {
	private GameObject menu				= null;
	private vrKeyboard keyb				= null;
	private double timeInCurrentState	= 0.0;

	// Use this for initialization
	void Start () {
		menu = GameObject.Find("KeyBindingMenu");
		menu.SetActive(false);
		keyb = MiddleVR.VRDeviceMgr.GetKeyboard();
	}
	
	void Update()
	{
		timeInCurrentState += MiddleVR.VRKernel.GetDeltaTime();
		Debug.Log (timeInCurrentState);
		if(timeInCurrentState > 0.5)
		{
			if (keyb.IsKeyPressed(MiddleVR.VRK_ESCAPE) && menu.activeSelf == false)
			{
				menu.SetActive(true);
				timeInCurrentState = 0.0;
			}
			else if (keyb.IsKeyPressed(MiddleVR.VRK_ESCAPE) && menu.activeSelf == true)
			{
				menu.SetActive(false);
				timeInCurrentState = 0.0;
			}
		}
	}
}
