using UnityEngine;
using System.Collections;

public class DeathandRespawn : MonoBehaviour {

    public GameObject Spawner;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Knight"))
        {
            this.transform.position = Spawner.transform.position;
        }
    }

}
