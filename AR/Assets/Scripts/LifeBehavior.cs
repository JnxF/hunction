using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBehavior : MonoBehaviour {
	public float maxHealth;
	public float damage;
	public Transform healthBar;

	private float health = 0;

	private Vector3 maxPos, maxScale;

	void Start() {
		maxPos = healthBar.localPosition;
		maxScale = healthBar.localScale;
	}

	void Update() {
		if (maxHealth != 0) {
			var rate = health / maxHealth;
			var add = (rate-1)/2;
			healthBar.localPosition = Vector3.Scale(maxPos, new Vector3(rate, 1.0f, 1.0f)) + new Vector3(add, 0.0f, 0.0f);
			healthBar.localScale = Vector3.Scale(maxScale, new Vector3(rate, 1.0f, 1.0f));
		}
	}

	public void SetMaxHealth() {
		health = maxHealth;
	}

	public void Hit() {
		health -= damage;
	}

	public float GetHealth() {
		return health;
	}
}
