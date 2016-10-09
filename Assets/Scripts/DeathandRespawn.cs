using UnityEngine;
using System.Collections;

public class DeathandRespawn : MonoBehaviour {

    public GameObject Spawner;

    private PlayerController MyPC;
    private GlobalController GC;
    private int playerNumber;

    void Start() {
        MyPC = GetComponent<PlayerController>();
        GC = GameObject.Find("GameController").GetComponent<GlobalController>();
        playerNumber = MyPC.playerNumber;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Knight"))
        {
            if (MyPC.hasCrown) {
                GC.ResetTheCrown();
                MyPC.hasCrown = false;
            }
            RespawnMe();
        }
    }

    public void RespawnMe() {
        this.transform.position = Spawner.transform.position;
    }

}
