using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] Slider energySlider;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void setMaxHealth(float health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    public void setHealth(float health)
    {
        healthSlider.value = health;
    }

    public void setMaxEnergy(float energy)
    {
        energySlider.maxValue = energy;
        energySlider.value = energy;
    }
    public void setEnergy(float energy)
    {
        energySlider.value = energy;
    }
}
