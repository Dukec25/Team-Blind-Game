using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

	private LevelManager levelManager;
	void Start () {
		GameObject go = GameObject.Find ("Game Master");
		levelManager = go.GetComponent<LevelManager> ();

        if (PreferenceManager.Instance.blind)
        {
            Renderer m_Renderer = GetComponent<Renderer>();
            m_Renderer.enabled = false;
        }
    }
	
	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.tag == "Player") {
			levelManager.resetLevel();
			return;
		}

		string[] solidTiles = new string[]{
			"Block",
			"Enemy",
			"Left_turret",
			"Right_turret"
		};

		for (int i = 0; i < solidTiles.Length; ++i) {
			if (collider.tag == solidTiles[i]) {
				Destroy(this.gameObject);
				return;
			}
		}
	}
}

