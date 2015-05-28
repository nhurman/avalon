using UnityEngine;
using System.Collections;

public class OnKeyBoardMenu : MonoBehaviour {

	private vrKeyboard keyb 						= null;
	private Modelisation.GameManager gameManager	= null;
	private VRFPSInputController vrfps				= null;


	private uint scenarioAppelSimple 		= MiddleVR.VRK_F5;
	private uint scenarioAppelInfirmier 	= MiddleVR.VRK_F6;
	private uint scenarioVisiteInconnu	 	= MiddleVR.VRK_F7;
	private uint noScenario				 	= MiddleVR.VRK_F8;

	private uint modeAssiste				= MiddleVR.VRK_F3;
	private uint modeAutonome				= MiddleVR.VRK_F4;

	private uint reloadScene				= MiddleVR.VRK_F9;
	private uint quitScene					= MiddleVR.VRK_F10;

	private uint sensibilityUp				= MiddleVR.VRK_F1;
	private uint sensibilityDown			= MiddleVR.VRK_F2;

	private uint lookSensibilityUp			= MiddleVR.VRK_F3;
	private uint lookSensibilityDown		= MiddleVR.VRK_F4;

	private float sensibilityFactor 		= 1.1f;
	private float lookSensibilityFactor 	= 1.05f;


	void Start(){
		keyb = MiddleVR.VRDeviceMgr.GetKeyboard();
		gameManager = GameObject.Find("GameManager").GetComponent<Modelisation.GameManager>();
		vrfps = GameObject.Find ("Utilisateur").GetComponent<VRFPSInputController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (keyb.IsKeyToggled(reloadScene))
			Application.LoadLevel(Application.loadedLevel);

		if (keyb.IsKeyToggled(quitScene))
			Application.Quit();

		if (keyb.IsKeyToggled (modeAssiste)) {
			gameManager.CurrentMode = Modelisation.Mode.Assisted;
		}
			

		if (keyb.IsKeyToggled (modeAutonome)) {
			gameManager.CurrentMode = Modelisation.Mode.Auto;
		}

		if (keyb.IsKeyToggled (scenarioAppelSimple)) {
			gameManager.loadScenario("appel");
		}

		if (keyb.IsKeyToggled (scenarioAppelInfirmier)) {
			gameManager.loadScenario("infirmier");
		}

		if (keyb.IsKeyToggled (scenarioVisiteInconnu)) {
			gameManager.loadScenario("inconnu");
		}

		if (keyb.IsKeyToggled (scenarioVisiteInconnu)) {
			gameManager.loadScenario("aucun");
		}

		if ((keyb.IsKeyToggled (sensibilityUp) && keyb.IsKeyPressed (sensibilityDown)) 
		   || (keyb.IsKeyToggled (sensibilityDown) && keyb.IsKeyPressed (sensibilityUp))) {
			vrfps.Sensibility = 3.0f;
		}
		else if (keyb.IsKeyToggled (lookSensibilityDown) && keyb.IsKeyPressed (lookSensibilityUp)){
			vrfps.lookSensibility = 1.0f;
		}

		else if (keyb.IsKeyToggled (sensibilityUp)) {
			vrfps.Sensibility *= sensibilityFactor;
			
		}

		else if (keyb.IsKeyToggled (sensibilityDown)) {
			vrfps.Sensibility /= sensibilityFactor;
			
		}

		else if (keyb.IsKeyToggled (lookSensibilityUp)) {
			vrfps.lookSensibility *= sensibilityFactor;
			
		}
		
		else if (keyb.IsKeyToggled (lookSensibilityDown)) {
			vrfps.lookSensibility /= sensibilityFactor;
			
		}


	}
}
