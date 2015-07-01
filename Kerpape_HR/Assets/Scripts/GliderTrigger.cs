using UnityEngine;
using System.Collections;

public class GliderTrigger : MonoBehaviour {
	private CameraGlider m_glider;

	// Use this for initialization
	void Start () {
		m_glider = GameObject.Find ("Utilisateur").GetComponent<CameraGlider> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		m_glider.StartGlide ();

	}
	void OnTriggerExit(Collider other) {

	}
}
