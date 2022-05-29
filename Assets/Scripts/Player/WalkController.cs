using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WalkController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float leftBoundary = -8f;
    [SerializeField] float rightBoundary = 5f;
    [SerializeField] AudioClip jumpClip;

    Vector2 moveVal;
    bool isOnGround = true;

    Rigidbody rb;
    AudioSource audioSource;
    Health playerHealth;
    PlayerCollision playerCollision;
    GameManager gameManager;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        playerHealth = GetComponent<Health>();
        playerCollision = GetComponent<PlayerCollision>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable() {
        rb.velocity = Vector3.zero;
    }

    void Update()
    {
        // Move Player
        transform.Translate(new Vector3(moveVal.x, 0f, 0f) * moveSpeed * Time.deltaTime);
        CheckBoundary();
    }

    private void CheckBoundary() {
        if (transform.position.x <= leftBoundary) transform.position = new Vector3(leftBoundary, transform.position.y, 0f);
        if (transform.position.x >= rightBoundary) transform.position = new Vector3(rightBoundary, transform.position.y, 0f);
    }

    private void OnMove(InputValue value) {
        if (!gameManager.isPlaying) return;
        moveVal = value.Get<Vector2>();
    }

    private void OnJump(InputValue value) {
        if (!gameManager.isPlaying) return;
        if (!this.enabled) return;
        
        if (playerCollision.anim == null) {
            Debug.LogError("There's something wrong in animator children");
            return;
        }
        
        if (isOnGround) {
            audioSource.clip = jumpClip;
            audioSource.Play();
            rb.AddForce(Vector3.up * jumpForce * rb.mass);
            playerCollision.anim.SetTrigger("isJump");
            playerCollision.anim.SetBool("airborne", true);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (!this.enabled) return;
        if (other.collider.CompareTag("Ground")) {
            isOnGround = true;
            playerCollision.anim.SetBool("airborne", false);
        }
    }

    private void OnCollisionExit(Collision other) {
        if (!this.enabled) return;
        if (other.collider.CompareTag("Ground")) {
            isOnGround = false;
        }
    }
}
