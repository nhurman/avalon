using UnityEngine;
using System.Collections;

public class AffichageSymbolique : MonoBehaviour {

	private Light spotLight;
	private Material material;
	private bool isOn = false;
	private float timer = 0f;

	public float lightIntensity = 0.25f;// Intensité de la lumière à son maximum
	public float total = 3f;  // Temps total ou l'affichage est allumé
	public float fadeIn = 1f; // Durée d'allumage de la lumière
	public float fadeOut = 1f;// Durée d'extinction de la lumière
	


	//TESTS
	public Texture lightTexture;
	public Texture doorTexture;
	public Texture shuttersTexture;

	// Use this for initialization
	void Start () {

		/*GameObject lightGameObject = new GameObject("The Light");
		Light lightComp = lightGameObject.AddComponent<Light>();
		lightComp.color = Color.blue;
		lightGameObject.transform.position = new Vector3(0, 5, 0);*/
		spotLight = gameObject.GetComponentInChildren<Light>();
		spotLight.intensity = 0f;

		material = gameObject.GetComponent<MeshRenderer>().material;
		material.mainTexture = lightTexture;

		StartCoroutine("test");
	}

	IEnumerator test()
	{
		yield return new WaitForSeconds(5);
		activer ();
		material.mainTexture = doorTexture;
		yield return new WaitForSeconds(3);
		material.mainTexture = shuttersTexture;
	}

	// Update is called once per frame
	void Update () {
		if(isOn)
		{
			if(timer <= fadeIn)
			{
				spotLight.intensity = timer/fadeIn * lightIntensity;
			}
			if(timer >= total - fadeOut)
			{
				spotLight.intensity = (total-timer)/fadeOut * lightIntensity;
				if(timer > total)
				{
					isOn = false;
					spotLight.intensity = 0f;
				}
			}

			timer += Time.deltaTime;
		}
	}

	public void activer()
	{
		isOn = true;
		timer = 0f;
	}
}
