using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] ParticleSystem explosionFX;
    [SerializeField] AudioClip explosionClip;
    [SerializeField] AudioClip OneUpClip;
    public float healthPoint = 3f;
    public bool isDead = false;

    GameObject player;
    Animator anim;
    AudioSource audioSource;
    GameManager gameManager;
    PlayerCollision playerCollision;

    private void Awake() {
        player = GameObject.FindWithTag("Player");
        playerCollision = GetComponent<PlayerCollision>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(float damage) {
        explosionFX.Play();
        audioSource.clip = explosionClip;
        audioSource.Play();

        healthPoint = Mathf.Max(0f, healthPoint - damage);
        if (healthPoint == 0f) Death();
    }

    private void Death() {
        if (isDead) return;

        player.GetComponent<WalkController>().enabled = false;
        player.GetComponent<FlyController>().enabled = false;

        if (playerCollision.isGhost) {
            print("You are dead as a ghost");
        } else {
            print("You are dead");
            GetComponent<Rigidbody>().useGravity = true;
            playerCollision.anim.SetTrigger("death");
        }

        isDead = true;
        gameManager.isGameOver = true;
        gameManager.isPlaying = false;
    }

    public void PlayOneUp() {
        audioSource.clip = OneUpClip;
        audioSource.Play();
    }
}