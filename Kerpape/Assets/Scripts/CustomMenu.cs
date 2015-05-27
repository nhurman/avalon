using UnityEngine;
using System.Collections;

public class CustomMenu : MonoBehaviour {

	private VRMenu vrMenu			 	= null;
	private vrWidgetMenu m_Menu;


	// Use this for initialization
	void Start () {
		while (vrMenu == null || vrMenu.menu == null)
		{
			vrMenu = FindObjectOfType(typeof(VRMenu)) as VRMenu;
		}
	}
	
	private void choixMode()
	{
		// Add a button at the start of the menu
		//sousMenuMode = new vrCommand(MyItemCommandHandler);
		
		vrWidgetMenu sousMenuMode = new vrWidgetMenu("choixMode", vrMenu.menu, "Choix du mode");
		vrMenu.menu.SetChildIndex(sousMenuMode, 0);
	}
}
