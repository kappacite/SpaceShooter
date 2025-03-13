using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyList
{
    public List<EnemyData> data = new List<EnemyData>();
}
public class EnemyController : MonoBehaviour
{
    public List<EnemyList> enemies;
    public GameObject enemyPrefab;
    float speed = 4f;
    public int currentEnemies;
    int enemiesKilled = 0;
    float enemySpeed = 2f;
    int currentWave = 0;
    float timeSinceLastWave = 0f;
    float timeBetweenWave = 5f;

    float startSpawnTime = 5f;
    float minSpawnTime = 0.5f;
    float reductionFactor = 0.2f;

    public void InstantiateEnemy(EnemyData enemyData)
    {

        int spawnSide = Random.Range(0, 4);
        Vector3 spawnPosition = Vector3.zero;
        Vector3 targetPosition = Vector3.zero;
        float sceneBoundary = 5f;
        float sceneSize = 8f;

        switch (spawnSide)
        {
            case 0:
                spawnPosition = new Vector3(-sceneBoundary, Random.Range(-sceneBoundary, sceneBoundary), 0f);
                break;
            case 1:
                spawnPosition = new Vector3(sceneBoundary, Random.Range(-sceneBoundary, sceneBoundary), 0f);
                break;
            case 2:
                spawnPosition = new Vector3(Random.Range(-sceneBoundary, sceneBoundary), sceneBoundary, 0f);
                break;
            case 3:
                spawnPosition = new Vector3(Random.Range(-sceneBoundary, sceneBoundary), -sceneBoundary, 0f);
                break;
        }
        
        Vector3 direction = (targetPosition - spawnPosition).normalized;

        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.GetComponent<SpriteRenderer>().sprite = enemyData.sprite;
        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = enemy.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.freezeRotation = true;
        }
        rb.velocity = direction * enemySpeed;
        currentEnemies++;
        
        EnemyDestroyed(enemy);
    }
    
    void Update()
    {
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {

        if (enemiesKilled == 30 + currentWave * 15){
            yield break;
        }
        
        if (timeSinceLastWave >= timeBetweenWave){
            timeSinceLastWave = 0;

            while (enemiesKilled != 30 + currentWave * 15){
                float spawnTime = Mathf.Max(minSpawnTime, startSpawnTime - reductionFactor * Mathf.Log(currentEnemies + 1));
                currentEnemies++;
                yield return new WaitForSeconds(spawnTime / (float) (currentEnemies*0.8));
                InstantiateEnemy(enemies[currentWave].data[Random.Range(0, enemies[currentWave].data.Count)]);
            }
            currentWave++;
        } else{
            timeSinceLastWave += Time.deltaTime;
        }
    }
    
    public void EnemyDestroyed(GameObject enemy)
    {
        currentEnemies--;
    }
}
