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
	private bool FreeWand		= false;

	public float Sensibility	= 0.75f;
	
	private GameObject wand		= null;
	private GameObject handNode	= null;
//	private GameObject copywand = null;
//	private Quaternion angBorne 	  ;
//	private Quaternion angBorneLocal  ;
	private Vector3 wandPosOrig       ;
	private float HorizontalPosDelta  ;
	private float VerticalPosDelta    ;
	private float bornePos		= 100f ;

	void Start()
	{
		wand = GameObject.Find("VRWand");
		handNode = GameObject.Find("WandNode");
//		copywand = new GameObject ();
		if(wand == null) Debug.Log ("wand non trouvee dans wandonlycontroller :( ");
	}

	void Update()
	{
		if (FreeWand){
			float wandVertical = MiddleVR.VRDeviceMgr.GetWandVerticalAxisValue();
			float wandHorizontal = MiddleVR.VRDeviceMgr.GetWandHorizontalAxisValue();
			
			HorizontalPosDelta += wandHorizontal;
			VerticalPosDelta   += wandVertical;

			wandPosOrig = handNode.transform.position;

			//wand.transform.position = wandPosOrig;
			Vector3 pos = wand.transform.position;
			pos.x = wandPosOrig.x;
			pos.y = wandPosOrig.y;
			pos.z = wandPosOrig.z;
			wand.transform.position = pos;

			/* First we do a copy of the current wand.transform.rotation, only with the axis y 
			 * Then we rotate that copy
			 * We measure the angle between the rotated copy and the initial angle
			 * If it does not exceeds the limits, we apply the rotation on the wand.
			 */
//			copywand.transform.rotation = Quaternion.Euler(0, wand.transform.rotation.eulerAngles.y, 0);
//			copywand.transform.Rotate(0,wandHorizontal,0,Space.World);
//			Quaternion wandY = Quaternion.Euler(0, angBorne.eulerAngles.y, 0);
//
//			if (Quaternion.Angle(copywand.transform.rotation, wandY) <= 30){
//				wand.transform.Rotate(Vector3.up, wandHorizontal, Space.World);
//			}
			if(HorizontalPosDelta > bornePos)
				HorizontalPosDelta = bornePos;
			if(HorizontalPosDelta < -bornePos)
				HorizontalPosDelta = -bornePos;
			wand.transform.Translate(new Vector3(HorizontalPosDelta*0.002f, 0, 0));

			/* Exactly like horizontal rotation, but on local x axis instead */
//			copywand.transform.localRotation = Quaternion.Euler(wand.transform.localRotation.eulerAngles.x, 0, 0);
//			copywand.transform.Rotate(wandVertical,0,0,Space.Self);
//			Quaternion wandLocalX = Quaternion.Euler(angBorneLocal.eulerAngles.x, 0, 0);
//			
//			if (Quaternion.Angle(copywand.transform.localRotation, wandLocalX) <= 30){
//				wand.transform.Rotate(wandVertical, 0,0,Space.Self);
//			}
			if(VerticalPosDelta > bornePos)
				VerticalPosDelta = bornePos;
			if(VerticalPosDelta < -bornePos)
				VerticalPosDelta = -bornePos;
			wand.transform.Translate(new Vector3(0, -VerticalPosDelta*0.002f, 0));
		}
	}

//	private void moveWandBackward()
//	{
//		wand.transform.Translate (0, 0, -1.6f, Space.Self);
//	}

	public void LockWand(){
		FreeWand = false;
		wand.transform.position = wandPosOrig;//Debug.Log("wand z = " + wand.transform.position.z);
	}

	public void UnlockWand(){
		FreeWand = true;
		initValues ();
	}

	public void invertWandLock()
	{
		if (FreeWand)
			LockWand ();
		else
			UnlockWand ();
	}

	public void setWandCenter(Vector3 pos)
	{
		wandPosOrig = pos;
	}

	private void initValues()
	{
//		angBorne = wand.transform.rotation;
//		angBorneLocal = wand.transform.localRotation;
		HorizontalPosDelta = 0;
		VerticalPosDelta   = 0;
		wandPosOrig.Set(wand.transform.position.x, wand.transform.position.y, wand.transform.position.z);
	}
}
