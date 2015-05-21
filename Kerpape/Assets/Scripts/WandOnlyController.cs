using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;
using System;

[RequireComponent (typeof (VRFPSInputController))]

public class WandOnlyController : MonoBehaviour
{
	/// <summary>
	/// Set to true when VRFPSInputController is desactivated, i.e. when WandOnlyController must be used
	/// </summary>
	public bool BlockedInputs	= false;

	public float Sensibility	= 0.75f;

	private GameObject wand		= null;

	void Start()
	{
		wand = GameObject.Find("VRWand");
		if(wand == null) Debug.Log ("wand non trouvee dans wandonlycontroller :( ");
	}

	void Update()
	{
		if (BlockedInputs){
			float wandVertical = MiddleVR.VRDeviceMgr.GetWandVerticalAxisValue();
			float wandHorizontal = MiddleVR.VRDeviceMgr.GetWandHorizontalAxisValue();

			Debug.Log(wandHorizontal);
			Debug.Log(wandVertical);

			wand.transform.Rotate(Vector3.up, wandHorizontal, Space.Self);
			wand.transform.Rotate(Vector3.right, wandVertical, Space.World);
		}
	}
}