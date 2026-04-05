using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float velocidade = -5f;

    void Update()
    {
        // Move o inimigo para a esquerda
        transform.Translate(Vector3.up * velocidade * Time.deltaTime);

        // Destrói o inimigo ao sair da tela pela esquerda
        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Raio"))
        {
            gameManager.instance.InimigoDestruido();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            gameManager.instance.PlayerAtingido();
            Destroy(gameObject); // ← esse era o problema, faltava destruir o alien
        }
    }

}