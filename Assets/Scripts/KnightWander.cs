using UnityEngine;
using System.Collections;

public class KnightWander : MonoBehaviour {

    public float speed = 1.0f;
    public float distanceFromWall = 10;
    public Vector3 destination1;
    public Vector3 destination2;
    public float distanceThreshhold = 3.0f;

    private NavMeshAgent NMAgent;
    private int currentDestination;

    void Start() {
        NMAgent = GetComponent<NavMeshAgent>();
        NMAgent.destination = new Vector3(0.0f,0.0f,0.0f);
        currentDestination = 1;
    }

    void Update() {
        if (ShouldITurnAround()) {
            if (currentDestination == 1) {
                NMAgent.destination = destination2;
                currentDestination = 2;
            } else {
                NMAgent.destination = destination1;
                currentDestination = 1;
            }
        }
    }

    private bool ShouldITurnAround() {
        if (currentDestination == 1) {
            if (Vector3.Distance(transform.position, destination1) <= distanceThreshhold) {
                return true;
            }
        } else {
            if (Vector3.Distance(transform.position, destination2) <= distanceThreshhold) {
                return true;
            }
        }
        return false;
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

}