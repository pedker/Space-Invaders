using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;

    private Collider2D m_collider;
    private Vector2 projectilePlacement;
    public GameObject enemyProjectile;
    public GameObject dummyGameObject;
    private GameManager gameManager;
    private IntUnityEvent enemyKilled;

    public float speed;
    public int kill_score;
    private int in_grid_x;
    private int in_grid_y;

    private void Awake()
    {
        m_rigidbody = this.GetComponent<Rigidbody2D>();
        m_collider = this.GetComponent<Collider2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyKilled = gameManager.EnemyWasKilled;
    }


    // Update is called once per frame
    void Update()
    {
        Move(); 
    }

    public void Init(int x, int y)
    {
        in_grid_x = x;
        in_grid_y = y;
    }

    private void Move()
    {
       // Vector2 currentVelocity = m_rigidbody.velocity;
        m_rigidbody.velocity = new Vector2(speed, 0f);
    }

    public void OnWallBumpEventListener()
    {
        speed = speed * -1.5f;
        this.transform.position = new Vector3(this.transform.position.x,
                                              this.transform.position.y - 1, 0);

    }

    public void Shoot()
    {
        projectilePlacement = this.transform.position + new Vector3(0, -0.5f, 0);
        GameObject newBullet = Instantiate(enemyProjectile, projectilePlacement, Quaternion.identity);
        Physics2D.IgnoreCollision(m_collider, newBullet.GetComponent<Collider2D>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            enemyKilled.Invoke(kill_score);
            Vector3 currentPosition = this.transform.position;
            Destroy(this.gameObject);
            GameObject newDummyGameObject = Instantiate(dummyGameObject, currentPosition, Quaternion.identity);
            gameManager.alienGrid[in_grid_x][in_grid_y] = newDummyGameObject;
        }
    }
}
