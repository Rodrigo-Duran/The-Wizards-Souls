using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] Slider slider;

    public void UpdateHealthBarValue(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }
}
