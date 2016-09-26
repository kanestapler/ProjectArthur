using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private readonly string JOGGING = "isJogging";
    private readonly string JUMPING = "isJumping";
    private readonly string ATTACK = "attack";
    private readonly string BACKWARDS = "isWalkingBackwards";
    private readonly string STRAFING = "isStrafing";

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
        Turn();

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
        /*TODO Fix bugs:
        1. Player only moves left/right when up/down is pressed (Fixed)
        2. Add animation so player doesn't float left and right
        Note from Kane: I know why these aren't working right now, but I just can't decide my favorite solution.
         */
        
        if (vMove > 0) { //Moving forward
            ani.SetBool(JOGGING, true);
        } else if (vMove < 0) { //Moving backwards
            //Waiting on backwards walking animation
            //ani.SetBool(BACKWARDS, true);
        } else if (Mathf.Abs(hMove) > 0) {
            ani.SetBool(JOGGING, false);
            //ani.SetBool(STRAFING, true);
        } else {
            ani.SetBool(JOGGING, false);
            //ani.SetBool(STRAFING, false);
        }
        transform.Translate(hMove * speed * Time.deltaTime, 0.0f, vMove * speed * Time.deltaTime);
    }

    private void Turn() {
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
