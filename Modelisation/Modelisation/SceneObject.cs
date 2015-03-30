﻿using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;

namespace Modelisation
{
    public abstract class SceneObject : MonoBehaviour
    {
        //Should be static with stored value, but perhaps there is a better way to achieve this
        public static string managerName = "GameManager";
        public GameManager Manager
        {
            get
            {
                return GameObject.Find(managerName).GetComponent<GameManager>();
            }
        }
    
        //Perhaps with parameters ? or Send name of current calling method
        public bool notifyGameManager()
        {
            //throw new System.NotImplementedException();
            return Manager.isAuthorised();
            //return true;
        }
    }
}
