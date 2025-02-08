using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    public GameObject asteroidPrefab;
    private float maxTime = 5f;
    private float minTime = 0f;
    private float currentTime;
    private float timeTillAsteroid;
    public int maxAsteroids = 10;
    public int currentAsteroids;
    private float asteroidSpeed = 2f;
    void Start()
    {
        timeTillAsteroid = Random.Range(minTime, maxTime);
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > timeTillAsteroid && currentAsteroids < maxAsteroids){
            InstantiateAsteroid();
        }
    }

    public void InstantiateAsteroid()
    {
        if (currentAsteroids >= maxAsteroids) return;
        
        int spawnSide = Random.Range(0, 4);
        Vector3 spawnPosition = Vector3.zero;
        Vector3 targetPosition = Vector3.zero;
        float sceneBoundary = 5f;
        float sceneSize = 8f;
        
        switch (spawnSide)
        {
            case 0:
                spawnPosition = new Vector3(-sceneBoundary, Random.Range(-sceneBoundary, sceneBoundary), 0f);
                targetPosition = new Vector3(sceneSize, Random.Range(-sceneSize, sceneSize), 0f);
                break;
            case 1:
                spawnPosition = new Vector3(sceneBoundary, Random.Range(-sceneBoundary, sceneBoundary), 0f);
                targetPosition = new Vector3(-sceneSize, Random.Range(-sceneSize, sceneSize), 0f);
                break;
            case 2:
                spawnPosition = new Vector3(Random.Range(-sceneBoundary, sceneBoundary), sceneBoundary, 0f);
                targetPosition = new Vector3(Random.Range(-sceneSize, sceneSize), -sceneSize, 0f);
                break;
            case 3:
                spawnPosition = new Vector3(Random.Range(-sceneBoundary, sceneBoundary), -sceneBoundary, 0f);
                targetPosition = new Vector3(Random.Range(-sceneSize, sceneSize), sceneSize, 0f);
                break;
        }
        
        Vector3 direction = (targetPosition - spawnPosition).normalized;

        GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = asteroid.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.freezeRotation = true;
        }
        rb.velocity = direction * asteroidSpeed;
        
        currentAsteroids++;
        timeTillAsteroid = Random.Range(minTime, maxTime);
        currentTime = 0;

        AsteroidDestroyed(asteroid);
    }
    
    public void AsteroidDestroyed(GameObject asteroid)
    {
        Destroy(asteroid, 10f);
        currentAsteroids--;
    }
}
