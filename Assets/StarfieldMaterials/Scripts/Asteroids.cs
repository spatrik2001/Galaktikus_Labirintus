using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Explosion Settings")]
    [SerializeField] private GameObject explosionPrefab; 

    [Header("Movement Settings")]
    [SerializeField] private float minSpeed = 2f;    
    [SerializeField] private float maxSpeed = 5f;   

    [Header("Size Settings")]
    [SerializeField] private float minSize = 0.5f;  
    [SerializeField] private float maxSize = 2.5f;   

    [Header("Rotation Settings")]
    [SerializeField] private float minRotationSpeed = -90f; 
    [SerializeField] private float maxRotationSpeed = 90f; 

    private float speed;         
    private float rotationSpeed; 
    private Rigidbody2D rb;      
    private GameUIManager gameUIManager; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        gameUIManager = FindObjectOfType<GameUIManager>(); 

        if (gameUIManager == null)
        {
            Debug.LogError("GameUIManager not found in the scene!");
        }

        InitializeAsteroid();
    }

    private void InitializeAsteroid()
    {
        // Véletlenszerű méret
        float size = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(size, size, 1);

        // Véletlenszerű sebesség és forgás
        speed = Random.Range(minSpeed, maxSpeed) / size; 
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed); 

        // Kezdeti mozgás
        rb.linearVelocity = Vector2.down * speed;
        rb.angularVelocity = rotationSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Debug.Log("Asteroid collided with another asteroid!");
            ReactToCollision();
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit! Asteroid destroyed.");

         
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

       
            gameUIManager?.LoseLife();

           
            Destroy(gameObject);
        }
    }

    private void ReactToCollision()
    {
        // Véletlenszerűen módosítjuk a sebességet és a forgási sebességet az ütközés után
        float newSpeed = Random.Range(minSpeed, maxSpeed);
        float newRotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);

        // Véletlenszerű új irány
        Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        // Alkalmazzuk az új mozgást és forgást
        rb.linearVelocity = randomDirection * newSpeed;
        rb.angularVelocity = newRotationSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeleteCollider"))
        {
            Debug.Log("Asteroid hit DeleteCollider. Destroying asteroid.");
            Destroy(gameObject); 
        }
    }
}
