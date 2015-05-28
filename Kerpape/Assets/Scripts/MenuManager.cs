 using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {
	private GameObject m_menu;
	private VRMenuManager m_menuManager;
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
		m_menu = GameObject.Find ("VRMenu");
		m_menuManager = m_menu.GetComponent<VRMenuManager> ();
		m_controller = GameObject.Find("Utilisateur").GetComponent<VRFPSInputController>();
		m_wController = GameObject.Find("Utilisateur").GetComponent<WandOnlyController>();
		m_wand = GameObject.Find ("VRWand");
	}
	
	// Update is called once per frame
	void Update () {
		if(m_keyb.IsKeyToggled (MiddleVR.VRK_F12))
		{
			m_menuManager.ToggleVisiblity();
			m_isToggled = !m_isToggled;
			m_controller.enabled = !m_controller.enabled;
			m_wController.enabled = !m_wController.enabled;

			m_wand.transform.Translate(0, m_isToggled? -10f : 10f, 0);
		}
		if(m_isToggled)
		{
			m_menu.transform.position = m_headNode.transform.position;
			m_menu.transform.Translate(0, 0, 0.5f);
		}
	}
}
