using UnityEngine;
using System.Collections;

public class movethatplayer : MonoBehaviour {

    private readonly string JOGGING = "isJogging";
    private readonly string JUMPING = "isJumping";
    private readonly string ATTACK = "attack";

    public float speed;
    public float turnSpeed;
    public float axisThreshold;
    public int fireWaitTime;

    private static Animator ani;
    private float hMove;
    private float vMove;
    private float angH;
    private float angV;
    private int fireTime;

    void Start () {
        ani = GetComponent<Animator>();
        fireTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        hMove = Input.GetAxis("Horizontal");
        vMove = Input.GetAxis("Vertical");
        angH = Input.GetAxis("RightH");
        angV = Input.GetAxis("RightV");

        Move();

        if (Input.GetButtonDown("Fire1")) {
            Fire();
        }
        if (!ReadyToFire()) {
            fireTime++;
        }
        if (Input.GetButtonDown("Jump")) {
            GrabCrown();
        }
    }

    private void GrabCrown() {
        ani.SetBool(JOGGING, false);
        ani.SetTrigger(JUMPING);
    }

    private void Move() {
        if (vMove > 0) { //Moving forward
            ani.SetBool(JOGGING, true);
            transform.Translate(hMove * speed * Time.deltaTime, 0.0f, vMove * speed * Time.deltaTime);
        } else if (vMove < 0) { //Moving backwards
            transform.Translate(hMove * speed * Time.deltaTime, 0.0f, vMove * speed * Time.deltaTime);
        } else {
            ani.SetBool(JOGGING, false);
        }
        if (Mathf.Abs(angV) > axisThreshold || Mathf.Abs(angH) > axisThreshold) {
            transform.Rotate(0, angH * turnSpeed * Time.deltaTime, 0);
        }
    }

    private void Fire() {
        if (ReadyToFire()) {
            fireTime = 1;
            ani.SetTrigger(ATTACK);
        }
    }


    private bool ReadyToFire() {
        return fireTime % fireWaitTime == 0;
    }
}
