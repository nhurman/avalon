using UnityEngine;
using System.Collections;

public class CameraAssistee : MonoBehaviour {
	public Camera camera;
	public GameObject renderPlane;
	
	public GameObject posShutters;
	public GameObject posLightKitchen;

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
	
	public void lookAtShutters()
	{
		camera.transform.position = posShutters.transform.position;
		camera.transform.rotation = posShutters.transform.rotation;
		showRenderPlane (3f);
	}
	public void lookAtKitchenLight()
	{
		camera.transform.position = posLightKitchen.transform.position;
		camera.transform.rotation = posLightKitchen.transform.rotation;
		showRenderPlane (3f);
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
	
	
	[ContextMenu("Debug")]
	private  void debug()
	{
		lookAtKitchenLight();
	}
}
