using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cassette : MonoBehaviour
{
    private Image itemUI;

    void OnEnable()
    {
        itemUI = GameObject.FindGameObjectWithTag("consumableUI").GetComponent<Image>();
        EventManager.useConsumableEvent += cassette;
    }

    void OnDisable()
    {
        itemUI = null;
        EventManager.useConsumableEvent -= cassette;
    }

    void cassette()
    {
        Debug.Log("Used cassette");
        var tempColor = itemUI.color;
        tempColor.a = 0f;
        itemUI.color = tempColor;
        Destroy(this.gameObject);
    }
}
