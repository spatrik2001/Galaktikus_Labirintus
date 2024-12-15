using UnityEngine; 
using System.Collections;

public class Move : MonoBehaviour
{
    public float moveSpeed = 5f; // Mozgási sebesség

    void Update()
    {
        // Bemenet olvasása (WASD vagy Nyilak)
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime; // Vízszintes mozgás (A/D)
        float moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;   // Függőleges mozgás (W/S)

        // Új pozíció kiszámítása
        Vector3 newPosition = transform.position + new Vector3(moveX, moveY, 0);

        // Az űrhajó pozíciójának frissítése
        transform.position = newPosition;
    }
}