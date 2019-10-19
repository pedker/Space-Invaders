using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{

    [SerializeField] protected float speed;
    private Rigidbody2D bulletBody;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        bulletBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        bulletBody.velocity = new Vector2(bulletBody.velocity.x, speed);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // INVOKE EVENT FOR CAN_SHOOT
        if ( ! collider.CompareTag("EnemyProjectile"))
        {
            player.canFire = true;
            Destroy(this.gameObject);
        }
    }
}
