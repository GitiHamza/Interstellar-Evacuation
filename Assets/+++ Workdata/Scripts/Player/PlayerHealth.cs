using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
   
    public float health;
    public float maxHealth;
    public Image healthBar;

    private PlayerController _playerController;

    void Start()
    {
        maxHealth = health;
        _playerController = GetComponent<PlayerController>();
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

    public void TakeDamage(float damage)
    {
        if(_playerController != null)
        {
            damage = _playerController.ModifyDamage(damage);
        }

        if (damage <= 0f) return;
        health -= damage;
        health =  Mathf.Clamp(health, 0, maxHealth);

    }

    private void PlayerDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
