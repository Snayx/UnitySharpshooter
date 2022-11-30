using InfimaGames.LowPolyShooterPack.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentManagerScript : MonoBehaviour
{
    public static PersistentManagerScript Instance { get; private set; }

    public int score;
    public float timeRemaining = 90.0F;
    public bool timerIsRunning = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            timeRemaining = 90;
            score = 0;
            timerIsRunning = false;
            SceneManager.LoadScene("Main_Menu");
        }
        if (Input.GetKeyDown("l")) {
            timeRemaining = 90;
            score = 0;
            timerIsRunning = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown("n"))
        {
            timerIsRunning = true;
        }
        if(timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;

            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }

        if (timeRemaining <= 0 & Input.GetKeyDown("n")) {
            timeRemaining = 90;
            score = 0;
            timerIsRunning = true;
        
        }
    }
}
