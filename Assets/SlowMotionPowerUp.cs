using UnityEngine;

public class SlowMotionPowerUp : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Ai, fui batido!");
            FindObjectOfType<gameManager>().ActivateSlowMotion();
            Destroy(gameObject);
        }
    }
}