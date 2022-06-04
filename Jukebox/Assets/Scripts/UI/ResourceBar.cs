using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    void Update()
    {
        UpdateSlider();
    }

    public void UpdateSlider()
    {
        if (slider.value > 0)
        {
            slider.fillRect.anchorMin = new Vector2(0.5f, 0);
            slider.fillRect.anchorMax = new Vector2(slider.handleRect.anchorMin.x, 1);
        }
        else
        {
            slider.fillRect.anchorMin = new Vector2(slider.handleRect.anchorMin.x, 0);
            slider.fillRect.anchorMax = new Vector2(0.5f, 1);
        }
    }

    public void SetValue(float resource)
    {
        slider.value = resource;
    }
}
