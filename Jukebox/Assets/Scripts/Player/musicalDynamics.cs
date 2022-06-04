using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class musicalDynamics : MonoBehaviour
{
    [SerializeField]
    private bool crescendo = false;
    [SerializeField]
    private bool diminuendo = false;

    [SerializeField][Range(-5f, 5f)]
    private float dynamicScale;

    public float dmgModifier;
    public float speedModifier;
    public float sizeModifier;
    public float rangeModifier;
    public float moveSpeedModifier;

    private bool fortissimo = false;
    private bool pianissimo = false;

    private bool forte = false;
    private bool mezzo_forte = false;
    private bool piano = false;
    private bool mezzo_piano = false;
    private bool defaultValues = false;

    private ResourceBar scaleUI;

    void Start()
    {
        scaleUI = GameObject.FindGameObjectWithTag("mDynamicsUI").GetComponent<ResourceBar>();
        if (scaleUI != null)
        {
            scaleUI.SetValue(dynamicScale);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            crescendo = false;
            diminuendo = true;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            crescendo = true;
            diminuendo = false;
        }
        else if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.E))
        {
            crescendo = false;
            diminuendo = false;
            dmgModifier = 1f;
            speedModifier = 1f;
            sizeModifier = 1f;
            rangeModifier = 1f;
            moveSpeedModifier = 1f;

            dynamicScale = 0f;
            if (scaleUI != null)
            {
                scaleUI.SetValue(dynamicScale);
            }
            defaultValues = true;
        }

        if (Input.GetKeyDown(KeyCode.Q) && pianissimo == true)
        {
            Pianissimo();
        }
        else if (Input.GetKeyDown(KeyCode.E) && fortissimo == true)
        {
            Fortissimo();
        }

        if (dynamicScale >= 4.95f)
        {
            fortissimo = true;
        }
        else if (2.95f <= dynamicScale && dynamicScale < 4.95f)
        {
            Forte();
        }
        else if (0.95f <= dynamicScale && dynamicScale < 2.95f)
        {
            mezzoForte();
        }
        else if (-0.95f <= dynamicScale && dynamicScale < 0.95f)
        {
            Base();
        }
        else if (-2.95f <= dynamicScale && dynamicScale < -0.95f)
        {
            mezzoPiano();
        }
        else if (-4.95f < dynamicScale && dynamicScale < -2.95f)
        {
            Piano();
        }
        else if (dynamicScale <= -4.95)
        {
            pianissimo = true;
        }
    }

    public void Charge(float charge)
    {
        if (crescendo == true)
        {
            dynamicScale += charge;
        }
        else if (diminuendo == true)
        {
            dynamicScale -= charge;
        }
        if (scaleUI != null)
        {
            scaleUI.SetValue(dynamicScale);
        }
    }

    void Base()
    {
        if (defaultValues == false)
        {
            dmgModifier = 1f;
            speedModifier = 1f;
            sizeModifier = 1f;
            rangeModifier = 1f;
            defaultValues = true;
            mezzo_forte = false;
            mezzo_piano = false;
        }
    }

    void mezzoForte()
    {
        if (mezzo_forte == false)
        {
            dmgModifier = 1.25f;
            sizeModifier = 1.25f;
            speedModifier = 0.75f;
            rangeModifier = 0.75f;
            mezzo_forte = true;
            defaultValues = false;
            forte = false;
        }
    }

    void Forte()
    {
        if (forte == false)
        {
            dmgModifier = 1.5f;
            sizeModifier = 1.5f;
            speedModifier = 0.5f;
            rangeModifier = 0.5f;
            mezzo_forte = false;
            forte = true;
        }
    }

    void Fortissimo()
    {
        dmgModifier = 3f;
        sizeModifier = 3f;
        speedModifier = 0.5f;
        rangeModifier = 0.5f;
        StartCoroutine(waiter());
    }

    void mezzoPiano()
    {
        if (mezzo_piano == false)
        {
            moveSpeedModifier = 1.25f;
            speedModifier = 1.25f;
            rangeModifier = 1.25f;
            dmgModifier = 0.85f;
            defaultValues = false;
            piano = false;
            mezzo_piano = true;
        }
    }

    void Piano()
    {
        if (piano == false)
        {
            moveSpeedModifier = 1.5f;
            speedModifier = 1.5f;
            rangeModifier = 1.5f;
            dmgModifier = 0.65f;
            piano = true;
            mezzo_piano = false;
        }
    }

    void Pianissimo()
    {
        moveSpeedModifier = 3f;
        speedModifier = 3f;
        rangeModifier = 3f;
        dmgModifier = 0.65f;
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(5f);
        fortissimo = false;
        pianissimo = false;
        dmgModifier = 1f;
        speedModifier = 1f;
        sizeModifier = 1f;
        rangeModifier = 1f;
        moveSpeedModifier = 1f;

        dynamicScale = 0f;
        if (scaleUI != null)
        {
            scaleUI.SetValue(dynamicScale);
        }
        defaultValues = true;
        forte = false;
        mezzo_forte = false;
        piano = false;
        mezzo_piano = false;
    }
}