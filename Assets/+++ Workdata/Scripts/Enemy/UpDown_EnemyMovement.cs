using UnityEngine;

public class UpDown_EnemyMovement : MonoBehaviour
{
    public float speed = 3f;
    public float waveHeight = 1f;
    public float waveSpeed = 3f;

    private Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * waveSpeed) * waveHeight;

        transform.position = new Vector2(
            transform.position.x - speed * Time.deltaTime,
            startPosition.y + yOffset
        );
    }
}
// Start is called once before the first execution of Update after the MonoBehaviour is created
 
