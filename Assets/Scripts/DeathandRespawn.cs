using UnityEngine;
using System.Collections;

public class DeathandRespawn : MonoBehaviour {

    public GameObject Spawner;
    private GameObject clone;

    void OnTriggerEnter(Collider other)
    {
        //if we have a death animation, we can put a delay here.
        Debug.Log("collision");
        if (other.CompareTag("Knight"))//or whoever can kill this.gameobject
        {
            Debug.Log("Knight attacked");
            clone = this.gameObject;
            Instantiate(clone, Spawner.transform.position, Spawner.transform.rotation);
            Destroy(this.gameObject);
        }
    }

}
