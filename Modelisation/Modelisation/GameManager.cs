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

        public bool isAuthorised()
        {
            //ScenarioData[ScenarioState]
            return true;
        }

        public void nextStep()
        {
            //ScenarioData.NEXT
            //ErrorNumber = 0;
            throw new System.NotImplementedException();
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
