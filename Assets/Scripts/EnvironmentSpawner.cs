using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour {
    [SerializeField] float minSpawnDelay = .5f;
    [SerializeField] float maxSpawnDelay = 3f;
    [SerializeField] List<GameObject> prefabs;

    GameManager gameManager;

    private void Awake() {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    public IEnumerator SpawnObject() {
        while (gameManager.isPlaying) {
            float spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(spawnDelay);
            int index = Random.Range(0, prefabs.Count);
            Instantiate(prefabs[index], transform);
        }
    }
}