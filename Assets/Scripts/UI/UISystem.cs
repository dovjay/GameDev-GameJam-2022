using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UISystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] GameObject titleGroup;
    [SerializeField] GameObject gameplayGroup;
    [SerializeField] GameObject gameOverGroup;
    [SerializeField] Slider ghostTimer;
    [SerializeField] GameObject helpWindow;

    GameObject player;
    PlayerCollision playerCollision;
    GameManager gameManager;
    ScoreSystem scoreSystem;
    ObstacleSpawner obstacleSpawner;
    EnvironmentSpawner[] environmentSpawners;
    AudioManager audioManager;
    
    private void Awake() {
        player = GameObject.FindWithTag("Player");
        gameManager = FindObjectOfType<GameManager>();
        scoreSystem = gameManager.transform.GetComponent<ScoreSystem>();
        playerCollision = player.GetComponent<PlayerCollision>();
        obstacleSpawner = FindObjectOfType<ObstacleSpawner>();
        environmentSpawners = FindObjectsOfType<EnvironmentSpawner>();
        audioManager = gameManager.transform.GetComponent<AudioManager>();

        ghostTimer.maxValue = playerCollision.ghostDuration;
        if (scoreSystem.GetHighScore() > 0) highScoreText.text = "High Score " + scoreSystem.GetHighScore();
    }

    void Update()
    {
        HandleGhostTimer();
        healthText.text = "Health: " + player.GetComponent<Health>().healthPoint;
        HandleScoreText();
        HandleGameOver();
    }

    private void OnClick(InputValue value) {
        if (value.isPressed && helpWindow.activeSelf) {
            helpWindow.SetActive(false);
        }
    }

    private void HandleGhostTimer() {
        if (!playerCollision.isGhost || player.GetComponent<Health>().isDead) {
            ghostTimer.value = ghostTimer.maxValue;
            ghostTimer.gameObject.SetActive(false);
            return;
        }

        ghostTimer.gameObject.SetActive(true);
        ghostTimer.value -= Time.deltaTime;
        if (ghostTimer.value <= 0f && !player.GetComponent<Health>().isDead) {
            player.GetComponent<Health>().TakeDamage(Mathf.Infinity);
            Destroy(player.transform.GetChild(0).transform.GetChild(0).gameObject);
        }
    }

    private void HandleScoreText() {
        scoreText.text = scoreSystem.GetScore().ToString();
    }

    private void HandleGameOver() {
        if (!gameManager.isGameOver) return;

        gameOverText.text = "Game Over.\nYour score " + scoreSystem.GetScore();
        if (scoreSystem.CompareHighScore()) gameOverText.text += "\nNew High Score!";

        gameplayGroup.gameObject.SetActive(false);
        gameOverGroup.gameObject.SetActive(true);
    }

    public void OpenHelp() {
        helpWindow.SetActive(true);
    }

    public void StartGame() {
        playerCollision.anim.SetTrigger("isPlaying");
        gameManager.isPlaying = true;
        titleGroup.gameObject.SetActive(false);
        gameplayGroup.gameObject.SetActive(true);
        StartCoroutine(obstacleSpawner.SpawnObject());
        foreach (EnvironmentSpawner spawner in environmentSpawners)
        {
            StartCoroutine(spawner.SpawnObject());
        }
    }

    public void RestartGame() {
        gameManager.RestartGameState();
        SceneManager.LoadScene(0);
        audioManager.TurnPitch("up", 1f);
        if (scoreSystem.GetHighScore() > 0) highScoreText.text = "High Score " + scoreSystem.GetHighScore();
    }

    public void ExitGame() {
        Application.Quit();
    }
}
