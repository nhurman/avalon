using UnityEngine;
using System.Collections;

public class CameraGlider : MonoBehaviour {
	public GameObject utilisateur;
	public GameObject destination;

	private VRFPSInputController inputController;

	private vrKeyboard keyb;
	
	private Vector3 savedUtilisateurPos;
	private Quaternion savedUtilisateurRot;

	private float startTime;

	private Vector3 startPos;
	private Quaternion startRot;
	private Vector3 endPos;
	private Quaternion endRot;

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
		//inputController.BlockInputs = true;

		savedUtilisateurPos = cloneVector3(utilisateur.transform.position);
		savedUtilisateurRot = cloneQuaternion(utilisateur.transform.rotation);

		startPos = savedUtilisateurPos;
		startRot = savedUtilisateurRot;

		endPos = destination.transform.position;
		endPos.x -= 0.4f;
		endPos.y -= 1.2f;
		endRot = Quaternion.Euler (0, 90f, 0);
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

		endPos = savedUtilisateurPos;
		endPos.x -= 1f;
		endRot = savedUtilisateurRot;

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
		inputController.BlockInputs = false;
	}

	private void Lerp()
	{
		float t = Mathf.Min (1f, (Time.time - startTime) / 1f);// 1f = temps de lerp total
		utilisateur.transform.position = Vector3.Lerp (startPos, endPos, t);
		utilisateur.transform.rotation = Quaternion.Lerp (startRot, endRot, t);
	}
}
