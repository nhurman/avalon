using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;

namespace Modelisation
{
	/// <summary>
	/// Base class of any object.
	/// </summary>
    public abstract class SceneObject : MonoBehaviour
    {
		/// <summary>
		/// Name that the element will tell to the GameManager. Used for scenarii.
		/// </summary>
        public string identifiant;

		/// <summary>
		/// Type of the object.
		/// </summary>
        protected string type;

		/// <summary>
		/// Default name of the game manager gameobject.
		/// </summary>
		private static string managerName = "GameManager";
		
		/// <summary>
		/// Gets the instance of the GameManager.
		/// </summary>
        public GameManager Manager
        {
            get
            {
                return GameObject.Find(managerName).GetComponent<GameManager>();
            }
        }
    
		/// <summary>
		/// Call the gamemanager to know if the object activation is allowed
		/// </summary>
		/// <returns>Bool true if the activation is allowed, else false</returns>
        public bool notifyGameManager()
        {
            return Manager.isAuthorized(type, identifiant);
            //return true;
        }
    }
}
