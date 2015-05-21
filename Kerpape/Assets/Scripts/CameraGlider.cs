using UnityEngine;
using System.Collections;

public class CameraGlider : MonoBehaviour {
	public GameObject utilisateur;
	public GameObject destination;
	public GameObject headNode;

	private VRFPSInputController inputController;

	private vrKeyboard keyb;
	
	private Vector3 savedUtilisateurPos;
	private Quaternion savedUtilisateurRot;
	private float savedHeadNodeAngle;

	private float startTime;

	private Vector3 startPos;
	private Quaternion startRot;
	private float startHeadAngle;
	private Vector3 endPos;
	private Quaternion endRot;
	private float endHeadAngle;

	private bool gliding;
	private bool frozen;
	private bool reversing;

	// Use this for initialization
	void Start () {
		if (MiddleVR.VRDeviceMgr.GetKeyboard() != null)
		{
			keyb = MiddleVR.VRDeviceMgr.GetKeyboard();
		}
		inputController = utilisateur.GetComponent<VRFPSInputController> ();
		headNode = GameObject.Find ("HeadNode");

		gliding = false;
		frozen = false;
		reversing = false;
	}
	
	Quaternion cloneQuaternion(Quaternion t)
	{
		return new Quaternion(t.x, t.y, t.z, t.w);
	}
	
	Vector3 cloneVector3(Vector3 t)
	{
		return new Vector3(t.x, t.y, t.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (gliding)
		{
			Glide ();
		}
		else if (frozen)
		{
			Freeze ();
		}
		else if (reversing)
		{
			ReverseGlide();
		}
	}
	
	public void StartGlide()
	{
		if (gliding || frozen || reversing) return;
		
		startTime = Time.time;
		gliding = true;

		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("Utilisateur"), true);
		inputController.lockCamera ();

		savedUtilisateurPos = cloneVector3(utilisateur.transform.position);
		savedUtilisateurRot = cloneQuaternion(utilisateur.transform.rotation);
		savedHeadNodeAngle = inputController.verticalAngle;

		startPos = savedUtilisateurPos;
		startRot = savedUtilisateurRot;
		startHeadAngle = savedHeadNodeAngle;

		endPos = destination.transform.position;
		endPos.x -= 0.4f;
		endPos.y -= 1.2f;
		endRot = Quaternion.Euler (0, 90f, 0);
		endHeadAngle = 0f;
	}

	private void Glide()
	{
		Lerp ();
		
		if(Time.time - startTime >= 1f)
		{
			startFreeze();
		}
	}

	private void startFreeze()
	{
		gliding = false;
		frozen = true;
	}

	private void Freeze()
	{
		utilisateur.transform.position = endPos;
		utilisateur.transform.rotation = endRot;
		
		if (keyb.IsKeyPressed (MiddleVR.VRK_DOWN) || keyb.IsKeyPressed (MiddleVR.VRK_S)) {
			StartReverseGlide ();
		}
	}
	
	private void StartReverseGlide()
	{
		frozen = false;
		reversing = true;

		startTime = Time.time;
		
		startPos = destination.transform.position;
		startPos.x -= 0.4f;
		startPos.y -= 1.2f;
		startRot = Quaternion.Euler (0, 90f, 0);
		startHeadAngle = 0f;

		endPos = savedUtilisateurPos;
		endPos.x -= 0.5f;
		//endRot = savedUtilisateurRot;
		endRot = Quaternion.Euler (savedUtilisateurRot.x, 90f, savedUtilisateurRot.z);
		endHeadAngle = 0f;

	}
	
	private void ReverseGlide()
	{
		Lerp ();
		
		if(Time.time - startTime >= 1f)
		{
			endReverseGlide();
		}
	}

	private void endReverseGlide()
	{
		reversing = false;
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("Utilisateur"), false);
		inputController.unLockCamera ();
	}

	private void Lerp()
	{
		float t = Mathf.Min (1f, (Time.time - startTime) / 1f);// 1f = temps de lerp total
		utilisateur.transform.position = Vector3.Lerp (startPos, endPos, t);
		utilisateur.transform.rotation = Quaternion.Lerp (startRot, endRot, t);
		inputController.verticalAngle = Mathf.Lerp (startHeadAngle, endHeadAngle, t);
	}
}
