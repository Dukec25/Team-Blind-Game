using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class right_turret : MonoBehaviour {
	private float nextActionTime = 0.0f;
	public float period = 3.0f;
	public int velocity;

	void Start() {
		nextActionTime = Time.time;
	}

	// Update is called once per frame
	void Update() {
		if (Time.time > nextActionTime ) {
			nextActionTime += period;
			GameObject bullet = (GameObject)GameObject.Instantiate (Resources.Load ("Bullet"), new Vector2 (this.transform.position.x - 1, this.transform.position.y), Quaternion.identity);
			Rigidbody2D m_Rigidbody2D = bullet.GetComponent<Rigidbody2D>();
			m_Rigidbody2D.velocity = new Vector2(-velocity, m_Rigidbody2D.velocity.y);
		}
	}
}


