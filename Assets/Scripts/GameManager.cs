using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GameManager : MonoBehaviour
{

    public int score = 0;
    private int kills = 0;
    [System.NonSerialized] public int currentLevel = 1;
    public GameOver gameOverOverlay;
    public GameOver playerWonCanvas;
    public UnityEvent WinnerWinnerChickenDinner = new UnityEvent();
    public UnityEvent WallBumpingEvent = new UnityEvent();
    public UnityEvent BottomTouchedEvent = new UnityEvent();
    public UnityEvent ScoreIncreasedEvent = new UnityEvent();
    public IntUnityEvent EnemyWasKilled = new IntUnityEvent();
    public List<List<GameObject>> alienGrid = new List<List<GameObject>>();
    public GameObject Ufo;
    public float decreaseShootTimeScalar;

    public float minTimeToShoot;
    public float maxTimeToShoot;
    [SerializeField] protected float chanceOfUFO;
    private float timeElapsed;
    private float UFOtimer;
    private float gameTimer;
    private float randomTime;
    

    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0.0f;
        UFOtimer = 0.0f;
        gameTimer = 0.0f;
        randomTime = Random.Range(0.0f, maxTimeToShoot);
        EnemyWasKilled.AddListener(EnemyKilledListener);
        BottomTouchedEvent.AddListener(BottomTouched);
        WinnerWinnerChickenDinner.AddListener(PlayerWon);
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        UFOtimer += Time.deltaTime;
        gameTimer += Time.deltaTime;
        if (timeElapsed > randomTime)
        {
            FindShooter();
            randomTime = Random.Range(minTimeToShoot, maxTimeToShoot);
            timeElapsed = 0.0f;
        }

        if (UFOtimer > 0.5f)
        {
            UFOtimer = 0.0f;
            float randomUFO = Random.Range(0.0f, 1.0f);
            if (randomUFO <= chanceOfUFO && !UFO.exists)
            {
                Instantiate(Ufo, new Vector3(0, 6, 0), Quaternion.identity);
            }
        }
        if (gameTimer > 10.0f)
        {
            minTimeToShoot *= decreaseShootTimeScalar;
            maxTimeToShoot *= decreaseShootTimeScalar;
            gameTimer = 0.0f;
        }
    }

    private void BottomTouched()
    {
        PlayerDead();
    }

    public void PlayerDead()
    {
        Time.timeScale = 0;
        gameOverOverlay.DisplayGameOver();
    }

    void FindShooter()
    {
        int[] temp = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        List<int> columnsToCheck = new List<int>(temp);

        for (int x = 0; x < 11; x++)
        {
            int index = Random.Range(0, 11-x);

            for (int y = 0; y < 5; y++)
            {
                if (alienGrid[columnsToCheck[index]][y].CompareTag("Enemy"))
                {
                    Enemy foundEnemy = alienGrid[columnsToCheck[index]][y].GetComponent<Enemy>();
                    foundEnemy.Shoot();
                    return;
                }
            }

            columnsToCheck.RemoveAt(index);
        }
    }

    private void CheckWin()
    {
        if (kills%55 == 0)
        {
            WinnerWinnerChickenDinner.Invoke();
        }
    }

    private void EnemyKilledListener(int _score)
    {
        kills++;
        IncreaseScore(_score);
        CheckWin();
    }

    public void IncreaseScore(int score_to_add)
    {
        score += score_to_add;
        ScoreIncreasedEvent.Invoke();
    }

    public void PlayerWon()
    {
        Time.timeScale = 0;
        playerWonCanvas.DisplayWin();
    }
}
