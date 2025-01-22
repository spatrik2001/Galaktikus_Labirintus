using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    private Vector2 screenBounds;
    private PolygonCollider2D spaceshipCollider;
    private Vector2 colliderSize;
    private GameUIManager gameUIManager;
    private Rigidbody2D rb;

    void Start()
    {
       
        gameUIManager = FindObjectOfType<GameUIManager>();
        if (gameUIManager == null)
        {
            Debug.LogError("GameUIManager not found in the scene!");
        }

        // Kiszámítjuk a képernyő széleit világkoordinátákban
        Camera mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

      
        spaceshipCollider = GetComponent<PolygonCollider2D>();
        colliderSize = spaceshipCollider.bounds.size;

     
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0; 
        }
    }

    void Update()
    {
        if (gameUIManager != null && !gameUIManager.IsGameRunning) return;


        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

       
        if (rb != null && moveX == 0 && moveY == 0)
        {
            rb.linearVelocity = Vector2.zero;
        }

        Vector3 position = transform.position;
        position.x += moveX;
        position.y += moveY;

      
        position.x = Mathf.Clamp(position.x, -screenBounds.x + colliderSize.x / 2, screenBounds.x - colliderSize.x / 2);
        position.y = Mathf.Clamp(position.y, -screenBounds.y + colliderSize.y / 2, screenBounds.y - colliderSize.y / 2);

        transform.position = position;
    }
}
