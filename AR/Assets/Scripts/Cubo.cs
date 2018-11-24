using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cubo : MonoBehaviour {

	[HideInInspector]
	public float health;
	private float damageRate;

	//public GameObject cube;
	//public GameObject camera;
	public GameObject Net;
	public GameObject seta;
	private Animation setaA;

	public Text displayScore;

	public Button profileButton;

	private float battleStart = -1f;

	private float totalScore;

	void Start () {
		setaA = seta.GetComponent<Animation>();
		totalScore = 0;
		health = 100f;
		damageRate = 1f;
		//profileButton.onClick.AddListener(showScore);
	}

	/*void showScore() {
		displayScore.text = "Total: " + totalScore.ToString();
	}*/
	
	void onTap(){
		if (health <= 0) {
			return;
		}

		Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			if(hit.collider.gameObject.name == "Cube") {
				setaA.Play("Damage");
				if (battleStart == -1) battleStart = Time.time;
				//Debug.Log("Haciendo daño, vida actual: " + health);
				health -= damageRate;
				if (health <= 0) deadCube();
				//cube.gameObject.GetComponent<MeshRenderer>().material.color = cRed;
			} 
		}
	}

	void deadCube() {
		//Prize
		setaA.Play("Death");
		float totalTime = Time.time - battleStart;
		battleStart = -1;
		float points = (10f-totalTime);
		if (points > 0) {
			displayScore.text = points + " points!";
			totalScore += points;
		} else {
			displayScore.text = "Better luck next time :(";
		}
	}

	void Update () {
		if (Input.touchCount > 0) onTap();
		//else cube.gameObject.GetComponent<MeshRenderer>().material.color = currentColor;
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