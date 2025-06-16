using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public GameObject[] objects;

    private GameObject child;

    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, objects.Length);
        child = Instantiate(objects[rand], transform.position, Quaternion.identity);
        child.transform.parent = transform;
        if (child.tag == "BossRoom")
        {
            StartCoroutine(waiter());
            
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(1f);
        child.SetActive(false);
    }
}
