using UnityEngine;
using System.Collections;

public class ActionTV : MonoBehaviour {

	GameObject planTV = null;
	bool activated	  = false;

	// Use this for initialization
	void Start () {
		planTV = GameObject.Find ("tele_allumee");
		planTV.transform.Translate (0, 15, 0);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void VRAction(){
		if (activated) {
			planTV.transform.Translate (0, 15, 0);
			activated = false;
		} else {
			planTV.transform.Translate (0, -15, 0);
			activated = true;
		}
	}
}
