using UnityEngine;

public class Follow_EnemyMovement : MonoBehaviour
{
    public float speed = 3f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        Vector2 direction = new Vector2(
            -1,
            player.position.y - transform.position.y
        ).normalized;

        transform.Translate(direction * speed * Time.deltaTime);
    }
}
