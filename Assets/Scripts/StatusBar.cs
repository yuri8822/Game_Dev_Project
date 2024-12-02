using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField] private Slider statusBar;

    public void ChangeSliderValue(float currentValue, float maxValue)
    {
        if (statusBar != null)
        {
            statusBar.value = currentValue / maxValue;
        }
    }
}
