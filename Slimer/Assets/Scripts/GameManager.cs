using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public string gameAudio;
    public string stopAudio;
    public Vector3[] testi;
    public GameObject player;
    public GameObject playerSlime;
    List<Vector3> fourNeighbors = new List<Vector3>() { Vector3.up, Vector3.right, Vector3.down, Vector3.left };
    //List<Transform> slimes = new List<Transform>();
    public Transform slimesFolder;
    public int megaSlime;
    Stack<GameObject> pooledSlimes = new Stack<GameObject>();
    public Vector3 lastPos;

    public void LastDir(Vector3 c) {
        if (fourNeighbors.Contains(c)) {
            fourNeighbors.Remove(c);
            fourNeighbors.Add(c);
            testi = fourNeighbors.ToArray();
        }
        else {
            Debug.LogError("!!");
        }
    }

    public GameObject Spawn(Vector3 pos = new Vector3()) {

        if (pooledSlimes.Count > 0) {
            GameObject go = pooledSlimes.Pop();
            go.transform.position = pos;
            return go;
        }

        return Instantiate(playerSlime, pos, Quaternion.identity);
    }

    public void Despawn(GameObject go) {
        go.SetActive(false);
        pooledSlimes.Push(go);
    }

    public Vector3 CenterCalc() {
        if (/*slimes*/pooledSlimes.Count <= 0) {
            return player.transform.position;
        }

        Vector3 foo = Vector3.zero;

        //laskee slimejen keskiarvopisteen
        foreach (var item in /*slimes*/pooledSlimes) {
            foo += item.transform.position;
        }
        foo /= /*slimes*/pooledSlimes.Count;
        return Vector3Int.RoundToInt((foo + player.transform.position + lastPos)* 1/3f);
    }

    public void FloodFill(int n, Vector3 center) {
        var fringe = new Queue<Vector3>(); 
        foreach (var delta in fourNeighbors) {
            fringe.Enqueue(center + delta);

        }

        while(fringe.Count > 0 && n > 0) {
            var pos = fringe.Dequeue();
            // tsekataan onko tyhjää
            var collider = Physics2D.OverlapPoint(pos); //TODO: lisää layermask kun koodi muuten ok
            bool isFree = !collider;
           if (!isFree) continue; //hyppää whilen alkuun toisin kuin return
            //instantioidaan slime
            var go = Spawn(pos)/*Instantiate(playerSlime, pos, Quaternion.identity)*/;
            go.SetActive(true);
            // slimes.Add(go.transform);
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
        lastPos = player.transform.position;
        FloodFill(megaSlime, CenterCalc());
        Fabric.EventManager.Instance.PostEvent(gameAudio);

    }
	


	void Update () {
        //liikuttaa dummyslimeja playerin mukana kun se liikkuu
        //käy läpi naapurit ja pistää uuteen listaan
        
        }
	
}
