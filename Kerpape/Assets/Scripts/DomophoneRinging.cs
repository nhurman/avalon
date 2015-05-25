using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;

[RequireComponent (typeof (AudioSource))]

public class DomophoneRinging : MonoBehaviour {
	/// <summary>
	/// The total duration of the ringing
	/// </summary>
	public float MaxDuration		= 10.0f;

	/// <summary>
	/// The AudioSource game component
	/// </summary>
	private AudioSource source		= null;
	/// <summary>
	/// The MiddleVR Keyboard, used to input a command that makes the phone ring
	/// Test purpose ONLY, to be removed for an automatic ringing later </summary>
	private vrKeyboard keyb 		= null;
	/// <summary>
	/// Used to remember for how long the phone has been ringing
	/// </summary>
	private float duration 			= 0.0f;

	void Start () {
		source = GetComponent<AudioSource>();
		source.loop = true;
		keyb = MiddleVR.VRDeviceMgr.GetKeyboard();
		if(keyb == null){
			Debug.Log ("Clavier non reconnu dans DomophoneRinging.cs");
		}
	}
	
	/// <summary>
	/// If X is pressed, the phone starts ringing for 10sec
	/// Used for test ONLY : has to be automatised later
	/// </summary>
	void Update () {
		if (keyb.IsKeyPressed(MiddleVR.VRK_X))
		{
			source.Play();
			duration = 0.0f;
		}

		duration += (float)MiddleVR.VRKernel.GetDeltaTime();

		if(duration > MaxDuration)
		{
			source.Stop ();
		}
	}
}
