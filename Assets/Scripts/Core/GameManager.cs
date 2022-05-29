using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGameOver = false;
    public bool isPlaying = false;

    ScoreSystem scoreSystem;
    AudioManager audioManager;

    private void Awake() {
        if (gameObject.scene.buildIndex != -1 && GameObject.FindGameObjectsWithTag("GameManager").Length == 1) {
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
            return;
        }

        scoreSystem = GetComponent<ScoreSystem>();
        audioManager = GetComponent<AudioManager>();
    }

    private void Start() {
        audioManager.PlayCharacterClip();
    }

    public void RestartGameState() {
        isGameOver = false;
        isPlaying = false;
        scoreSystem.ResetScore();
    }
}
