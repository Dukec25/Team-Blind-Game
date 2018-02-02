using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    private const int SUBTITLE_OFFSET = 2;

	private string path;

	private int nextLevelNum = 1;
	private List<string> levelList = new List<string> ();

	private LevelIO lio = new LevelIO();
	private LevelCreator levelCreator = new LevelCreator();

	private AudioSource completionSound;
	private AudioSource deathSound;
    private AudioSource winSubtitle;
    private List<AudioSource> subtitleList = new List<AudioSource>();

	public void Start(){
		path = Application.dataPath + "/Levels/";
		completionSound = GetComponents<AudioSource>()[0];
		deathSound = GetComponents<AudioSource>()[1];
        winSubtitle = GetComponents<AudioSource>()[2];

        // Add levels that you want to be playable to this list
        levelList.Add("Flat Zone");
		levelList.Add("Press z for echolocation");
		levelList.Add("Monster!");
		levelList.Add("Watch for the hole");
		levelList.Add("Climb the platforms");
		levelList.Add("Introducing the spring");
		levelList.Add("Bouncy fun");
		levelList.Add("Look before you leap");
		levelList.Add("Introducing the turret");
		levelList.Add("Find the spring");
		levelList.Add("Crossfire");
		levelList.Add("Holey staircase");
		levelList.Add("Slot machine");
		levelList.Add("Between two monsters");
		levelList.Add("Bounce for the skies");

        for (int i = 0; i <= levelList.Count; i += 1) {
            if (i + SUBTITLE_OFFSET <= GetComponents<AudioSource>().Length) {
                subtitleList.Add(GetComponents<AudioSource>()[i + SUBTITLE_OFFSET]);
            }
        }

        StartCoroutine(resetLevel(false));

	}

	public void advanceLevel(){
        // If we have reached the last level, just loop
        if (nextLevelNum != levelList.Count) {
            nextLevelNum += 1;
            StartCoroutine(resetLevel(false));
        } else {
            StartCoroutine(endGame());
            Application.Quit();
        }
    }

	public void resetLevel(){
        StartCoroutine(resetLevel (true));
	}

	public System.Collections.IEnumerator resetLevel(bool died){
        levelCreator.destroyLevel();
        if (died) {
			deathSound.Play();
        }
        else {
            completionSound.Play();
            yield return new WaitForSeconds(1);
            if (nextLevelNum <= subtitleList.Count) {
                subtitleList[nextLevelNum].Play();
            }
            yield return new WaitForSeconds(3);
        }

		Level level = lio.importLevel(path + levelList[nextLevelNum - 1]);
		levelCreator.createLevel(level);
        yield return null;
	}

    public System.Collections.IEnumerator endGame() {
        levelCreator.destroyLevel();
        completionSound.Play();
        yield return new WaitForSeconds(1);
        winSubtitle.Play();
        yield return new WaitForSeconds(2);
        yield return null;
    }
}
