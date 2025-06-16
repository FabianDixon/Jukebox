using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    public List<GameObject> enemies;
    public bool spawned = false;

    private GameObject Parent;

    void Start()
    {
        Parent = gameObject.transform.parent.gameObject;
        if (Parent.tag == "initialRoom")
        {
            StartCoroutine(waiter());
        }
    }

    void Update()
    {
        if (enemies.Count > 0)
        {
            spawned = true;
        }

        if (enemies.Count == 0 && spawned == true)
        {
            EventManager.roomCompleted();
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(2);
        EventManager.roomCompleted();
    }
}