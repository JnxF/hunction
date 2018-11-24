using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cubo : MonoBehaviour {

	private float health;
	private float damageRate;
	public Text displayHealth;

	public GameObject cube;
	private Color cBlack = Color.black;
	private Color cRed = Color.red;
	private Color cGreen = Color.green;

	void Start () {
		health = 100f;
		damageRate = 1f;
		displayHealth.text = health.ToString();
		cube.gameObject.GetComponent<MeshRenderer>().material.color = cGreen;
	}
	
	void onTap(){
		Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			Debug.Log("Clicked: " + hit.collider.gameObject.name);
			if(hit.collider.gameObject.name == "Cube") {
				Debug.Log("Haciendo daño, vida actual: " + health);
				health -= damageRate;
				if (health <= 0) deadCube();
				cube.gameObject.GetComponent<MeshRenderer>().material.color = cRed;
			} 
		}
	}

	void deadCube() {
		Destroy(gameObject);
	}

	void Update () {
		if (Input.touchCount > 0) onTap();
		else cube.gameObject.GetComponent<MeshRenderer>().material.color = cGreen;
	}
}



/*
	void initVariables(){
		//Stamina/Transparency intitial values
		alphaColor = cube.GetComponent<MeshRenderer>().material.color;
		alphaColor.a = 0;
		cube.GetComponent<MeshRenderer>().material.color = Color.Lerp(cube.GetComponent<MeshRenderer>().material.color, alphaColor, 1f); //Lerp in one single step (1f)
		fadeSpeed = 10f;
	}

	void staminaIndicator(float staminaValue){
		alphaColor.a = 1 - (staminaValue / 100);
		//cube.GetComponent<MeshRenderer>().material.color.a = //Color.Lerp(cube.GetComponent<MeshRenderer>().material.color, alphaColor, fadeSpeed * Time.deltaTime);
	}
*/