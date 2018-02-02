using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echo : MonoBehaviour {

	private Rigidbody2D m_Rigidbody2D;
	private double acceleration;
	private int initial_velocity;
	private int max_velocity;
	private int time;

	void Start () {
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		acceleration = 0.3;
		initial_velocity = 2;
		max_velocity = 40;
		time = 0;

        if (PreferenceManager.Instance.blind) {
            Renderer m_Renderer = GetComponent<Renderer>();
            m_Renderer.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
		int new_velocity = (int)(initial_velocity + acceleration * time);
		time += 1;
		if (new_velocity > max_velocity) {
			return;
		}

		if (m_Rigidbody2D.velocity.x < 0) {
			new_velocity = -new_velocity; 
		}

		m_Rigidbody2D.velocity = new Vector2(new_velocity, m_Rigidbody2D.velocity.y);
	}


	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag != "Player" && col.tag != "Echo") {
			AudioSource hitNoise = col.gameObject.GetComponent<AudioSource>();

			hitNoise.panStereo = (float)(transform.position.x > col.transform.position.x ? -1.0 : 1.0);
			hitNoise.Play ();

			Destroy (this.gameObject);
		}
	}
}
