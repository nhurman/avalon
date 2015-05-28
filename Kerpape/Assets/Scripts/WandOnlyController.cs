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

	/// <summary>
	/// Sensibilyt is a coefficient applied on the movement off the wand, the greater it is, the faster the movement are
	/// </summary>
	public float Sensibility	= 0.75f;

	/// <summary>
	/// GameObject corresponding to the wand
	/// </summary>
	private GameObject wand		= null;

	/// <summary>
	/// /// GameObject corresponding to the handNode
	/// </summary>
	private GameObject handNode	= null;

	/// <summary>
	/// Wand's global position when switching from VRFPS Controller to WandOnlyController
	/// Is reset during the next switch
	/// </summary>
	private Vector3 wandPosOrig       ;

	/// <summary>
	/// Horizontal movement to apply top the wand
	/// </summary>
	private float HorizontalPosDelta  ;

	/// <summary>
	/// Vertical movement to apply top the wand
	/// </summary>
	private float VerticalPosDelta    ;

	/// <summary>
	/// Limit used for the amplitude of wand's movement
	/// </summary>
	private float bornePos		= 100f ;


	private void Start()
	{
		wand = GameObject.Find("VRWand");
		handNode = GameObject.Find("WandNode");
		if(wand == null) Debug.Log ("wand non trouvee dans wandonlycontroller :( ");
	}



	private void Update()
	{
		if (FreeWand){
			float wandVertical = MiddleVR.VRDeviceMgr.GetWandVerticalAxisValue();
			float wandHorizontal = MiddleVR.VRDeviceMgr.GetWandHorizontalAxisValue();
			
			HorizontalPosDelta += wandHorizontal;
			VerticalPosDelta   += wandVertical;

			wandPosOrig = handNode.transform.position;

			Vector3 pos = wand.transform.position;
			pos.x = wandPosOrig.x;
			pos.y = wandPosOrig.y;
			pos.z = wandPosOrig.z;
			wand.transform.position = pos;

			if(HorizontalPosDelta > bornePos)
				HorizontalPosDelta = bornePos;
			if(HorizontalPosDelta < -bornePos)
				HorizontalPosDelta = -bornePos;
			wand.transform.Translate(new Vector3(HorizontalPosDelta*0.002f, 0, 0));

			if(VerticalPosDelta > bornePos)
				VerticalPosDelta = bornePos;
			if(VerticalPosDelta < -bornePos)
				VerticalPosDelta = -bornePos;
			wand.transform.Translate(new Vector3(0, -VerticalPosDelta*0.002f, 0));
		}
	}


	/// <summary>
	/// LockWand locks the wand with the handNode, i.e. the wand follows the camera
	/// </summary>
	public void LockWand(){
		FreeWand = false;
		wand.transform.position = wandPosOrig;
	}

	/// <summary>
	/// UnlockWand unlocks the wand with the handNode, i.e. the wand moves freely between its boundaries,
	/// The camera is fixed
	/// </summary>
	public void UnlockWand(){
		FreeWand = true;
		initValues ();
	}

	/// <summary>
	/// Reverse the wand state (free, not free)
	/// </summary>
	public void invertWandLock()
	{
		if (FreeWand)
			LockWand ();
		else
			UnlockWand ();
	}

	/// <summary>
	/// Set the wand's position when switching from VRFPS Controller to WandOnlyController
	/// i.e. set the wandPosOrig
	/// </summary>
	/// <param name="type">Vector3</param>
	/// <param name="name">pos</param>
	public void setWandCenter(Vector3 pos)
	{
		wandPosOrig = pos;
	}

	/// <summary>
	/// Initializes values used for WandOnlyController when switching to WandOnlyController
	/// </summary>
	private void initValues()
	{
		HorizontalPosDelta = 0;
		VerticalPosDelta   = 0;
		wandPosOrig.Set(wand.transform.position.x, wand.transform.position.y, wand.transform.position.z);
	}
}
