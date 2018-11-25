using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Extra : MonoBehaviour {

	public string stringToEdit;
    private TouchScreenKeyboard keyboard;
	private const string exampleCoupon = "M00sh3um";
	private bool showGUI;
	public Button optionsButton;
	public GyroCamera cam;
	public GameObject seta;
	public Material otherMaterial;

	void Start() {
		showGUI = false;
		optionsButton.onClick.AddListener(toggleOptions);
	}

	void toggleOptions() {
		showGUI = !showGUI;
	}

	bool checkCoupon(string inputCoupon) {
		if (inputCoupon == exampleCoupon) return true;
		return false;
	}

	void Update() {

	}

    void OnGUI() {
    	if (showGUI) {
 		    int inicio = 150;
 		    int buttonHeight = 110;
 		    int menuWidth = 600;
 		    int buttonMargin = 10;
 		    int margenIzq = 25;

			int pos = inicio + 55;

 		    GUIStyle customTextField = GUI.skin.textField;
 		    customTextField.fontSize = 70;
	        stringToEdit = GUI.TextField(new Rect(margenIzq, pos, menuWidth, buttonHeight), stringToEdit, 30, customTextField);
	        pos += buttonHeight + buttonMargin;
	        //GUI.backgroundColor = Color.blue;
	        GUIStyle customButton = GUI.skin.button;
 		    customButton.fontSize = 70;
	        if (GUI.Button(new Rect(margenIzq,  pos, menuWidth, buttonHeight), "Check coupon")) {
	        	if (stringToEdit != null) {
		        	showGUI = false;
		            if (checkCoupon(stringToEdit)) {
		            	cam.ShowText("Red mushroom!");
		            	seta.gameObject.GetComponent<SkinnedMeshRenderer>().material = otherMaterial;
		            } else {
		            	cam.ShowText("Invalid coupon");
		            }
	        	}
			}
			pos += buttonHeight + buttonMargin;
	        if (cam.GetState() == GyroCamera.States.WaitingToArrive) {
		        if (GUI.Button(new Rect(margenIzq,  pos, menuWidth, buttonHeight), "Skip")) {
		        	cam.Skip();
		        	showGUI = false;
		    	}
		    	pos += buttonHeight + buttonMargin;
		    }
			if (GUI.Button(new Rect(margenIzq,  pos, menuWidth, buttonHeight), "Restart")) {
	        	cam.Restart();
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