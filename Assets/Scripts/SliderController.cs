using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] Slider slider;

    public void UpdateSliderValue(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }
}
