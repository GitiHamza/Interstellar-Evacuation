using System;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private float speed;
    
    
    public Image healthBar;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       health = maxHealth;
    }

    private void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
    }
    // Update is called once per frame
    
    

    public void DealDamage(int damageAmount)
    {
        health -= damageAmount;
        health = Mathf.Clamp(health, 0, maxHealth);
        

        if (health<= 0)
        {
            Destroy(gameObject);
            Destroy(healthBar.gameObject);
        }
    }
}
