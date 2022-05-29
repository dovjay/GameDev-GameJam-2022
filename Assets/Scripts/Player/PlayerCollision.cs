using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {
    public PlayableObject[] characters;
    public bool isGhost = false;
    public bool canFly = false;
    public float ghostDuration = 5f;
    int ghostIndex = 0;
    int characterIndex = 1;

    public Animator anim;
    Health playerHealth;
    AudioManager audioManager;

    private void Awake() {
        playerHealth = GetComponent<Health>();
        audioManager = GameObject.FindWithTag("GameManager").GetComponent<AudioManager>();

        // assign character for first playthrough
        ChangeCharacter(characters[characterIndex]);
    }

    private void ChangeCharacter(PlayableObject playableObject) {
        if (playerHealth.isDead) return;
        Destroy(transform.GetChild(0).transform.GetChild(0).gameObject);
        GameObject playerPrefab = Instantiate(playableObject.prefab, transform.GetChild(0));

        canFly = playableObject.canFly;
        isGhost = playableObject.isGhost;
        anim = playerPrefab.GetComponent<Animator>();
        playerPrefab.layer = isGhost ? LayerMask.NameToLayer("Ghost") : LayerMask.NameToLayer("Player");
        
        GetComponent<Rigidbody>().useGravity = !canFly;
        GetComponent<WalkController>().enabled = !canFly;
        GetComponent<FlyController>().enabled = canFly;
    }

    private void HandleMusic() {
        if (isGhost) audioManager.TurnPitch("up", 1f);
        else audioManager.TurnPitch("down", 0.7f);
    }

    private void HitByEnemy(Collision other) {
        HandleMusic();

        ObstacleMover obstacle = other.transform.GetComponent<ObstacleMover>();
        float damage = obstacle.damage;
        Destroy(other.gameObject);

        if (!isGhost) {
            playerHealth.TakeDamage(damage);
            ChangeCharacter(characters[ghostIndex]);
        } else {
            ChangeCharacter(characters[obstacle.playableIndex]);
        }
    }

    private void HitByGhost(Collision other)
    {
        playerHealth.TakeDamage(Mathf.Infinity);
        Destroy(other.gameObject);
        Destroy(transform.GetChild(0).transform.GetChild(0).gameObject);
    }

    private void HitByObstacle(Collision other)
    {
        HandleMusic();

        float damage = other.transform.GetComponent<Obstacle>().damage;
        playerHealth.TakeDamage(damage);
        ChangeCharacter(characters[ghostIndex]);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.transform.CompareTag("Enemy")) {
            HitByEnemy(other);
        }
        if (other.transform.CompareTag("Ghost")) {
            HitByGhost(other);
        }
        if (other.transform.CompareTag("Obstacle")) {
            HitByObstacle(other);
        }
    }
}