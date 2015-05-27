using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    public enum Mode { Auto, Assisted, Symbolic };


	/// <summary>
	/// Handle Scenario and authorisations on actions, and any code related to user errors.
    /// Handle any information not directly related to an object. Only one instance of that script is and should be used. 
	/// </summary>
    public class GameManager : MonoBehaviour
    {

		private vrKeyboard keyb					= null;
		
		public static IList<ScenarioItem> Scenar1 = new List<ScenarioItem>() 
        {
			new AudioScenarioItem("domophone", "Decrocher le domophone", "domophone/sonnerie"),
			//new AudioScenarioItem("domophone", "Parler à votre interlocuteur, puis raccrocher", "domophone/son_interlocuteur"),
			new EndScenario()
        };

        public static IList<ScenarioItem> Scenar2 = new List<ScenarioItem>() 
        {
			new AudioScenarioItem("domophone", "Decrocher le domophone", "domophone/sonnerie"),
			//new AudioScenarioItem("domophone", "Parler à votre interlocuteur, c'est un infirmier, raccrocher", "domophone/son_infirmier"),
			new ScenarioItem("front_door_open", "Ouvrir la porte du batiment"),
			new ScenarioItem("appartement_door_switch", "Ouvrir la porte de l'appartement"),
			new ScenarioItem("appartement_door_switch", "Fermer la porte de l'appartement"),
			new EndScenario()
        };

        public static IList<ScenarioItem> Scenar3 = new List<ScenarioItem>() 
		{
			new AudioScenarioItem("domophone", "Decrocher le domophone", "domophone/sonnerie"),
			//new AudioScenarioItem("domophone", "Parler à votre interlocuteur, c'est un inconnu à la porte, raccrocher", "domophone/son_inconnu"),
			new ScenarioItem("switch_tele", "Ouvrir la porte du batiment"),
			// Canal S8

			new ScenarioItem("front_door_open", "Ouvrir la porte du batiment"),
			new ScenarioItem("appartement_door_switch", "Ouvrir la porte de l'appartement"),
			new ScenarioItem("appartement_door_switch", "Fermer la porte de l'appartement"),
			// Eteindre TV ?
			new EndScenario()

        };
		

        public static IList<ScenarioItem> EmptyScenar = new List<ScenarioItem>() 
        {
            new ScenarioItem("Rien", "Il n'y a rien à faire, faites ce que vous voulez :)", true)
        };

        public IList<ScenarioItem> ScenarioData { get; protected set; }
        
		/// <summary>
		/// Gets or Sets the mode.
		/// </summary>
		public Mode CurrentMode { get; set; }

		/// <summary>
		/// Gets or Sets the number of the scenario item.
		/// </summary>
        public int ScenarioState { get; set; }

		/// <summary>
		/// Gets the number of errors.
		/// </summary>
        public int ErrorNumber { get; protected set; }
		
		private Mode oldMode;
		
		/// <summary>
		/// Gets the current task on current scenario.
		/// </summary>
        public ScenarioItem CurrentTask {
            get
            {
                return ScenarioData[ScenarioState];
            }
        }

		// Use this for initialization
		void Start () {
			CurrentMode = Mode.Assisted;
			keyb = MiddleVR.VRDeviceMgr.GetKeyboard ();
			if (keyb == null)
				Debug.Log ("Clavier MiddleVR non trouvé");

			loadScenario ("appel");
		}
		
		
		// Update is called once per frame
		void Update () {
			// L'appui sur RETURN recharge la scène
			if (keyb.IsKeyToggled (MiddleVR.VRK_RETURN)) {
				
				Application.LoadLevel ("Kerpape");
			}
		}
		
		


		/// <summary>
		/// Default constructor.
		/// </summary>
        public GameManager()
        {
            ScenarioData = EmptyScenar;
            ScenarioState = 0;
        }

		/// <summary>
		/// Depending on parameters and current state, tell if the interaction is allowed or not.
		/// </summary>
		/// <param name="type">the action type : action or switch</param>
		/// <param name="name">name ot the activated object</param>
		/// <returns>Bool : True if the action is allowed.</returns>
        public bool isAuthorized(string type, string name)
        {
			//Should perhaps use enum/typeof instead
            if (type == "Action") // Any action is authorized
            {
                // reset original settings
				//This will not result in the correct behavior
				//CurrentMode = oldMode;
                return true;
            }
			/*
			if (CurrentTask.modeOverride != null) 
            {
				//set new mode
				oldMode = CurrentMode;
				CurrentMode = (Mode) CurrentTask.modeOverride;
            }
            */
            if (name == CurrentTask.elementName)
            {
				CurrentTask.stopAction();
                ScenarioState++;
                ErrorNumber = 0;
                if (CurrentTask.isEnd())
                {
                    //End scenario
                }
				CurrentTask.startAction();

                return true;
            }
            else if (CurrentTask.authAll)
            {
				// call onError ?
                return true;
            }
            else
            {
                onError();
                return false;
            }
        }
        /*
        public void nextStep(string activatedItem)
        {
            if (activatedItem == CurrentTask.elementName)
            {
                ScenarioState++;
                ErrorNumber = 0;
                if (CurrentTask.isEnd())
                {
                    //End scenario
                }
            }
            else
            {
                onError();
            }
        }
        */

		/// <summary>
		/// Method called on user error (activation of an unallowed item).
		/// </summary>
        public void onError()
        {
            ErrorNumber++;
            if (ErrorNumber > 20)
            {
                // Self destroy
            }
            else if (ErrorNumber > 12)
            {
                // Show solution
            }
            else if (ErrorNumber > 6)
            {
                // Give  precise instructions
            }
            else if (ErrorNumber > 3)
            {
                // Tell number of error and give an explanation of the objective
            }
        }

		/// <summary>
		/// Change current scenario
		/// </summary>
		/// <param name="name">Name of the scenario to load</param>
		public void loadScenario(string nom) {
			CurrentTask.stopAction ();
			ErrorNumber = 0;
			ScenarioState = 0;
			switch (nom) {
				case "appel":
						ScenarioData = Scenar1;
						break;
				case "infirmier":
						ScenarioData = Scenar2;
						break;
				case "inconnu":
						ScenarioData = Scenar3;
						break;
				case "aucun":
						ScenarioData = EmptyScenar;
						break;
			}
			CurrentTask.startAction();
		}
    }
}
