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
    public LayerMask wallLayer;
    Vector3 move;


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
            move = moveX;
        }

        if (Input.GetKey("a") && countToMove <= 0) {
            moved = true;
            move = -moveX;
        }

        if (Input.GetKey("w") && countToMove <= 0) {
            moved = true;
            countToMove = countToMoveStart;
            move = moveY;
        }

        if (Input.GetKey("s") && countToMove <= 0) {
            moved = true;
            move = -moveY;
        }

        if (moved) {
            gm.LastDir(move);
            countToMove = countToMoveStart;
            gm.lastPos = player.transform.position;
            if (!Physics2D.OverlapPoint(player.transform.position + move, wallLayer)) {
                player.transform.position += move;
            }
            // kill
            foreach (Transform child in gm.slimesFolder) {

                //Destroy(child.gameObject);
                gm.Despawn(child.gameObject);
            }
            gm.FloodFill(gm.megaSlime, gm.CenterCalc());
        }
    }
}
