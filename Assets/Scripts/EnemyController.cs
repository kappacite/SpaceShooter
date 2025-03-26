using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
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
    float reductionFactor = 1.5f;

    private static EnemyController instance;
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

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
        enemy.GetComponent<Animator>().runtimeAnimatorController = enemyData.controller;
        enemy.GetComponent<Enemy>().enemyData = enemyData;
        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = enemy.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.freezeRotation = true;
        }
        rb.velocity = direction * enemySpeed;
        currentEnemies++;
        StartCoroutine(Despawn(enemy));
       
    }

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
    private IEnumerator SpawnEnemies()
    {
        while (true){

            if (enemiesKilled >= 10 + currentWave * 15){
                timeSinceLastWave = 0;
                enemiesKilled = 0;
                currentWave++;
            } else {
                float spawnTime = Mathf.Max(minSpawnTime, startSpawnTime - reductionFactor * Mathf.Log(currentEnemies + 1));
                currentEnemies++;
                yield return new WaitForSeconds(spawnTime);
                InstantiateEnemy(enemies[currentWave].data[Random.Range(0, enemies[currentWave].data.Count)]);
            }
        }
    }
    
    public void EnemyDestroyed()
    {
        if(currentEnemies > 0) currentEnemies--;
        enemiesKilled++;
        GameObject.Find("RemainingEnemies").GetComponent<TMP_Text>().text = "Enemies restant : " + ((30 + currentWave * 15) - enemiesKilled);
    }

    private IEnumerator Despawn(GameObject enemy)
    {

        if (enemy == null) yield break;

        yield return new WaitForSeconds(15);
        Debug.Log("destroy");
        Destroy(enemy);
        if (currentEnemies > 0) currentEnemies--;
    }

    public static EnemyController getInstance() {
        return instance;
    }
}
