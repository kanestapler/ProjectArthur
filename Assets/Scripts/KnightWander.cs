using UnityEngine;
using System.Collections;

public class KnightWander : MonoBehaviour {

    public float speed = 1.0f;
    public float distanceFromWall = 1;

    void Start() {

    }

    void Update() {
        FrontHit();
    }

    private bool FrontHit () {
        RaycastHit RCHit;
        Vector3 direction = new Vector3(0,0,0);
        print(Physics.Raycast(transform.position, direction, out RCHit, distanceFromWall));
        return true;
    }

    private bool LeftHit() {
        return true;
    }

    private bool RightHit() {
        return true;
    }

    private bool BackHit() { //Maybe not needed?
        return true;
    }

}