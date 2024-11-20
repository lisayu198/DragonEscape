using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider; 
    public void SetMaxHealth(int health) {
        if(slider == null) {
            Debug.LogError("Slider is null in HealthBar");
        }
        slider.maxValue = health;
        slider.value = health;
    }
    
    public void SetHealth(int health) {
        slider.value = health;
    }
}
