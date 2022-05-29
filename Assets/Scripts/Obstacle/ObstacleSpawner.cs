using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] float minYOffset = 2f;
    [SerializeField] float maxYOffset = 5f;
    [SerializeField] float minSpawnDelay = 1f;
    [SerializeField] float maxSpawnDelay = 3f;
    [SerializeField] float minMoveSpeed = 5f;
    [SerializeField] float maxMoveSpeed = 15f;
    [SerializeField] int powerupSpawnRate = 5;
    [SerializeField] List<PlayableObject> enemies;
    [SerializeField] List<GameObject> obstacles;
    [SerializeField] List<GameObject> powerups;

    GameObject player;
    Health playerHealth;
    GameManager gameManager;

    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerHealth = player.GetComponent<Health>();
    }

    private void Update() {
        if (playerHealth.isDead) StopCoroutine(SpawnObject());
    }

    public IEnumerator SpawnObject() {
        while(!playerHealth.isDead) {
            float spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(spawnDelay);

            int randomSpawn = Random.Range(0, 3);
            switch (randomSpawn) {
                case 0:
                    SpawnObstacle();
                break;
                case 1:
                    SpawnEnemy();
                break;
                case 2:
                    SpawnPowerup();
                break;
            }
        }
    }

    private void SpawnPowerup()
    {
        int spawnChance = Random.Range(0, 100);
        if (spawnChance > powerupSpawnRate) return;

        int index = Random.Range(0, powerups.Count);
        Vector3 spawnPos = new Vector3 (transform.position.x, Random.Range(minYOffset, maxYOffset), transform.position.z);
        Instantiate(powerups[index], spawnPos, Quaternion.identity, transform);
    }

    private void SpawnEnemy() {
        int index = Random.Range(0, enemies.Count);
        Vector3 spawnPos = new Vector3 (transform.position.x, Random.Range(minYOffset, maxYOffset), transform.position.z);
        GameObject enemy = Instantiate(enemies[index].prefab, transform.position, Quaternion.Euler(0f, -90f, 0f), transform);
        EnemySetup(enemy, index);
    }

    private void EnemySetup(GameObject enemy, int index) {
        float moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        enemy.AddComponent<ObstacleMover>();
        enemy.GetComponent<ObstacleMover>().moveSpeed = moveSpeed;
        enemy.GetComponent<ObstacleMover>().playableIndex = enemies[index].index;
        
        enemy.AddComponent<Rigidbody>();
        enemy.GetComponent<Rigidbody>().useGravity = !enemies[index].canFly;

        if (enemies[index].isGhost) {
            enemy.tag = "Ghost";
            enemy.layer = LayerMask.NameToLayer("Ghost");
        } else {
            enemy.tag = "Enemy";
            enemy.layer = LayerMask.NameToLayer("Enemy");
        }
    }

    private void SpawnObstacle() {
        int index = Random.Range(0, obstacles.Count);
        Vector3 spawnPos = new Vector3 (transform.position.x, Random.Range(minYOffset, maxYOffset), transform.position.z);
        GameObject obstacle = Instantiate(obstacles[index], spawnPos, Quaternion.Euler(-90f, 0f, 0f), transform);

        float moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        obstacle.AddComponent<ObstacleMover>();
        obstacle.GetComponent<ObstacleMover>().moveSpeed = moveSpeed;
    }

}
