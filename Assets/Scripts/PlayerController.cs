using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private readonly string JOGGING = "isJogging";
    private readonly string JUMPING = "isJumping";
    private readonly string ATTACK = "attack";
    private readonly string BACKWARDS = "isWalkingBackwards";
    private readonly string STRAFINGRIGHT = "isStrafingRight";
    private readonly string STRAFINGLEFT = "isStrafingLeft";

    public float forwardSpeed;
    public float backwardsSpeed;
    public float strafeSpeed;
    public float turnSpeed;
    public float axisThreshold;
    public int fireWaitTime;
    public float strafeThreshold;

    public int playerNumber;

    private Animator ani;
    private float hMove;
    private float vMove;
    private float angH;
    private float angV;
    private int fireTime;

    private GlobalController GC;

    private GameObject crown;

    void Start () {
        GC = GameObject.Find("GameController").GetComponent<GlobalController>();
        crown = GameObject.FindGameObjectWithTag("Crown");
        ani = GetComponent<Animator>();
        fireTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        hMove = Input.GetAxis("Horizontal" + playerNumber);
        vMove = Input.GetAxis("Vertical" + playerNumber);
        angH = Input.GetAxis("RightH" + playerNumber);
        angV = Input.GetAxis("RightV" + playerNumber);
        
        if (GC.gameReady) {
            Move();
            Turn();
            if (InRangeOfCrown()) {
                GiveMeTheCrown();
            }
            if (Input.GetButtonDown("Fire" + playerNumber)) {
                Fire();
            }
            if (!ReadyToFire()) {
                fireTime++;
            }
            if (Input.GetButtonDown("Jump" + playerNumber)) {
                //GrabCrown();
            }
        }
    }

    private bool InRangeOfCrown() {
        if (playerNumber == 1) {
            return true;
        } else {
            return false;
        }
    }

    private void GiveMeTheCrown() {
        crown.transform.parent = this.transform;
    }

    private void GrabCrown() {
        /*TODO
         * Add in small jump animation
         * This will play when player isn't next to the crown
         * This will prevent the long animation from being played on accident and the player feeling like they are stuck
         * Also would help the player know if they are in the correct position to get the crown
         */
        ani.SetBool(JOGGING, false);
        ani.SetTrigger(JUMPING);
    }

    private void Move() {
        /*TODO Fix bugs:
        1. Make isIdle a bool. This should stop the weird transitions
         */
        float speed = 0.0f;

        if (vMove > strafeThreshold) { //Moving forward
            ani.SetBool(JOGGING, true);
            ani.SetBool(BACKWARDS, false);
            ani.SetBool(STRAFINGRIGHT, false);
            ani.SetBool(STRAFINGLEFT, false);
            speed = forwardSpeed;
        } else if (vMove < -strafeThreshold) { //Moving backwards
            ani.SetBool(BACKWARDS, true);
            ani.SetBool(JOGGING, false);
            ani.SetBool(STRAFINGRIGHT, false);
            ani.SetBool(STRAFINGLEFT, false);
            speed = backwardsSpeed;
        } else if (hMove > 0) {
            ani.SetBool(JOGGING, false);
            ani.SetBool(STRAFINGRIGHT, true);
            ani.SetBool(STRAFINGLEFT, false);
            ani.SetBool(BACKWARDS, false);
        } else if (hMove < 0) {
            ani.SetBool(JOGGING, false);
            ani.SetBool(STRAFINGLEFT, true);
            ani.SetBool(STRAFINGRIGHT, false);
            ani.SetBool(BACKWARDS, false);
        } else {
            ani.SetBool(JOGGING, false);
            ani.SetBool(STRAFINGLEFT, false);
            ani.SetBool(STRAFINGRIGHT, false);
            ani.SetBool(BACKWARDS, false);
        }
        transform.Translate(hMove * strafeSpeed * Time.deltaTime, 0.0f, vMove * speed * Time.deltaTime);
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
