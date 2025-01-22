using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Asteroid Prefab and Parent")]
    public GameObject asteroidPrefab;
    public Transform asteroidParent;

    [Header("Spawn Settings")]
    [SerializeField] private float initialSpawnInterval = 1.7f;  
    [SerializeField] private float minimumSpawnInterval = 0.5f;  
    [SerializeField] private float spawnIntervalDecrease = 0.01f; 

    [Header("Spawn Area")]
    [SerializeField] private float spawnXMin = -16f; 
    [SerializeField] private float spawnXMax = 16f; 
    [SerializeField] private float spawnYMin = 6f;  
    [SerializeField] private float spawnYMax = 10f;

    private float timer = 0f;
    private float currentSpawnInterval;

    public float CurrentSpawnInterval => currentSpawnInterval; 

    void Start()
    {
        currentSpawnInterval = initialSpawnInterval; 
    }

    void Update()
    {
      
        if (FindObjectOfType<GameUIManager>()?.IsGameRunning == false) return;

        timer += Time.deltaTime;

       
        if (timer >= currentSpawnInterval)
        {
            SpawnAsteroid();
            timer = 0f;
            currentSpawnInterval = Mathf.Max(minimumSpawnInterval, currentSpawnInterval - spawnIntervalDecrease);
        }
    }

    void SpawnAsteroid()
    {
        // Véletlenszerű X és Y koordináták a megadott tartományokon belül
        float randomX = Random.Range(spawnXMin, spawnXMax);
        float randomY = Random.Range(spawnYMin, spawnYMax);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

       
        Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity, asteroidParent);
    }
}
