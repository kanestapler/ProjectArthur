using UnityEngine;
using System.Collections;

public class GlobalController : MonoBehaviour {

    [HideInInspector]
    public bool gameReady;
    [HideInInspector]
    public bool gameOver;
    public Transform crownSpawn;
    public AudioClip CrownDrop;
    public AudioClip WinningSong;

    private GameObject whoHasTheCrown;
    private AudioSource audioSource;

    private GameObject crown;

	// Use this for initialization
	void Start () {
        gameOver = false;
        gameReady = false;
        whoHasTheCrown = null;
        crown = GameObject.Find("Crown");
        audioSource = GetComponent<AudioSource>();
	}

    public bool IsCrownPossessed() {
        if (whoHasTheCrown == null) {
            return false;
        } else {
            return true;
        }
    }

    public void GiveMeTheCrown(GameObject player, Transform newCrownParent) {
        whoHasTheCrown = player;
        crown.transform.parent = newCrownParent;
        crown.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        crown.transform.localRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
    }

    public void ResetTheCrown() {
        audioSource.Stop();
        audioSource.clip = CrownDrop;
        audioSource.Play();
        whoHasTheCrown = null;
        crown.transform.parent = null;
        crown.transform.position = crownSpawn.position;
        crown.transform.rotation = crownSpawn.rotation;
    }

    public GameObject WhoHasTheCrown() {
        return whoHasTheCrown;
    }

    public void GameOver(int WinningPlayerNumber) {
        gameOver = true;
        if (audioSource.clip != WinningSong) {
            audioSource.Stop();
            audioSource.clip = WinningSong;
            audioSource.Play();
        }
    }
}
