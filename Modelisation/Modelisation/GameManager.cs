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
        public IEnumerable<ScenarioItem> ScenarioData
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int Mode
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int ScenarioState
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int ErrorNumber
        {
            get;
            protected set;
        }

        public bool isAuthorised()
        {
            throw new System.NotImplementedException();
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
            if (ErrorNumber > 10)
            {
                // Do something
            }
            throw new System.NotImplementedException();
        }
    }
}
