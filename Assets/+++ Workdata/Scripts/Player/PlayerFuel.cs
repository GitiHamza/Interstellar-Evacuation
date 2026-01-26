using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class PlayerFuel : MonoBehaviour
{
    public float fuel;
    public float maxFuel;
    public Image fuelBar;
    public float fuelLossPerSecond;

    void Start()
    {
        maxFuel = fuel;
    }
    
    void Update()
    {
        fuelBar.fillAmount = Mathf.Clamp(fuel / maxFuel, 0, 1);
        FuelLoss();
    }

    private void FixedUpdate()
    {
        PlayerDeath();
    }

    private void FuelLoss()
    {
        fuel -= fuelLossPerSecond * Time.deltaTime;
        fuel = Mathf.Clamp(fuel, 0, maxFuel);
    }

    public void FuelRefill()
    {
        fuel = maxFuel;
    }
    private void PlayerDeath()
    {
        if (fuel <= 0)
        {
            Destroy(gameObject);
        }
    }
}
