using UnityEngine;
using System.Collections;

public class ControlleurPorte : MonoBehaviour {

	private bool mouvement;
	private Vector3 directionTranslation;
	private Transform porte;
	private Transform positionOuverte;
	private Transform positionFermee;
	private Transform positionIntermediaire;
	private Vector3 cible;
	private float vitesse = 0.005f;


	void Start () {

		mouvement = false;
		porte = transform.Find ("porte");
		positionOuverte = transform.Find ("position_ouverte");
		positionFermee = transform.Find ("position_fermee");
		positionIntermediaire = transform.Find ("position_fermee");
		cible = positionFermee.position;

	}
	

	void Update () {

			porte.position = Vector3.Lerp (porte.position, cible, Time.deltaTime);

	}

	public void fermerPorte(){

		cible = positionFermee.position;
	}

	public void ouvrirPorte(){

		cible = positionOuverte.position;
	}
}
