using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapMovement : MonoBehaviour {

    private GameObject player;
    public Vector3 moveX;
    public Vector3 moveY;


    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {

        if (Input.GetKeyDown("d")) {
            player.transform.position += moveX;
        }
        if (Input.GetKeyDown("a")) {
            player.transform.position -= moveX;
        }
        if (Input.GetKeyDown("w")) {
            player.transform.position += moveY;
        }
        if (Input.GetKeyDown("s")) {
            player.transform.position -= moveY;
        }
    }
}
