using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour
{
    [Header("PowerUp Settings")]
    public GameObject slowMotionPowerUpPrefab; // Arraste o prefab do powerup aqui
    
    [Header("Spawn Settings")]
    public float minSpawnTime = 5f;    // Tempo mínimo entre spawns
    public float maxSpawnTime = 15f;   // Tempo máximo entre spawns
    
    [Header("Wall References (use as mesmas do player)")]
    public Transform leftWall;   // Referência da parede esquerda
    public Transform rightWall;  // Referência da parede direita
    public Transform topWall;    // Referência da parede superior
    public Transform bottomWall; // Referência da parede inferior
    
    [Header("Spawn Offset (opcional)")]
    public float offsetFromWalls = 0.5f; // Distância das paredes para não spawnar colado
    
    void Start()
    {
        // Verificar se as paredes foram atribuídas
        if (leftWall == null || rightWall == null || topWall == null || bottomWall == null)
        {
            Debug.LogWarning("Algumas referências de paredes não foram atribuídas no PowerUpSpawner!");
            Debug.LogWarning("Usando valores padrão (-15 a 15 para X, -8 a 8 para Y)");
        }
        
        // Começar a spawnar powerups em intervalos aleatórios
        StartCoroutine(SpawnPowerUps());
    }
    
    IEnumerator SpawnPowerUps()
    {
        while (true) // Loop infinito para spawnar durante todo o jogo
        {
            // Aguardar um tempo aleatório
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);
            
            // Spawnar um powerup
            SpawnPowerUp();
        }
    }
    
    void SpawnPowerUp()
    {
        if (slowMotionPowerUpPrefab != null)
        {
            // Calcular os limites de spawn baseado nas paredes
            float minX, maxX, minY, maxY;
            
            if (leftWall != null && rightWall != null && topWall != null && bottomWall != null)
            {
                // Usar as posições das paredes como limites
                minX = leftWall.position.x + offsetFromWalls;
                maxX = rightWall.position.x - offsetFromWalls;
                minY = bottomWall.position.y + offsetFromWalls;
                maxY = topWall.position.y - offsetFromWalls;
                
                // Garantir que min não seja maior que max
                if (minX > maxX)
                {
                    Debug.LogWarning("LeftWall está à direita da RightWall! Trocando os valores.");
                    float temp = minX;
                    minX = maxX;
                    maxX = temp;
                }
                
                if (minY > maxY)
                {
                    Debug.LogWarning("BottomWall está acima da TopWall! Trocando os valores.");
                    float temp = minY;
                    minY = maxY;
                    maxY = temp;
                }
            }
            else
            {
                // Valores padrão caso as paredes não estejam atribuídas
                minX = -15f;
                maxX = 15f;
                minY = -8f;
                maxY = 8f;
                Debug.Log($"Usando valores padrão: X[{minX}, {maxX}], Y[{minY}, {maxY}]");
            }
            
            // Gerar posição aleatória dentro dos limites calculados
            Vector3 spawnPosition = new Vector3(
                Random.Range(minX, maxX),
                Random.Range(minY, maxY),
                0f
            );
            
            // Instanciar o powerup
            GameObject newPowerUp = Instantiate(slowMotionPowerUpPrefab, spawnPosition, Quaternion.identity);
            
            Debug.Log($"PowerUp spawnado em: {spawnPosition} (Limites: X[{minX:F1}, {maxX:F1}], Y[{minY:F1}, {maxY:F1}])");
        }
        else
        {
            Debug.LogError("Slow Motion PowerUp Prefab não atribuído no PowerUpSpawner!");
        }
    }
    
    // Método opcional para visualizar os limites de spawn no Editor
    void OnDrawGizmosSelected()
    {
        if (leftWall != null && rightWall != null && topWall != null && bottomWall != null)
        {
            float minX = leftWall.position.x + offsetFromWalls;
            float maxX = rightWall.position.x - offsetFromWalls;
            float minY = bottomWall.position.y + offsetFromWalls;
            float maxY = topWall.position.y - offsetFromWalls;
            
            // Desenhar um retângulo mostrando a área de spawn
            Gizmos.color = Color.green;
            Vector3 center = new Vector3((minX + maxX) / 2f, (minY + maxY) / 2f, 0);
            Vector3 size = new Vector3(maxX - minX, maxY - minY, 0);
            Gizmos.DrawWireCube(center, size);
        }
    }
}