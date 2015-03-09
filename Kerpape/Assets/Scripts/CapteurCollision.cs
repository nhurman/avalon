using UnityEngine;
using System.Collections;

public class CapteurCollision : MonoBehaviour {


	// Use this for initialization
	void Start () {


	}
	

	void Update () {

		if (Input.GetMouseButtonDown (1)) {
			transform.parent.gameObject.GetComponent<ControlleurPorte>().collision();
		}
		if (Input.GetMouseButtonDown (0)) {
			transform.parent.gameObject.GetComponent<ControlleurPorte>().ouvrirPorte();
		}

	}

	void OnCollisionEnter(Collision coll)
	{
		Debug.Log ("reussi");
	}
}
