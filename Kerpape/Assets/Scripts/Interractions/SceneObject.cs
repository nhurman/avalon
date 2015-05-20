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
        //Should be static with stored value, but perhaps there is a better way to achieve this
        public static string managerName = "GameManager";
        public string identifiant;
        protected string type;
		//private GameManager _manager;
		
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
    
        //Perhaps with parameters ? or Send name of current calling method
		/// <summary>
		/// Call the gamemanager to know if the object activation is allowed
		/// </summary>
		/// <returns>Bool true if the activation is allowed, else false</returns>
        public bool notifyGameManager()
        {
            return Manager.isAuthorised(type, identifiant);
            //return true;
        }

		/*private void Start () {
			_manager = GameObject.Find (managerName).GetComponent<GameManager> ();
		}*/

        //Type Method ?
    }
}
