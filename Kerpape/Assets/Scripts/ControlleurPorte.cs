using UnityEngine;
using System.Collections;

public class ControlleurPorte : MonoBehaviour {
	
	private bool timerActif;
	private bool mouvement;
	private Vector3 directionTranslation;
	private Transform porte;
	private Vector3  positionOuverte;
	private Vector3 positionFermee;
	private Vector3 positionIntermediaire;
	private Vector3 cible;
	private bool obstacle;
	private float instantOuverture = 0f;
	private float dureeOuverture = 5f;
	private float vitesse = 1f;
	
	
	void Start () {
		
		timerActif = true;
		obstacle = false;
		porte = transform.Find ("porte");
		positionOuverte = transform.Find ("position_ouverte").position;
		positionFermee = transform.Find ("position_fermee").position;
		positionIntermediaire = transform.Find ("position_fermee").position;
		cible = positionOuverte;
		
	}
	
	
	void Update () {
		
		//ligne de test
		if (Input.GetMouseButtonDown (1)) {
			collision();
		}
		if (Input.GetMouseButtonDown (0)) {
			ouvrirPorte();
		}
		//fin ligne de test
		
		if(cible == positionIntermediaire && (int)(porte.position[2]*100) == (int)(positionIntermediaire[2]*100 + 5)){
			
			obstacle = false;
			cible = positionFermee;
		}
		
		if(timerActif && Time.fixedTime > dureeOuverture + instantOuverture ){
			
			timerActif = false;
			fermerPorte();
		}
		
		porte.position = Vector3.Lerp (porte.position, cible, Time.deltaTime * vitesse);
		
	}
	
	public void fermerPorte(){
		
		if (obstacle) {
			
			cible = positionIntermediaire;
			
		} else {
			
			cible = positionFermee;
		}
	}
	
	public void ouvrirPorte(){
		
		instantOuverture = Time.fixedTime;
		timerActif = true;
		cible = positionOuverte;
	}
	
	public void collision(){
		
		obstacle = true;
		positionIntermediaire[2] = porte.position[2] - 0.01f;
		ouvrirPorte ();
		
	}
}
