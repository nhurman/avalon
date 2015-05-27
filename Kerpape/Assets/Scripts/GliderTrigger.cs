using UnityEngine;
using System.Collections;

public class GliderTrigger : MonoBehaviour {
	public GameObject m_gliderContainer;

	private CameraGlider m_glider;

	// Use this for initialization
	void Start () {
		m_glider = m_gliderContainer.GetComponent<CameraGlider> ();
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
