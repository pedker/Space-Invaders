using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    private GameManager gameManager;

    static public bool exists = false;

    public float speed;
    private int direction;
    private int[] killScoreArray = { 100, 150, 200, 250, 300 };
    private int killScoreIndex;

    private void Awake()
    {
        m_rigidbody = this.GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        killScoreIndex = Random.Range(0, 5);
        direction = -1;
        exists = true;
        int spawnSide = Random.Range(0, 2);
        if (spawnSide == 0)
        {
            spawnSide -= 1;
            direction *= -1;
        }

        this.transform.position = new Vector2(11.5f * spawnSide, this.transform.position.y);
    }


    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        // Vector2 currentVelocity = m_rigidbody.velocity;
        m_rigidbody.velocity = new Vector2( (speed * direction) , 0f);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
        exists = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            gameManager.IncreaseScore(killScoreArray[killScoreIndex]);
            Destroy(this.gameObject);
            exists = false;
        }
    }
}
