using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapMovement : MonoBehaviour {

    private GameObject player;
    public Vector3 moveX;
    public Vector3 moveY;
    GameManager gm;
    public float countToMove;
    private float countToMoveStart;

    void Start() {
        gm = FindObjectOfType<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        countToMoveStart = countToMove;
    }

  

    void Update() {
        countToMove -= Time.deltaTime;
        bool moved = false;

            if (Input.GetKey("d") && countToMove <= 0) {
            moved = true;
            countToMove = countToMoveStart;
            player.transform.position += moveX;

            }
            if (Input.GetKey("a") && countToMove <= 0) {
                moved = true;
            countToMove = countToMoveStart;
            player.transform.position -= moveX;
            }
            if (Input.GetKey("w") && countToMove <= 0) {
                moved = true;
            countToMove = countToMoveStart;
            player.transform.position += moveY;
            }
            if (Input.GetKey("s") && countToMove <= 0) {
                moved = true;
            countToMove = countToMoveStart;
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
