using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour {
	private LevelManager levelManager;
	private bool complete;

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.Find ("Game Master");
		levelManager = go.GetComponent<LevelManager> (); 
		complete = false;
	}

	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D collider) {
		if (!complete && collider.tag == "Player") {
			complete = true;
			levelManager.advanceLevel();
		}
		
	}
}


