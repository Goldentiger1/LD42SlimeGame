using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBehavor : MonoBehaviour {

    GameManager gm;
    GameObject[] food;
    float foodsEaten;
    public TextMeshProUGUI FoodEaten;
    public TextMeshProUGUI statusText;


    void Start () {
        gm = GameObject.FindObjectOfType<GameManager>();
        food = GameObject.FindGameObjectsWithTag("food");
	}

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "food") {
            print("osuma ruokaan, kasvoit hieman");
            gm.megaSlime += 5;
            foodsEaten++;
            FoodEaten.text = "Food eaten: " + foodsEaten;
            Destroy(collision.gameObject);

        }

        if (collision.tag == "enemy") {
            gm.megaSlime -= 5;
            //vähennä myös scorea (ja kerrointa?)
            if (gm.megaSlime < 0) {
                gameObject.SetActive(false);
                statusText.text = "Gameover, better luck next time!";
            }
        }
    }

    void Update () {
	
	}
}
