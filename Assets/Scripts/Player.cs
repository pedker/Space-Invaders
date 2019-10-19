using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IntUnityEvent : UnityEvent<int> { }

public class Player : MonoBehaviour
{
    [SerializeField] public int lives = 3;
    [SerializeField] public bool canFire = true;
    [SerializeField] protected float speed = 5;
    private Rigidbody2D m_rigidbody;
    private Collider2D m_collider;
    private Transform m_transform;
    private Vector2 projectilePlacement;

    public delegate void IntDelegate(int lives);
    public event IntDelegate takeDamage = delegate { };

    public GameObject playerProjectile;
    GameManager m_gameManager;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = this.GetComponent<Rigidbody2D>();
        m_collider = this.GetComponent<Collider2D>();
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_transform = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        Move();

        if (canFire && Time.timeScale != 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space))
                Shoot();
        }
    }

    void Move()
    {
        float movementModifier = Input.GetAxisRaw("Horizontal");
        Vector2 currentVelocity = m_rigidbody.velocity;
        m_rigidbody.velocity = new Vector2(movementModifier * speed, currentVelocity.y);
    }

    void Shoot()
    {
        canFire = false;
        projectilePlacement = this.transform.position + new Vector3(0, 0.5f, 0);
        GameObject newBullet = Instantiate(playerProjectile, projectilePlacement, Quaternion.identity);
        Physics2D.IgnoreCollision(m_collider, newBullet.GetComponent<Collider2D>());
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("EnemyProjectile"))
        {
            Destroy(collider.gameObject);
            lives--;
            takeDamage(lives);
            if (lives == 0)
            {
                Death();
            }
        }
    }

    public void oneUpEventListener(int life)
    {
        takeDamage(life);
    }

    void Death()
    {
        m_gameManager.PlayerDead();
    }
}
