using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float velocidade = -5f;

    void Update()
    {
        // Move o inimigo para a esquerda
        transform.Translate(Vector3.right * velocidade * Time.deltaTime);

        // Destrói o inimigo ao sair da tela pela esquerda
        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }
}