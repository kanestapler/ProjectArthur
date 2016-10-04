using UnityEngine;
using System.Collections;

public class KnightWander : MonoBehaviour {

    public float speed = 1.0f;
    public float distanceFromWall = 10;

    private NavMeshAgent NMAgent;

    void Start() {
        NMAgent = GetComponent<NavMeshAgent>();
        NMAgent.destination = new Vector3(0.0f,0.0f,0.0f);
    }

    void Update() {
        print(FrontHit());
        //transform.Translate(0.0f,0.0f,0.0f);
    }

    private bool FrontHit () {
        RaycastHit RCHit;
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, direction, out RCHit, distanceFromWall)) {
            print("I HIT IT");
            print(RCHit.distance);
        }
        return Physics.Raycast(transform.position, direction, out RCHit, distanceFromWall);
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