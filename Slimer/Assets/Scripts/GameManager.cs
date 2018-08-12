using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject player;
    public GameObject playerSlime;
    List<Vector3> fourNeighbors = new List<Vector3>() { Vector3.up, Vector3.right, Vector3.down, Vector3.left };
    public Transform slimesFolder;
    public int megaSlime;

    public void FloodFill(int n) {
        var fringe = new Queue<Vector3>();
        foreach (var delta in fourNeighbors) {
            fringe.Enqueue(player.transform.position + delta);

        }

        while(fringe.Count > 0 && n > 0) {
            var pos = fringe.Dequeue();
            // tsekataan onko tyhjää
            var collider = Physics2D.OverlapPoint(pos); //TODO: lisää layermask kun koodi muuten ok
            bool isFree = !collider;
            if (!isFree) continue; //hyppää whilen alkuun toisin kuin return
            //instantioidaan slime
            var go = Instantiate(playerSlime, pos, Quaternion.identity);
            go.transform.parent = slimesFolder;
            n--;
            foreach (var delta in fourNeighbors) 
                fringe.Enqueue(pos + delta);

        }
    }

	void Start () {
        //foreach (var delta in fourNeighbors) {
        //    GameObject go = Instantiate(playerSlime, player.transform.position + delta, Quaternion.identity);
        //    go.transform.parent = slimesFolder;
        //}
        FloodFill(megaSlime);
	}
	


	void Update () {
        //liikuttaa dummyslimeja playerin mukana kun se liikkuu
        //käy läpi naapurit ja pistää uuteen listaan

        }
	
}
