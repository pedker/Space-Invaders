using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesText : MonoBehaviour
{
    Player playerComponent;
    TextMeshProUGUI livesText;
    void Start()
    {
        livesText = this.GetComponent<TextMeshProUGUI>();
        playerComponent = GameObject.Find("Player").GetComponent<Player>();
        playerComponent.takeDamage += takeDamageListener;
    }

    void Update()
    {
    }

    void takeDamageListener(int lives)
    {
        livesText.text = string.Format("Lives: {0}", lives);
    }
}
