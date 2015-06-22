 using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {
	private GameObject m_headNode;
	private VRFPSInputController m_controller;
	private WandOnlyController m_wController;
	private GameObject m_wand;
	
	private vrKeyboard m_keyb;

	private bool m_isToggled  = false;

	// Use this for initialization
	void Start () {
		m_keyb = MiddleVR.VRDeviceMgr.GetKeyboard();
		m_headNode = GameObject.Find ("HeadNode");
		m_controller = GameObject.Find("Utilisateur").GetComponent<VRFPSInputController>();
		m_wController = GameObject.Find("Utilisateur").GetComponent<WandOnlyController>();
		m_wand = GameObject.Find ("VRWand");

		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	/// <summary>
	/// Toggles the visibility of the VR menu
	/// </summary>
	public void DisplayOrHideMenu()
	{
		m_isToggled = !m_isToggled;
		m_controller.enabled = !m_controller.enabled;
		m_wController.enabled = !m_wController.enabled;
		m_wand.transform.Translate(0, m_isToggled? -10f : 10f, 0);
		Cursor.visible = !Cursor.visible;
	}
}
