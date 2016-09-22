using UnityEngine;
using System.Collections;

public class movethatplayer : MonoBehaviour {
    static Animator ani;
    // Use this for initialization
    void Start () {
        ani = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        float hMove = Input.GetAxis("Horizontal");
        float vMove = Input.GetAxis("Vertical");
        if (Input.GetButtonDown("Jump")) {
            GrabCrown();
        } else if (vMove > 0.2) {
            print("Moving");
            ani.SetBool("isJogging", true);
        } else {
            ani.SetBool("isJogging", false);
        }

    }

    void GrabCrown() {
        ani.SetTrigger("isjumping");
    }
}
