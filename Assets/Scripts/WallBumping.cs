using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WallBumping : MonoBehaviour
{
    private bool wasBumped = false;
    [System.NonSerialized] public UnityEvent OnWallBumpEvent;

    // Start is called before the first frame update
    void Start()
    {
        OnWallBumpEvent = this.transform.parent.gameObject.GetComponent<GameManager>().WallBumpingEvent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!wasBumped && collision.collider.CompareTag("Enemy"))
        {
            OnWallBumpEvent.Invoke();
            wasBumped = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        wasBumped = false;
    }
}
