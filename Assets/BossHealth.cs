using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    
    public ChildCage childCageScript;
    //public Image healthBar;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        childCageScript = GetComponent<ChildCage>();
        health = maxHealth;
    }

    private void Update()
    {
        //healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
    }
    
    
    

    public void DealDamage(int damageAmount)
    {
        health -= damageAmount;
        health = Mathf.Clamp(health, 0, maxHealth);
        

        if (health<= 0)
        {
            Destroy(gameObject);
            //Destroy(healthBar.gameObject);
        }
    }
}

