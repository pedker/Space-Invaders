using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverOverlay;
    public IntUnityEvent oneUp = new IntUnityEvent();
    private GameManager gameManager;
    private EnemySpawner enemySpawner;
    private Player player;
    [SerializeField] private bool isDead;
    [SerializeField] private bool won;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        player = FindObjectOfType<Player>().GetComponent<Player>();
        oneUp.AddListener(player.oneUpEventListener);
        isDead = false;
        won = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayGameOver()
    {
        isDead = true;
        gameOverOverlay.SetActive(true);
    }

    public void DisplayWin()
    {
        won = true;
        gameOverOverlay.SetActive(true);
    }

    public void RestartGame()
    {
        isDead = false;
        won = false;
        Time.timeScale = 1;
    }

    public bool ReturnDead()
    {
        return isDead;
    }

    public bool ReturnWon()
    {
        return won;
    }

    public void NextLevel()
    {
        gameManager.currentLevel++;
        gameManager.maxTimeToShoot = 4.0f;
        gameManager.minTimeToShoot = 0.5f;

        //This is highkey jank
        GameObject[] epList;
        GameObject[] doList;
        epList = GameObject.FindGameObjectsWithTag("EnemyProjectile");
        doList = GameObject.FindGameObjectsWithTag("Dummy");
        for (int x = 0; x < epList.Length; x++)
        {
            Destroy(epList[x]);
        }
        for (int x = 0; x < doList.Length; x++)
        {
            Destroy(doList[x]);
        }

        player.lives++;
        oneUp.Invoke(player.lives);
        enemySpawner.SpawnAliens();
        gameOverOverlay.SetActive(false);
    }
}
