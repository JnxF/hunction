using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Extra : MonoBehaviour {

	public string stringToEdit = "pepe";
    private TouchScreenKeyboard keyboard;
	private const string exampleCoupon = "pepe";
	private bool showGUI;
	public Button optionsButton;
	public GyroCamera cam;
	public GameObject seta;
	public Material otherMaterial;

	void Start() {
		showGUI = false;
		optionsButton.onClick.AddListener(openOptions);
	}

	void openOptions() {
		showGUI = true;
	}

	bool checkCoupon(string inputCoupon) {
		if (inputCoupon == exampleCoupon) return true;
		return false;
	}

	void Update() {

	}

    void OnGUI() {
    	if (showGUI) {
	        stringToEdit = GUI.TextField(new Rect(10, 10, 300, 30), stringToEdit, 30);
	        //GUI.backgroundColor = Color.blue;
	        GUIStyle customButton = GUI.skin.button;
 		    customButton.fontSize = 44;
	        if (GUI.Button(new Rect(10, 50, 300, 55), "Check coupon")) {
	        	if (stringToEdit != null) {
		        	showGUI = false;
		            if (checkCoupon(stringToEdit)) {
		            	Debug.Log("Cambiado de seta");
		            	seta.gameObject.GetComponent<SkinnedMeshRenderer>().material = otherMaterial;
		            }
	        	}
			}
			int pos = 110;
			if (cam.GetState() == GyroCamera.States.WaitingToArrive) {
		        if (GUI.Button(new Rect(10, pos, 300, 55), "Skip")) {
		        	cam.Skip();
		        	showGUI = false;
		    	}
		    	pos += 60;
		    }
			if (GUI.Button(new Rect(10, pos, 300, 55), "Restart")) {
	        	cam.Restart();
	        	showGUI = false;
	        }
	        if (GUI.Button(new Rect(10, pos+60, 300, 50), "Close")) {
	        	showGUI = false;
	        }
	    }
        /*if (GUI.Button(new Rect(10, 150, 200, 100), "ASCIICapable"))
        {
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.ASCIICapable);
        }
        if (GUI.Button(new Rect(10, 250, 200, 100), "Numbers and Punctuation"))
        {
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.NumbersAndPunctuation);
        }
        if (GUI.Button(new Rect(10, 350, 200, 100), "URL"))
        {
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.URL);
        }
        if (GUI.Button(new Rect(10, 450, 200, 100), "NumberPad"))
        {
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.NumberPad);
        }
        if (GUI.Button(new Rect(10, 550, 200, 100), "PhonePad"))
        {
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.PhonePad);
        }
        if (GUI.Button(new Rect(10, 650, 200, 100), "NamePhonePad"))
        {
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.NamePhonePad);
        }
        if (GUI.Button(new Rect(10, 750, 200, 100), "EmailAddress"))
        {
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.EmailAddress);
        }
        if (GUI.Button(new Rect(10, 850, 200, 100), "Social"))
        {
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Social);
        }
        if (GUI.Button(new Rect(10, 950, 200, 100), "Search"))
        {
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Search);
        }*/
    }
}