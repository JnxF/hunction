using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Extra : MonoBehaviour {

	private const string exampleCoupon = "51A8GQ3A091";

	void Start() {

	}

	void checkCoupon(string inputCoupon) {
		if (inputCoupon == exampleCoupon) return true;
		return false;
	}

	void Update() {

	}
}