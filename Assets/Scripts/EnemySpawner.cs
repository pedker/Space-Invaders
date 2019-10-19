using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;
    GameObject parent_object;
    GameManager gameManagerComponent;
    List<List<GameObject>> alien_grid;
    private Dictionary<int, GameObject> prefabDict = new Dictionary<int, GameObject>();

    private void Awake()
    {
        parent_object = this.transform.parent.gameObject;
        gameManagerComponent = parent_object.GetComponent<GameManager>();
        alien_grid = gameManagerComponent.alienGrid;
        prefabDict.Add(0, enemyPrefab1);
        prefabDict.Add(1, enemyPrefab1);
        prefabDict.Add(2, enemyPrefab2);
        prefabDict.Add(3, enemyPrefab2);
        prefabDict.Add(4, enemyPrefab3);

    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnAliens();
    }

    public void SpawnAliens()
    {
        alien_grid.Clear();
        for (int x = 0; x < 11; x++)
        {
            alien_grid.Add(new List<GameObject>());
            for (int y = 0; y < 5; y++)
            {
                GameObject enemyPrefab = prefabDict[y];
                Vector3 spawnLocation = new Vector3(((1.5f*x)-7), ((1.5f*y)-1), 0);
                if (gameManagerComponent.currentLevel >= 3 && 
                    gameManagerComponent.currentLevel < 5)
                {
                    spawnLocation.y -= 1;
                }
                else if (gameManagerComponent.currentLevel >= 5)
                {
                    spawnLocation.y -= 2;
                }
                GameObject newEnemyObject = Instantiate(enemyPrefab, this.transform);
                newEnemyObject.transform.localPosition = spawnLocation;
                Enemy newEnemyComponent = newEnemyObject.GetComponent<Enemy>();
                newEnemyComponent.Init(x, y);
                gameManagerComponent.WallBumpingEvent.AddListener(newEnemyComponent.OnWallBumpEventListener);
                alien_grid[x].Add(newEnemyObject);
            }
        }
    }
}
