using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    GameManager gM;
    TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        gM = GameObject.Find("GameManager").GetComponent<GameManager>();
        gM.ScoreIncreasedEvent.AddListener(IncreasedScoreListener);
        scoreText = this.GetComponent<TextMeshProUGUI>();
    }

    void IncreasedScoreListener()
    {
        scoreText.text = string.Format("Score: {0}", gM.score);
    }

}
