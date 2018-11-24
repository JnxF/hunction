using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TappingGame : MonoBehaviour {

	private int health;
	public Text displayHealth;

	void Start () {
		health = 100;
	}
	
	void Update () {
		displayHealth.text = health.ToString();		
	}

	void checkTap () {
	
	}
}
