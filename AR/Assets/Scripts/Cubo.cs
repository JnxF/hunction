using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cubo : MonoBehaviour {
	//public GameObject cube;
	//public GameObject camera;
	public GameObject Net;
	public GameObject seta;
	public LifeBehavior health;
	private Animation setaA;

	public Button profileButton;

	void Start () {
		setaA = seta.GetComponent<Animation>();
		setaA["Attack"].wrapMode = WrapMode.Once;
		setaA["Run"].wrapMode = WrapMode.Once;
		health.SetMaxHealth();
		//profileButton.onClick.AddListener(showScore);
	}

	void onTap(){
		if (health.GetHealth() <= 0) {
			return;
		}

		Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			if(hit.collider.gameObject.name == "Cube") {
				setaA.Play("Damage");
				//Debug.Log("Haciendo daño, vida actual: " + health);
				health.Hit();
				if (health.GetHealth() <= 0) deadCube();
				//cube.gameObject.GetComponent<MeshRenderer>().material.color = cRed;
			} 
		}
	}

	void deadCube() {
		//Prize
		setaA.Play("Death");
	}

	void Update () {
		if (Input.touchCount > 0) onTap();
		if (setaA.IsPlaying("Idle")) {
			if (Random.value < 0.005) {
        	    if (Random.value < 0.2) {
        	        setaA.Play("Attack");
        	    } else {
        	    	setaA.Play("Run");
        	    }
        	}
        	setaA.PlayQueued("Idle");
    	}
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