/* VRMenu
 * MiddleVR
 * (c) i'm in VR
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiddleVR_Unity3D;

public class VRMenu : MonoBehaviour {

	private Modelisation.GameManager manager;
	
	private VRManagerScript m_VRManager;
	
	private vrGUIRendererWeb m_GUIRendererWeb;
	private vrWidgetMenu     m_Menu;
	
	public vrGUIRendererWeb guiRendererWeb
	{
		get
		{
			return m_GUIRendererWeb;
		}
	}
	
	public vrWidgetMenu menu
	{
		get
		{
			return m_Menu;
		}
	}
	
	// General
	private vrWidgetButton       m_ResetButton;
	private vrWidgetButton       m_ExitButton;

	// Choix mode
	private vrWidgetMenu         menuChoixMode;
	private vrWidgetButton 		 modeAssiste;
	private vrWidgetButton		 modeLibre;

	// Choix scénario
	private vrWidgetMenu         menuChoixScenar;
	private vrWidgetButton scenarAppel;
	private vrWidgetButton scenarInfirmier;
	private vrWidgetButton scenarInconnu;

	// General
	private vrCommand m_ResetButtonCommand;
	private vrCommand m_ExitButtonCommand;
	
	// Mode
	private vrCommand modeMenuCommand;
	private vrCommand modeAssisteCommand;
	private vrCommand modeLibreCommand;
	
	// Scénario
	private vrCommand scenarioMenuCommand;
	private vrCommand scenarioAppelCommand;
	private vrCommand scenarioInfirmierCommand;
	private vrCommand scenarioInconnuCommand;
	
	// Bind with Interaction events
	private vrEventListener m_Listener;
	private Dictionary<string, vrCommand> m_Commands = new Dictionary<string, vrCommand>();
	private Dictionary<string, vrWidgetToggleButton> m_Buttons = new Dictionary<string, vrWidgetToggleButton>();
	
	
	private bool EventListener(vrEvent iEvent)
	{
		// Catch interaction events
		vrInteractionEvent interactionEvt = vrInteractionEvent.Cast(iEvent);
		if (interactionEvt != null)
		{
			vrInteraction interaction = interactionEvt.GetInteraction();
			
			bool needLabelRefresh = false;
			
			// Identify interaction
			// If existing in the Menu, update the menu
			if (interactionEvt.GetEventType() == (int)VRInteractionEventEnum.VRInteractionEvent_Activated)
			{
				vrWidgetToggleButton interactionButton;
				
				if (m_Buttons.TryGetValue(interaction.GetName(), out interactionButton))
				{
					interactionButton.SetChecked(true);
				}
				
				needLabelRefresh = true;
			}
			else if (interactionEvt.GetEventType() == (int)VRInteractionEventEnum.VRInteractionEvent_Deactivated)
			{
				vrWidgetToggleButton interactionButton;
				
				if (m_Buttons.TryGetValue(interaction.GetName(), out interactionButton))
				{
					interactionButton.SetChecked(false);
				}
				
				needLabelRefresh = true;
			}
		}
		return true;
	}
	
	// General
	private vrValue ResetButtonHandler(vrValue iValue)
	{
		Application.LoadLevel(Application.loadedLevel);
		return null;
	}
	
	private vrValue ExitButtonHandler(vrValue iValue)
	{
		m_VRManager.QuitApplication();
		return null;
	}
	
	private vrValue ModeAssisteHandler(vrValue iValue)
	{
		manager.CurrentMode = Modelisation.Mode.Assisted;
		return null;
	}
	
	private vrValue ModeLibreHandler(vrValue iValue)
	{
		manager.CurrentMode = Modelisation.Mode.Auto;
		return null;
	}

	private vrValue ScenarioAppelHandler(vrValue iValue)
	{

		return null;
	}

	private vrValue ScenarioInfirmierHandler(vrValue iValue)
	{
		
		return null;
	}

	private vrValue ScenarioInconnuHandler(vrValue iValue)
	{
		
		return null;
	}

	void Start ()
	{

		manager = GameObject.Find("GameManager").GetComponent<Modelisation.GameManager>();

		// Retrieve the VRManager
		VRManagerScript[] foundVRManager = FindObjectsOfType(typeof(VRManagerScript)) as VRManagerScript[];
		if (foundVRManager.Length != 0)
		{
			m_VRManager = foundVRManager[0];
		}
		else
		{
			MVRTools.Log("[X] VRMenu: impossible to retrieve the VRManager.");
			return;
		}
		
		// Start listening to MiddleVR events
		m_Listener = new vrEventListener(EventListener);
		MiddleVR.VRKernel.AddEventListener(m_Listener);
		
		// Create commands
		
		// General
		m_ResetButtonCommand		= new vrCommand("VRMenu.ResetCurrentButtonCommand", ResetButtonHandler);
		m_ExitButtonCommand			= new vrCommand("VRMenu.ExitButtonCommand", ExitButtonHandler);
		
		// Mode
		modeAssisteCommand			= new vrCommand("VRMenu.modeAssisteCommand", ModeAssisteHandler);
		modeLibreCommand			= new vrCommand("VRMenu.modeLibreCommand", ModeLibreHandler);
		
		// Scenario
		scenarioAppelCommand		= new vrCommand("VRMenu.ScenarioAppelCommand", ScenarioAppelHandler);
		scenarioInfirmierCommand	= new vrCommand("VRMenu.ScenarioInfirmierCommand", ScenarioInfirmierHandler);
		scenarioInconnuCommand		= new vrCommand("VRMenu.ScenarioInconnuCommand", ScenarioInconnuHandler);

		// Create GUI
		m_GUIRendererWeb = null;
		
		VRWebView webViewScript = GetComponent<VRWebView>();
		
		if (webViewScript == null)
		{
			MVRTools.Log(1, "[X] VRMenu does not have a WebView.");
			return;
		}
		
		m_GUIRendererWeb = new vrGUIRendererWeb("", webViewScript.webView);
		
		m_Menu = new vrWidgetMenu("VRMenu.VRManagerMenu", m_GUIRendererWeb);
		
		// Mode
		menuChoixMode = new vrWidgetMenu("VRMenu.ChoixMode", m_Menu, "Modes");

		modeAssiste = new vrWidgetButton("VRMenu.ModeAssisteButton", menuChoixMode, "Passer en mode assisté", modeAssisteCommand);
		modeLibre = new vrWidgetButton("VRMenu.ModeLibreButton", menuChoixMode, "Passer en mode libre", modeLibreCommand);

		// Scénario
		menuChoixScenar = new vrWidgetMenu("VRMenu.ChoixScenario", m_Menu, "Scenarios");

		scenarAppel = new vrWidgetButton("VRMenu.ScenarAppelButton", menuChoixScenar, "Appel téléphonique", scenarioAppelCommand);
		scenarInfirmier = new vrWidgetButton("VRMenu.ScenarInfirmierButton", menuChoixScenar, "Infirmier à l'entrée", scenarioInfirmierCommand);
		scenarInconnu = new vrWidgetButton("VRMenu.ScenarInconnuButton", menuChoixScenar, "Inconnu à l'entrée", scenarioInconnuCommand);

		// Reset and Exit
		m_ResetButton = new vrWidgetButton("VRMenu.ResetCurrentButton", m_Menu, "Reload simulation", m_ResetButtonCommand);

		m_ExitButton     = new vrWidgetButton("VRMenu.ExitButton", m_Menu, "Exit simulation", m_ExitButtonCommand);
	}
}
