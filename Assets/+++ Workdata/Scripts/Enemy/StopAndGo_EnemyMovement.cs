using UnityEngine;
using System.Collections;
public class StopAndGo_EnemyMovement : MonoBehaviour
{
    public float speed = 4f;
    public float stopDistance = 8f;
    public float shootDuration = 2f;

    private Transform player;

    private enum State { Flying, Shooting }
    private State currentState = State.Flying;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Flying:
                transform.Translate(Vector2.left * speed * Time.deltaTime);

                if (distance <= stopDistance)
                {
                    StartCoroutine(ShootState());
                }
                break;
        }
    }

    IEnumerator ShootState()
    {
        currentState = State.Shooting;

        // 👉 Shoot();
        Debug.Log("Enemy schießt!");

        yield return new WaitForSeconds(shootDuration);

        currentState = State.Flying;
    }

   
}
