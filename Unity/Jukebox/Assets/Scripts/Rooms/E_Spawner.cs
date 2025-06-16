using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Spawner : MonoBehaviour
{
    public GameObject[] spawnPoints;

    public int maxEnemies;
    private int randNumEnemies;

    private float PosibleX;
    private float PosibleY;


    // Start is called before the first frame update
    void Start()
    {
        randNumEnemies = Random.Range(1, maxEnemies + 1); //limite superior exclusivo e inferior inclusivo.
        for (int i = 0; i < randNumEnemies; i++)
        {
            PosibleX = Random.Range(-237f, 237f);
            PosibleY = Random.Range(-101f, 101f);

            while ((PosibleX > -80f && PosibleX < 80f && PosibleY > 41f) || (PosibleX > -80f && PosibleX < 80f && PosibleY < -41f))
            {
                PosibleX = Random.Range(-237f, 237f);
            }
            while ((PosibleY > -49f && PosibleY < 49f && PosibleX > 120f) || (PosibleY > -49f && PosibleY < 49f && PosibleX < -120f))
            {
                PosibleY = Random.Range(-101f, 101f);
            }

            Vector2 pos = new Vector2(transform.position.x + PosibleX, transform.position.y + PosibleY);
            
            GameObject child = Instantiate(spawnPoints[0], pos, Quaternion.identity);
            child.transform.parent = transform;
        }
    }



}
