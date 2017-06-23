using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;


    Animator anim;
	//bool gameRunning = true;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");
        }

		if (Input.GetKeyDown (KeyCode.R))
		{
			playerHealth.RestartLevel();
		}
    }
}
