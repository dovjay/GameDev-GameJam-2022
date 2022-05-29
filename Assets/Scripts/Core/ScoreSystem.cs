using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    float score = 0f;
    float highScore = 0f;
    float scoreMultiplier = 1f;
    bool scoreCompared = false;

    GameManager gameManager;

    private void Awake() {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (gameManager.isGameOver) return;
        if (!gameManager.isPlaying) return;
        UpdateScore();
    }

    public void UpdateScore() {
        score += Time.deltaTime * scoreMultiplier;
        scoreCompared = false;
    }

    public float GetScore() {
        return Mathf.Round(score * 100f) / 100f;
    }

    public float GetHighScore() {
        return highScore;
    }

    public void ResetScore() {
        score = 0;
    }

    public bool CompareHighScore() {
        if (scoreCompared) return true;
        if (score > highScore) {
            highScore = Mathf.Round(score * 100f) / 100f;
            scoreCompared = true;
            return true;
        }

        return false;
    }
}
