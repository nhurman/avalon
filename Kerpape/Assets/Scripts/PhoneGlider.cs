using UnityEngine;
using System.Collections;

public class PhoneGlider : MonoBehaviour {
	public GameObject m_phone;
	private GameObject m_headNode;
	private GameObject m_ear;

	private float startTime;
	
	private bool gliding   = false;
	private bool frozen    = false;
	private bool reversing = false;
	
	private Vector3 savedPhonePos;
	private Quaternion savedPhoneRot;

	private Vector3 startPos;
	private Quaternion startRot;
	private Vector3 endPos;
	private Quaternion endRot;

	// Use this for initialization
	void Start () {
		m_ear = new GameObject ();

		m_headNode = GameObject.Find ("HeadNode");
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

	public void VRAction()
	{
		if(gliding || reversing) return;
		if(frozen)
		{
			StartReverseGlide();
		}
		else
		{
			StartGlide();
		}
	}
	
	/// <summary>
	/// This function is called when the phone starts moving toward the user's ear
	/// </summary>
	public void StartGlide()
	{
		if (gliding || frozen || reversing) return;
		
		startTime = Time.time;
		gliding = true;
		
		m_ear.transform.position = m_headNode.transform.position;
		m_ear.transform.rotation = m_headNode.transform.rotation;
		m_ear.transform.Translate (0.2f, 0.1f, 0.2f);
		m_ear.transform.Rotate (0, 0, 270f);
		
		savedPhonePos = m_phone.transform.position;
		savedPhoneRot = m_phone.transform.rotation;
		
		startPos = savedPhonePos;
		startRot = savedPhoneRot;
		
		endPos = m_ear.transform.position;
		endRot = m_ear.transform.rotation;
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
		m_ear.transform.position = m_headNode.transform.position;
		m_ear.transform.rotation = m_headNode.transform.rotation;
		m_ear.transform.Translate (0.2f, 0.1f, 0.2f);
		m_ear.transform.Rotate (0, 0, 270f);

		m_phone.transform.position = m_ear.transform.position;
		m_phone.transform.rotation = m_ear.transform.rotation;
	}
	
	/// <summary>
	/// This function is called when the phone starts moving back to its original place
	/// </summary>
	public void StartReverseGlide()
	{
		frozen = false;
		reversing = true;
		
		startTime = Time.time;
		
		startPos = m_ear.transform.position;
		startRot = m_ear.transform.rotation;
		
		endPos = savedPhonePos;
		endRot = savedPhoneRot;
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
	}

	private void Lerp()
	{
		float t = Mathf.Min (1f, (Time.time - startTime) / 1f);// 1f = temps de lerp total
		m_phone.transform.position = Vector3.Lerp (startPos, endPos, t);
		m_phone.transform.rotation = Quaternion.Lerp (startRot, endRot, t);
	}
}
