using UnityEngine;
using System.Collections;

public class WallDestroyer : MonoBehaviour {


    void OnTriggerEnter(Collider other) {
        if (other.tag == "Wall") {
            print("BOOM");
            Destroy(other.gameObject);
        }
    }
}
