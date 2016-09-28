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
        hMove = Input.GetAxis("Horizontal" + playerNumber);
        vMove = Input.GetAxis("Vertical" + playerNumber);
        angH = Input.GetAxis("RightH" + playerNumber);
        angV = Input.GetAxis("RightV" + playerNumber);
        

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
            speed = forwardSpeed;
        } else if (vMove < -strafeThreshold) { //Moving backwards
            ani.SetBool(BACKWARDS, true);
            speed = backwardsSpeed;
        } else if (hMove > 0) {
            ani.SetBool(JOGGING, false);
            ani.SetBool(STRAFINGRIGHT, true);
            if (playerNumber == 1) {
                print("Player 1 Strafe Right");
            }
        } else if (hMove < 0) {
            ani.SetBool(JOGGING, false);
            ani.SetBool(STRAFINGLEFT, true);
            if (playerNumber == 1) {
                print("Player 1 Strafe Left");
            }
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
