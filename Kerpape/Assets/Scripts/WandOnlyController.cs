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
	private GameObject copywand = null;
	private Quaternion angBorne 	  ;
	private Quaternion angBorneLocal 	  ;

	void Start()
	{
		wand = GameObject.Find("VRWand");
		copywand = new GameObject ();
		if(wand == null) Debug.Log ("wand non trouvee dans wandonlycontroller :( ");
	}

	void Update()
	{
		if (BlockedInputs){
			float wandVertical = MiddleVR.VRDeviceMgr.GetWandVerticalAxisValue();
			float wandHorizontal = MiddleVR.VRDeviceMgr.GetWandHorizontalAxisValue();

			/* First we do a copy of the current wand.transform.rotation, only with the axis y 
			 * Then we rotate that copy
			 * We measure the angle between the rotated copy and the initial angle
			 * If it does not exceeds the limits, we apply the rotation on the wand.
			 */
			copywand.transform.rotation = Quaternion.Euler(0, wand.transform.rotation.eulerAngles.y, 0);
			copywand.transform.Rotate(0,wandHorizontal,0,Space.World);
			Quaternion wandY = Quaternion.Euler(0, angBorne.eulerAngles.y, 0);

			if (Quaternion.Angle(copywand.transform.rotation, wandY) <= 30){
				wand.transform.Rotate(Vector3.up, wandHorizontal, Space.World);
			}


			/* Exactly like horizontal rotation, but on local x axis instead */
			copywand.transform.localRotation = Quaternion.Euler(wand.transform.localRotation.eulerAngles.x, 0, 0);
			copywand.transform.Rotate(wandVertical,0,0,Space.Self);
			Quaternion wandLocalX = Quaternion.Euler(angBorneLocal.eulerAngles.x, 0, 0);
			
			if (Quaternion.Angle(copywand.transform.localRotation, wandLocalX) <= 30){
				wand.transform.Rotate(wandVertical, 0,0,Space.Self);
			}

		}
	}

	public void setAngle(){
		angBorne = wand.transform.rotation;
		angBorneLocal = wand.transform.localRotation;
	}
}