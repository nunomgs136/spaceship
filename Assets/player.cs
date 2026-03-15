using UnityEngine;

public class player : MonoBehaviour
{
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;
    public KeyCode shootKey = KeyCode.Space;

    public float velocidade = 10f;
    // Prefab do raio
    public GameObject raioPrefab;

    // Ponto de onde o raio sai
    public Transform pontoDisparo;

    // Paredes
    public Transform leftWall;
    public Transform rightWall;

    void Update()
    {
        float movimento = 0f;

        if (Input.GetKey(moveLeft))
            movimento = -1f;

        if (Input.GetKey(moveRight))
            movimento = 1f;

        // Move o jogador
        Vector3 deslocamento = Vector3.right * movimento * velocidade * Time.deltaTime;
        transform.Translate(deslocamento);

        // Limites das paredes
        if (leftWall != null && rightWall != null)
        {
            Vector3 posicaoAtual = transform.position;
            posicaoAtual.x = Mathf.Clamp(posicaoAtual.x, leftWall.position.x, rightWall.position.x);
            transform.position = posicaoAtual;
        }

        // Atirar
        if (Input.GetKeyDown(shootKey))
        {
            Atirar();
        }
    }

    void Atirar()
    {
        if (raioPrefab != null && pontoDisparo != null)
        {
            Instantiate(raioPrefab, pontoDisparo.position, Quaternion.identity);
        }
    }
}