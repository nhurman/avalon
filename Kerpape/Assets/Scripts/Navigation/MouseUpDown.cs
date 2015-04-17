using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;
using System;

public class MouseUpDown : MonoBehaviour
{
	public float sensibility = 1.0f;
	public void FixedUpdate ()
	{

		/*
		 * Objet utilisé pour un test : il faut créer une sphère, ou tout autre objet, dans Unity, et l'appeler BOULE.
		 * Si ce script est présent quelque part, e.g. attaché à la sphère en question, quand on bouge la souris verticalement
		 * ça fera bouger la sphère en conséquence. 
		 */
		//GameObject testBoule = GameObject.Find("BOULE");
		float rotation = 0.0f;
		vrMouse mouse = null;
		if (MiddleVR.VRDeviceMgr.GetMouse() != null)
		{
			mouse = MiddleVR.VRDeviceMgr.GetMouse();
		}

		if (Math.Abs(mouse.GetAxisValue(1)) > 0.1f)
		{
			rotation = mouse.GetAxisValue(1);
		}
		else if(Math.Abs(Input.GetAxis("Mouse Y")) > 0)
		{
			rotation = Input.GetAxis("Mouse Y");
		}
		/*
		 * A décommenter pour le test de la boule, cf plus haut
		 */
		//testBoule.transform.Translate(0,rotation*sensibility, 0);
		transform.Rotate(0, 0, rotation*sensibility);
	}
}
