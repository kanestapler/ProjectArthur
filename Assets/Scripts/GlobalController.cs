using UnityEngine;
using System.Collections;

public class GlobalController : MonoBehaviour {

    [HideInInspector]
    public bool gameReady;
    public Transform crownSpawn;

    private GameObject whoHasTheCrown;

    private GameObject crown;

	// Use this for initialization
	void Start () {
        gameReady = false;
        whoHasTheCrown = null;
        crown = GameObject.Find("Crown");
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
        whoHasTheCrown = null;
        crown.transform.parent = null;
        crown.transform.position = crownSpawn.position;
        crown.transform.rotation = crownSpawn.rotation;
    }

    public GameObject WhoHasTheCrown() {
        return whoHasTheCrown;
    }
}
