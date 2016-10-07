using UnityEngine;
using System.Collections;

public class DeathandRespawn : MonoBehaviour {

    public GameObject Spawner;

    private PlayerController MyPC;
    private int playerNumber;

    void Start() {
        MyPC = GetComponent<PlayerController>();
        playerNumber = MyPC.playerNumber;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Knight"))
        {
            RespawnMe();
        }
    }

    public void RespawnMe() {
        this.transform.position = Spawner.transform.position;
    }

}
