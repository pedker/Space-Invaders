using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bottom : MonoBehaviour
{
    private GameManager gameManager;
    private UnityEvent BottomTouched;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        BottomTouched = gameManager.BottomTouchedEvent;
        Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), 
                                  GameObject.Find("Player").GetComponent<Collider2D>());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            BottomTouched.Invoke();
            Debug.Log("You Fixed it");
        }
    }
}
