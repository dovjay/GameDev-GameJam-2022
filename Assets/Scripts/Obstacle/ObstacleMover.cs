using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    [SerializeField] float positionOffset = -20f;
    public float moveSpeed = 8f;
    public float damage = 1f;
    public int playableIndex; // match the playable object index in playerCollision list

    GameObject player;
    GameManager gameManager;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (!gameManager.isPlaying || gameManager.isGameOver) return;
        CheckBoundary();
        Move();
    }

    private void Move() {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
    }

    private void CheckBoundary() {
        if (transform.position.x < positionOffset) Destroy(gameObject);
    }
}
