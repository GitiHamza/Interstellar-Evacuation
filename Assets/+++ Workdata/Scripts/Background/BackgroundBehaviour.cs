using UnityEngine;

public class BackgroundBehaviour : MonoBehaviour
{

    public float backgroundSpeed;

    void Update()
    {
        transform.Translate(Vector2.left * backgroundSpeed * Time.deltaTime);
    }
}
