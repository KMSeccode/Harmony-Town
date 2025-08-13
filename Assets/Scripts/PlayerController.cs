using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidade de movimento

    void Update()
    {
        // Captura input do teclado (WASD ou setas)
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        
        // Cria vetor de movimento e normaliza (para movimento diagonal não ser mais rápido)
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f).normalized;
        
        // Move o objeto
        transform.position += movement * moveSpeed * Time.deltaTime;
    }
}