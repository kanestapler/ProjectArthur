using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class KnightWander : MonoBehaviour {

    public float speed = 1.0f;
    public float distanceFromWall = 10;
    public Vector3 destination1;
    public Vector3 destination2;
    public float distanceThreshholdToTurnAround = 3.0f;
    public float distanceToStartFollowingPlayer = 7.0f;

    private NavMeshAgent NMAgent;
    private int currentDestination;

    private GameObject[] players;
    private int numberOfPlayers;

    void Start() {
        NMAgent = GetComponent<NavMeshAgent>();
        NMAgent.destination = new Vector3(0.0f,0.0f,0.0f);
        currentDestination = 1;

        players = GameObject.FindGameObjectsWithTag("Player");
        numberOfPlayers = players.Length;
    }

    void Update() {
        GameObject closestPlayer = ShouldIFollowPlayer();
        if (closestPlayer != null) {
            currentDestination = 3;
            NMAgent.SetDestination(closestPlayer.transform.position);
        } else if (ShouldITurnAround()) {
            if (currentDestination == 1) {
                NMAgent.destination = destination2;
                currentDestination = 2;
            } else {
                NMAgent.destination = destination1;
                currentDestination = 1;
            }
        }
    }

    private GameObject ShouldIFollowPlayer() {
        float[] playerDistances = new float[numberOfPlayers];
        for (int i = 0; i < numberOfPlayers; i++) {
            playerDistances[i] = Vector3.Distance(players[i].transform.position, transform.position);
        }
        float smallestDistance = playerDistances.Min();=
        if (smallestDistance <= distanceToStartFollowingPlayer) {
            int smallestIndex = Array.IndexOf(playerDistances, smallestDistance);
            return players[smallestIndex];
        } else {
            return null;
        }
    }

    private bool ShouldITurnAround() {
        if (currentDestination == 1) {
            if (Vector3.Distance(transform.position, destination1) <= distanceThreshholdToTurnAround) {
                return true;
            }
        } else if (currentDestination == 2) {
            if (Vector3.Distance(transform.position, destination2) <= distanceThreshholdToTurnAround) {
                return true;
            }
        } else { //Needs to stop following player
            return true;
        }
        return false;
    }

    private bool FrontHit () {
        RaycastHit RCHit;
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, direction, out RCHit, distanceFromWall)) {

        }
        return Physics.Raycast(transform.position, direction, out RCHit, distanceFromWall);
    }

}