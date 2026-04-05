using UnityEngine;

public class SlowMotionPowerUp : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f; // Velocidade de movimento do powerup
    public Vector3 moveDirection = Vector3.left; // Direção de movimento
    
    void Update()
    {
        // Move o powerup (opcional - pode ser estático também)
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        
        // Destruir se sair da tela
        if (transform.position.x < -20f || transform.position.x > 20f || 
            transform.position.y > 10f || transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PowerUp de Slow Motion coletado!");
            gameManager.instance.ActivateSlowMotion();
            Destroy(gameObject);
        }
    }
}