using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pause : MonoBehaviour
{
    public GameObject pausePanel;
    bool isPaused = false;
    public GameObject gameOverOverlay;
    



    void Update()
    {
        if ((!Input.GetKeyDown(KeyCode.Space) && Input.GetKeyDown(KeyCode.Escape)) && 
             !gameOverOverlay.GetComponent<GameOver>().ReturnDead() &&
             !gameOverOverlay.GetComponent<GameOver>().ReturnWon())
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
    }
}
