using UnityEngine;
using UnityEngine.InputSystem;

public class FlyController : MonoBehaviour {
    [SerializeField] float upperBoundary = 4f;
    [SerializeField] float leftBoundary = -8f;
    [SerializeField] float rightBoundary = 5f;
    [SerializeField] float lowerBoundary = .5f;
    [SerializeField] float flySpeed = 10f;
    
    Vector2 moveVal;
    Animator anim;
    Rigidbody rb;
    Health playerHealth;
    PlayerCollision playerCollision;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        playerHealth = GetComponent<Health>();
    }

    private void OnEnable() {
        rb.velocity = Vector3.zero;
    }

    private void FixedUpdate() {
        transform.Translate(new Vector3(moveVal.x, moveVal.y, 0f) * flySpeed * Time.deltaTime);
        CheckBoundary();
    }

    private void CheckBoundary() {
        if (transform.position.x <= leftBoundary) transform.position = new Vector3(leftBoundary, transform.position.y, 0f);
        if (transform.position.x >= rightBoundary) transform.position = new Vector3(rightBoundary, transform.position.y, 0f);
        if (transform.position.y >= upperBoundary) transform.position = new Vector3(transform.position.x, upperBoundary, 0f);
        if (transform.position.y <= lowerBoundary) transform.position = new Vector3(transform.position.x, lowerBoundary, 0f);
    }

    private void OnMove(InputValue value) {
        if (playerHealth.isDead) return;
        moveVal = value.Get<Vector2>();
    }
}