using UnityEngine;

public class Pralax : MonoBehaviour
{
    private float leght;
    public float prallaxeffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
    transform.position += Vector3.left * Time.deltaTime * parallaxEffect;
    if(transform.position.x < -lenght ) {
        transform.position = new Vector3(lenght, transform.position.y, transform.position.z);
    }

    }
}
