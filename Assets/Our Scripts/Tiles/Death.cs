using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {
	LevelManager levelManager;

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.Find ("Game Master");
		levelManager = go.GetComponent<LevelManager> ();

	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.tag == "Player") {
			levelManager.resetLevel ();
		}
	}
}
