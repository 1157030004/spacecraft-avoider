using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] float scoreMultiplier;

    bool shouldCount = true;
    float score;

    void Update()
    {
        if(!shouldCount) return;

        score += Time.deltaTime * scoreMultiplier;
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    public void StartTimer()
    {
        shouldCount = true;
    }

    public int EndTimer()
    {
        shouldCount = false;
        scoreText.text = string.Empty;

        return Mathf.FloorToInt(score);
    }
}
