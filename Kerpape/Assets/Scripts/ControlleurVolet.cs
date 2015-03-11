using UnityEngine;
using System.Collections;

public class ControlleurVolet : MonoBehaviour {

	private bool mouvement;
	private Vector3 directionTranslation;
	private Transform volet;
	private Transform positionHaute;
	private Transform positionBasse;
	private float vitesse = 0.005f;

	void Start (){

		directionTranslation = Vector3.forward;
		mouvement = false;
		volet = transform.Find ("volet");
		positionHaute = transform.Find ("position_haute");
		positionBasse = transform.Find ("position_basse");

	}
	void Update () {
		if (mouvement) {
			verifieLimite ();
		}

		if (mouvement) {

			volet.Translate(directionTranslation * vitesse);

		}
	
	}
	
	public void monte(){

		directionTranslation = Vector3.forward;
		mouvement = true;

	}

	public void descend(){

		directionTranslation = Vector3.back;
		mouvement = true;

	}

	public void stop(){

		mouvement = false;
	}

	private void verifieLimite (){

		if(directionTranslation == Vector3.forward){

			mouvement = volet.localPosition[2] < positionHaute.localPosition[2];

		}else{

			mouvement = volet.localPosition[2] > positionBasse.localPosition[2]; 

		}

	}


}









