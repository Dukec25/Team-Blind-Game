using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	private LevelManager levelManager;
	private float startPosition; // We want to keep the start pos so we can go back and forth around it.
	private Rigidbody2D body;

	// Use this for initialization
	void Awake () {
		GameObject go = GameObject.Find ("Game Master");
		levelManager = go.GetComponent<LevelManager>();

		startPosition = transform.position.x;
		body = GetComponent<Rigidbody2D>();
		body.velocity = new Vector2(2, 0);
		
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.tag == "Player") {
			levelManager.resetLevel();
			return;
		}

		string[] solidTiles = new string[]{
			"Block",
			"Enemy",
			"Left_turret",
			"Right_turret",
			"Bounce"
		};

		for (var i = 0; i < solidTiles.Length; ++i) {
			// TODO The y check is so that it doesn't collide with the floor below it. Not sure how to do this properly.
			if (collider.tag == solidTiles[i] && collider.bounds.center.y == transform.position.y) {
				body.velocity *= -1;
				return;
			}
		}
		
	}

	// Move the enemy back and forth around the starting position.
	// When it gets far enough to the right, it will turn around and continue left, and vice-versa.
	void Update(){
		float RANGE_RADIUS = 3; // Also an arbitrary value 

		float newPosition = transform.position.x;

		if (Mathf.Abs(newPosition - startPosition) > RANGE_RADIUS) {
			var distanceBeyondRange =  newPosition - (startPosition + RANGE_RADIUS * Mathf.Sign(newPosition - startPosition));
			newPosition -= 2 * distanceBeyondRange;
			body.velocity *= -1;
		}
		transform.position.Set (newPosition, transform.position.y, transform.position.z);
	}
}





