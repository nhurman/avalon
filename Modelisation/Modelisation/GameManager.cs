using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    enum Mode { Auto, Assisted, Symbolic };
    public class GameManager : MonoBehaviour
    {
        public static IList<ScenarioItem> Scenar1 = new List<ScenarioItem>() 
        {
            new ScenarioItem("aaa", "desc")
        };

        public static IList<ScenarioItem> Scenar2 = new List<ScenarioItem>() 
        {
            new ScenarioItem("aaa", "desc")
        };

        public static IList<ScenarioItem> Scenar3 = new List<ScenarioItem>() 
        {
            new ScenarioItem("aaa", "desc")
        };
        public IList<ScenarioItem> ScenarioData { get; protected set; }

        public Mode CurrentMode { get; set; }

        public int ScenarioState { get; set; }

        public int ErrorNumber { get; protected set; }
        public ScenarioItem CurrentTask {
            get
            {
                return ScenarioData[ScenarioState];
            }
        }

        public bool isAuthorised(string name)
        {
            if (Enum.IsDefined(typeof(Mode), CurrentTask.modeOverride)) 
            {

            }
            if (name == CurrentTask.elementName)
            {
                ScenarioState++;
                ErrorNumber = 0;
                if (CurrentTask.isEnd())
                {
                    //End scenario
                }
                switch (CurrentMode)
                {
                    case Mode.Auto:
                        break;
                    case Mode.Assisted:
                        break;
                    case Mode.Symbolic:
                        break;

                }
                return true;
            }
            else if (CurrentTask.authAll)
            {
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
        public void onError()
        {
            ErrorNumber++;
            if (ErrorNumber > 20)
            {
                // Self destroy
            }
            else if (ErrorNumber > 15)
            {
                // Show solution
            }
            else if (ErrorNumber > 10)
            {
                // Give  precise instructions
            }
            else if (ErrorNumber > 5)
            {
                // Tell number of error and give an explanation of the objective
            }
           
        }

    }
}
