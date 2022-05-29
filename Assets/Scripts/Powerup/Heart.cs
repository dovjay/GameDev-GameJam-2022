using UnityEngine;

public class Heart : MonoBehaviour {
    [SerializeField] float addHealth = 1f;

    Health playerHealth;

    private void Awake() {
        GameObject player = GameObject.FindWithTag("Player");
        playerHealth = player.GetComponent<Health>();
    }

    private void OnTriggerEnter(Collider other) {
        playerHealth.PlayOneUp();
        playerHealth.healthPoint += addHealth;
        Destroy(gameObject);
    }
}