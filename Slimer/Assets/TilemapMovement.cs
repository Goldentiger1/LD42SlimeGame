using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapMovement : MonoBehaviour {

    private GameObject player;
    public Vector3 moveX;
    public Vector3 moveY;
    GameManager gm;

    void Start() {
        gm = FindObjectOfType<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

  

    void Update() {
        bool moved = false;
        if (Input.GetKeyDown("d")) {
            moved = true;
            player.transform.position += moveX;
        }
        if (Input.GetKeyDown("a")) {
            moved = true;
            player.transform.position -= moveX;
        }
        if (Input.GetKeyDown("w")) {
            moved = true;
            player.transform.position += moveY;
        }
        if (Input.GetKeyDown("s")) {
            moved = true;
            player.transform.position -= moveY;
        }
        if (moved) {
            // kill
            foreach (Transform child in gm.slimesFolder) {
                Destroy(child.gameObject);
            }
                //for (int i=0; i<)
            gm.FloodFill(gm.megaSlime);
        }

    }
}
