using UnityEngine;

public class Obstacle : MonoBehaviour {
    public float damage = 1f;

    GameObject player;

    private void Awake() {
        player = GameObject.FindWithTag("Player");
    }

    private void OnCollisionEnter(Collision other) {
        if (other.transform.CompareTag("Player")) {
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
    }
}