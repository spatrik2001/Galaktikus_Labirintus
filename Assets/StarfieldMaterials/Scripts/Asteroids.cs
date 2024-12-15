using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float speed; 
    private float size;  

    void Start()
    {
      
        size = Random.Range(3f, 10.0f); 
        speed = Random.Range(5f, 10f);    

       
        transform.localScale = new Vector3(size, size, 1);
    }

    void Update()
    {

        transform.Translate(Vector2.down * speed * Time.deltaTime);

        
        if (transform.position.y < -6f) 
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("DeleteCollider")) 
        {
            Destroy(gameObject);
        }if (collision.CompareTag("Player")) 
        {
            Destroy(gameObject);
        }
    }
}