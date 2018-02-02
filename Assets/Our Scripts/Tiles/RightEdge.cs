using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightEdge : MonoBehaviour {
	public AudioSource WARNING_BEEP;

	void Start () {
		WARNING_BEEP.panStereo = 1.0f;
	}
	
	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.tag == "Player") {
			WARNING_BEEP.Play();
		}
	}
}

