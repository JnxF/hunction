using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Extra : MonoBehaviour {

	public string stringToEdit = "";
    private TouchScreenKeyboard keyboard;
	private const string exampleCoupon = "51A8GQ3A091";
	private bool showGUI;
	public Button optionsButton;

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

	// Opens native keyboard
    void OnGUI() {
    	if (showGUI) {
	        //stringToEdit = GUI.TextField(new Rect(10, 10, 200, 30), stringToEdit, 30);
	        //GUI.backgroundColor = Color.blue;
	        if (GUI.Button(new Rect(10, 50, 150, 75), "Insert Coupon Here"))
	        {
	            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
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