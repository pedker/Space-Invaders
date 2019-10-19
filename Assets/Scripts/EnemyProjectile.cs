using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] protected float speed;
    private Rigidbody2D bulletBody;

    // Start is called before the first frame update
    void Start()
    {
        bulletBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bulletBody.velocity = new Vector2(bulletBody.velocity.x, (speed * -1) );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( ! collision.collider.CompareTag("PlayerProjectile") && ! collision.collider.CompareTag("Enemy") )
        {
            Destroy(this.gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
