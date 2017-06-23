using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
    public GameObject explosion;
    public GameObject playerExplosion;
    public int score;
    private GameController gameController;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Boundary") {
            return;
        }
        Instantiate(explosion, transform.position, transform.rotation);
        if (other.tag == "Player") {
            Instantiate(playerExplosion, other.transform.position, transform.rotation);
            gameController.GameOver();
        }
        gameController.addScore(score);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null) {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameControllerObject == null) {
            Debug.Log("Cannot find 'GameController' script");
        }
	{
		 
	}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
