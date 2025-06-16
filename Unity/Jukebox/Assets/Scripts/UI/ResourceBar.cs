using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    //void update()
    //{
    //    updateslider();
    //}

    //public void updateslider()
    //{
    //    if (slider.value > 0)
    //    {
    //        slider.fillrect.anchormin = new vector2(0.5f, 0);
    //        slider.fillrect.anchormax = new vector2(slider.handlerect.anchormin.x, 1);
    //    }
    //    else
    //    {
    //        slider.fillrect.anchormin = new vector2(slider.handlerect.anchormin.x, 0);
    //        slider.fillrect.anchormax = new vector2(0.5f, 1);
    //    }
    //}

    public void SetValue(float resource)
    {
        slider.value = resource;
    }
}
