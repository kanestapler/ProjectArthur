using UnityEngine;
using System.Collections;

public class movethatplayer : MonoBehaviour {

    public float speed;
    public float turnSpeed;

    private static Animator ani;
    private float hMove;
    private float vMove;
    private float angH;
    private float angV;

    void Start () {
        ani = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        hMove = Input.GetAxis("Horizontal");
        vMove = Input.GetAxis("Vertical");
        angH = Input.GetAxis("RightH") * 60;
        angV = Input.GetAxis("RightV") * 45;
        if (Input.GetButtonDown("Jump")) {
            GrabCrown();
        } else if (vMove > 0) { //Moving forward
            ani.SetBool("isJogging", true);
        } else { //Not moving forward
            ani.SetBool("isJogging", false);
        }
        Move();
    }

    private void GrabCrown() {
        ani.SetTrigger("isjumping");
    }

    private void Move() {
        transform.Translate(hMove * speed * Time.deltaTime, 0.0f,vMove * speed * Time.deltaTime);
        transform.Rotate(angV * turnSpeed * Time.deltaTime, angH * turnSpeed * Time.deltaTime, 0);
    }
}
