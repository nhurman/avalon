using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    public abstract class SceneObject : MonoBehaviour
    {
        public GameManager GameManager
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    
        //Perhaps with parameters ? or Send name of current calling method
        public bool notifyGameManager()
        {
            /*throw new System.NotImplementedException();
            return GameManager.isAuthorised();*/
            return true;
        }
    }
}
