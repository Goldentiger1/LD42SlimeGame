using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerBehavor : MonoBehaviour {

    public string pickupAudio;
    public string dmgAudio;


    GameManager gm;
    GameObject[] food;
    float foodsEaten;
    public TextMeshProUGUI FoodEaten;
    public TextMeshProUGUI statusText;
    public GameObject retryButton;



    void Start () {
        gm = GameObject.FindObjectOfType<GameManager>();
        food = GameObject.FindGameObjectsWithTag("food");
        retryButton.SetActive(false);
	}

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "food") {
            //AudioManager.PlaySound("pickup");
            Fabric.EventManager.Instance.PostEvent(pickupAudio);
            print("osuma ruokaan, kasvoit hieman");
            gm.megaSlime += 5;
            foodsEaten++;
            FoodEaten.text = "Food eaten: " + foodsEaten;
            Destroy(collision.gameObject);
            


        }

        if (collision.tag == "enemy") {
            //AudioManager.PlaySound("damage");
            Fabric.EventManager.Instance.PostEvent(dmgAudio);
            gm.megaSlime -= 5;
            //vähennä myös scorea (ja kerrointa?)
            if (gm.megaSlime < 0) {
                gameObject.SetActive(false);
                statusText.text = "Gameover, better luck next time!";
                retryButton.SetActive(true);
                
            }
        }

        if (collision.tag == "goal") {
            print("pääsit maaliin");
        }
    }

    public void Retry(string level) {
        SceneManager.LoadScene(level);
    }

    void Update () {

	}
}
