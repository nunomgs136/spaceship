using UnityEngine;

public class SpawnInimigos : MonoBehaviour
{
    public GameObject naveMaePrefab;
    public Transform pontoSpawn;

    void Start()
    {
        InvokeRepeating("Spawnar", 10f, 10f);
    }

    void Spawnar()
    {
        Instantiate(naveMaePrefab, pontoSpawn.position, Quaternion.identity);
    }
}