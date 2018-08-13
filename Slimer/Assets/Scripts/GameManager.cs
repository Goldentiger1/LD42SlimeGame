using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Vector2 size;
    public GameObject player;
    public GameObject playerSlime;
    public GameObject someFood;
    public Transform slimesFolder;
    public Vector3 lastPos;
    public int megaSlime;

    private TilemapMovement tm;
    private List<Vector3> fourNeighbors = new List<Vector3>() { Vector3.up, Vector3.right, Vector3.down, Vector3.left };

    public Stack<GameObject> PooledSlimes { get { return pooledSlimes; } }
    public Stack<GameObject> PooledFood { get { return pooledFood; } }

    private Stack<GameObject> pooledSlimes = new Stack<GameObject>();
    private Stack<GameObject> pooledFood = new Stack<GameObject>();

    public void LastDir(Vector3 c) {
        if (fourNeighbors.Contains(c)) {
            fourNeighbors.Remove(c);
            fourNeighbors.Add(c);
        }
        else {
            Debug.LogError("!!");
        }
    }

    public GameObject Spawn(GameObject prafab, Stack<GameObject>poolStack, Vector3 pos = new Vector3()) {

        if (poolStack.Count > 0) {
            GameObject go = poolStack.Pop();
            go.transform.position = pos;
            return go;
        }

        return Instantiate(prafab, pos, Quaternion.identity);
    }

    public void Despawn(Stack<GameObject> poolStack, GameObject go) {
        go.SetActive(false);
        poolStack.Push(go);
    }

    public Vector3 CenterCalc() {
        if (pooledSlimes.Count <= 0) {
            return player.transform.position;
        }

        Vector3 foo = Vector3.zero;

        //laskee slimejen keskiarvopisteen
        foreach (var item in pooledSlimes) {
            foo += item.transform.position;
        }
        foo /= pooledSlimes.Count;
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
            var go = Spawn(playerSlime, pooledSlimes, pos);
            go.SetActive(true);
            go.transform.parent = slimesFolder;
            n--;
            foreach (var delta in fourNeighbors) 
                fringe.Enqueue(pos + delta);
        }
    }

	void Start () {
        tm = FindObjectOfType<TilemapMovement>();
        lastPos = player.transform.position;
        FloodFill(megaSlime, CenterCalc());
        RandomizeFood(size, 20);
    }

    private void RandomizeFood(Vector2 size, int amount) {
        for (int i = 0; i < amount; i++) {
            var p = transform.position;
            var rndPos = player.transform.position;
            while (Vector3.Distance(rndPos, player.transform.position) < 7) {
                float rX = p.x + (Random.value - 0.5f) * size.x;
                float rY = p.y + (Random.value - 0.5f) * size.y;
                rndPos = new Vector2(rX, rY);
            }
            //float rX = p.x + Random.Range(-1, 1) * 0.5f * size.x;
            //float rX = Random.Range(p.x - (0.5f * size.x), p.x + (0.5f * size.x));
            GameObject go = Spawn(someFood, PooledFood, (Vector2)Vector2Int.RoundToInt(rndPos));
            go.SetActive(true);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);        
    }
}
