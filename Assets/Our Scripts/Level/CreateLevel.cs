using System.Collections.Generic;
using UnityEngine;

public class LevelCreator{
	public GameObject block;
	public string path; 

	private List<GameObject> tileList = new List<GameObject>();
	private int BLOCK_INDEX = 2;
	private int WALL_HEIGHT = 30;

	private List<GameObject> tileCache = new List<GameObject>(); // Cache placed tiles so that we can delete them later

	public void createTile(GameObject tile, Vector3 pos) {
		GameObject createdTile = GameObject.Instantiate(tile, pos, Quaternion.identity);
        if (PreferenceManager.Instance.blind) {
            Renderer renderer = createdTile.GetComponent<Renderer>();
            if (renderer != null) {
                renderer.enabled = false;
            }
        }
		tileCache.Add(createdTile);
	}

    public void destroyLevel() {
        for (int i = 0; i < tileCache.Count; i += 1) {
            GameObject.Destroy(tileCache[i]);
        }

        // Bullets need to be destroyed manually, since they are not a part of the level itself
        var bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (var bullet in bullets) {
            GameObject.Destroy(bullet);
        }

        var player = GameObject.Find("Player");
        player.GetComponent<AudioSource>().Stop();
    }

	public void createLevel(Level level){
		int tile;

		tileCache = new List<GameObject>();

		// Load tiles into the tileset here.  These correspond to the tiles being loaded from the level file.
		// For example, a 2 in the level file means that a standard block will be in that row/col
		tileList.Add(null);											// 0 is nothing
		tileList.Add(null);											// 1 is the player's starting position
		tileList.Add(Resources.Load("Block") as GameObject);		// 2 is a standard block
		tileList.Add(Resources.Load("Exit") as GameObject);			// 3 is the exit to the level
		tileList.Add(Resources.Load("Enemy") as GameObject);    	// 4 is an enemy
		tileList.Add(Resources.Load("LeftEdge") as GameObject); 	// 5 is the left edge of a platform
		tileList.Add(Resources.Load("RightEdge") as GameObject);	// 6 is the right edge of a platform
		tileList.Add(Resources.Load("Bounce") as GameObject);		// 7 is a bounce pad
		tileList.Add(Resources.Load("Left_turret") as GameObject);	// 8 is the left of a turret 
		tileList.Add(Resources.Load("Right_turret") as GameObject);	// 9 is the right of a turret 
		tileList.Add(Resources.Load("Death") as GameObject);        // 10 causes death when touched


		// Create the walls
		for (int i = 0; i < level.height; i += 1) {
			var leftWallPos = new Vector3(-1 - level.width / 2, i + 1, 0);
			var rightWallPos = new Vector3(level.width % 2 + level.width / 2, i + 1, 0);

			createTile(tileList[BLOCK_INDEX], leftWallPos);
			createTile(tileList[BLOCK_INDEX], rightWallPos);
		}

		// Create the level based on what was in the file
		for (int i = 0; i < level.height; i += 1) {
			for (var j = 0; j < level.width; j += 1) {
				tile = level.tiles[i, j];
				if (tile >= 2) {
					var blockPos = new Vector3(j - level.width / 2, level.height - i, 0);
					createTile(tileList[tile], blockPos);
				} else if (tile == 1) {
					var player = GameObject.Find("Player");
					player.transform.position = new Vector3(j - level.width / 2, level.height - i, 0);

                    Rigidbody2D rigidbody = player.GetComponent<Rigidbody2D>();
					if (rigidbody != null) {
						rigidbody.velocity = Vector2.zero;
					}

                    player.GetComponent<AudioSource>().Play();
                }
			}
		}
    }
}
