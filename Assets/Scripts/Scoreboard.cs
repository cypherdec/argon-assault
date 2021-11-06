using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{

    TMP_Text scoreText;
    int score = 0;

    void Start() {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "0";
    }

    public void IncreaseScore(int points){
        score += points;
        scoreText.text = score.ToString();
    }
    
}
