using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    public class GameManager : MonoBehaviour
    {
        public IEnumerable<ScenarioItem> ScenarioData { get; protected set; }

        public int Mode { get; set; }

        public int ScenarioState { get; set; }

        public int ErrorNumber { get; protected set; }
        public ScenarioItem CurrentTask {
            get
            {
                return ScenarioData[ScenarioState];
            }
        }

        public bool isAuthorised()
        {
            string activatedItem = "";
            if (CurrentTask.authAll || CurrentTask.elementName == activatedItem)
            //ScenarioData[ScenarioState]
            return true;
        }

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
