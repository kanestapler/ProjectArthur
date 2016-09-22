using UnityEngine;
using System.Collections;

public class movethatplayer : MonoBehaviour {

    public float speed;
    public float turnSpeed;

    private static Animator ani;
    private float hMove;
    private float vMove;

    void Start () {
        ani = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        hMove = Input.GetAxis("Horizontal");
        vMove = Input.GetAxis("Vertical");
        if (Input.GetButtonDown("Jump")) {
            GrabCrown();
        } else if (vMove > 0.2) {
            ani.SetBool("isJogging", true);
        } else {
            ani.SetBool("isJogging", false);
        }
        Move();
    }

    private void GrabCrown() {
        ani.SetTrigger("isjumping");
    }

    private void Move() {
        transform.Translate(hMove * turnSpeed * Time.deltaTime, 0.0f,vMove * speed * Time.deltaTime);

    }
}
