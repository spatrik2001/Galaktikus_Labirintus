using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab; // Az aszteroida prefab
    public float spawnInterval = 1.7f;  // Időköz az aszteroidák között
    private float timer = 0f;

    private int asteroidCount = 0;    // Összes aszteroida száma
    private int maxAsteroids = 100;  // Maximum aszteroida

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && asteroidCount < maxAsteroids)
        {
            SpawnAsteroid();
            timer = 0f;

            // Csökkentjük az időközt, hogy egyre több aszteroida jöjjön
            spawnInterval = Mathf.Max(0.5f, spawnInterval - 0.01f);
        }
    }

    void SpawnAsteroid()
    {
        // Véletlenszerű x koordináta a képernyő tetején
        float randomX = Random.Range(-16f, 16f); // -8 és 8 a képernyő szélessége

        // Aszteroida létrehozása
        Vector3 spawnPosition = new Vector3(randomX, 6f, -5); // 6 a képernyő teteje
        Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

        asteroidCount++;
    }
}