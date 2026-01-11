using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{
    public Vector3 offset;
    public Image healthBar;
    private Camera cam;

    public Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void SetHealthBar(float health, float maxHealth)
    {
        healthBar.fillAmount = health;
    }

    private void Awake()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null || cam == null) return;
        healthBar.transform.position = cam.WorldToScreenPoint(target.position + offset);
    }
}
