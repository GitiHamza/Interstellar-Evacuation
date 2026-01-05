using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
   
    public float health;
    public float maxHealth;
    public Image healthBar;

    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
        // Beschränkt die Füllmenge des HP Bars auf HP und maxHP
    }

    private void FixedUpdate()
    {
        PlayerDeath();
    }

    private void PlayerDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
