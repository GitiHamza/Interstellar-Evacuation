using UnityEngine;

public class Linear_EnemyMovement : MonoBehaviour
{
    public float speed = 4f;

    void Update()
    {
        // Bewegung nach links
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Enemy zerstören, wenn er links aus dem Bild ist
        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  

    
}
