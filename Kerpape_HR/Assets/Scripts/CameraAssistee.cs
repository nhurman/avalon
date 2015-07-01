using UnityEngine;
using System.Collections;

public class CameraAssistee : MonoBehaviour {
	public Camera camera;
	public GameObject renderPlane;
	
	public GameObject posShutters;
	public GameObject posLightKitchen;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void lookAtShutters()
	{
		camera.transform.position = posShutters.transform.position;
		camera.transform.rotation = posShutters.transform.rotation;
	}
	public void lookAtKitchenLight()
	{
		camera.transform.position = posLightKitchen.transform.position;
		camera.transform.rotation = posLightKitchen.transform.rotation;
	}
	
	
	[ContextMenu("Debug")]
	private  void debug()
	{
		lookAtKitchenLight();
	}
}
