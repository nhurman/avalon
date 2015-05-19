using UnityEngine;
using System.Collections;

public class CameraGlider : MonoBehaviour {
	public GameObject utilisateur;
	public GameObject destination;

	private GameObject headNode;
	private vrKeyboard keyb;

	private Quaternion savedHeadNode;
	private Quaternion savedCamera0;
	private Quaternion savedUtilisateurRot;
	private Vector3 savedUtilisateur;

	private bool frozen;

	// Use this for initialization
	void Start () {
		if (MiddleVR.VRDeviceMgr.GetKeyboard() != null)
		{
			keyb = MiddleVR.VRDeviceMgr.GetKeyboard();
		}

		headNode = null;
		frozen = false;
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
		if (keyb.IsKeyToggled(MiddleVR.VRK_F1)) {
			if (!frozen) { // Lock camera
				Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("Utilisateur"), true);
				savedHeadNode = cloneQuaternion(GameObject.Find("HeadNode").transform.rotation);
				savedCamera0 = cloneQuaternion(GameObject.Find("Camera0").transform.rotation);
				savedUtilisateur = cloneVector3(utilisateur.transform.position);
				savedUtilisateurRot = cloneQuaternion(utilisateur.transform.rotation);
			}
			else { // Unlock camera
				GameObject.Find("HeadNode").transform.rotation = savedHeadNode;
				GameObject.Find("Camera0").transform.rotation = savedCamera0;
				utilisateur.transform.position = savedUtilisateur;
				utilisateur.transform.rotation = savedUtilisateurRot;
				Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("Utilisateur"), false);
			}

			frozen = !frozen;
			utilisateur.GetComponent<VRFPSInputController>().BlockInputs = frozen;
		}

		if (frozen)
		{
			utilisateur.GetComponent<CharacterController>().detectCollisions = false;
			Vector3 pos = destination.transform.position;
			pos.x -= 0.4f;
			pos.y -= 1.2f;
			utilisateur.transform.position = pos;

			//Quaternion q = cloneQuaternion(utilisateur.transform.rotation);
			utilisateur.transform.LookAt(destination.transform);
			utilisateur.transform.rotation = new Quaternion(0, 0, 0, 0);//Rotate(new Vector3(-utilisateur.transform.rotation.x, 0f, 0f));
			utilisateur.transform.Rotate (Vector3.up, 90f);

			GameObject.Find("HeadNode").transform.LookAt(destination.transform);
			GameObject.Find("Camera0").transform.LookAt(destination.transform);
		}
		if (keyb.IsKeyPressed (MiddleVR.VRK_F2))
		{
			utilisateur.GetComponent<CharacterController>().detectCollisions = true;
		}
	}
}
