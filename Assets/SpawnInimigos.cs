using UnityEngine;

public class SpawnInimigos: MonoBehaviour
{
    [Header("Prefab e paredes")]
    public GameObject inimigoPrefab;
    public Transform topWall;
    public Transform bottomWall;
    public Transform rightWall;

    [Header("Configuração de spawn")]
    public float intervaloMinimo = 1f;
    public float intervaloMaximo = 3f;

    private float temporizador;

    void Start()
    {
        temporizador = GerarIntervalo();
    }

    void Update()
    {
        temporizador -= Time.deltaTime;

        if (temporizador <= 0f)
        {
            SpawnarInimigo();
            temporizador = GerarIntervalo();
        }
    }

    void SpawnarInimigo()
    {
    Camera cam = Camera.main;
    float alturaMetade = cam.orthographicSize;
    float larguraMetade = cam.orthographicSize * cam.aspect;

    float yAleatorio = Random.Range(-alturaMetade, alturaMetade);
    float xSpawn = cam.transform.position.x + larguraMetade + 1f;

    Vector3 posicaoSpawn = new Vector3(xSpawn, yAleatorio, 0f);
    Quaternion rotacao = Quaternion.Euler(0f, 0f, 90f);

    Instantiate(inimigoPrefab, posicaoSpawn, rotacao);
    }

    float GerarIntervalo()
    {
        return Random.Range(intervaloMinimo, intervaloMaximo);
    }
}