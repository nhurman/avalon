using UnityEngine;
using System.Collections;

public class CameraAssistee : MonoBehaviour {
	public Camera camera;
	public GameObject renderPlane;

	private Transform currentPos;
	private GameObject playerCamera;

	private float endTime;

	// Use this for initialization
	void Start () {
		playerCamera = GameObject.Find ("Camera0");
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= endTime)
		{
			renderPlane.SetActive (false);
		}
		else
		{
			renderPlane.transform.rotation = playerCamera.transform.rotation;
			renderPlane.transform.Rotate (90f, 270f, 90f);
			renderPlane.transform.position = playerCamera.transform.position;
			renderPlane.transform.Translate (-0.175f, -0.365f, 0.1f);
		}
	}

	public void lookAt(string name) {
		currentPos = transform.Find("/mode_assiste/" + name + "_pos");
		if (currentPos != null) {
			camera.transform.position = currentPos.position;
			camera.transform.rotation = currentPos.rotation;
			showRenderPlane (3f);
		}
	}
	
	private void showRenderPlane(float time)
	{
		endTime = Time.time + time;
		renderPlane.SetActive (true);
	}

	public void hideRenderPlane()
	{
		endTime = 0;
	}
}
