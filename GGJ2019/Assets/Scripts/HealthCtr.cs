using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCtr : MonoBehaviour
{
    public static int Health = 3;
    private int h;
    public GameObject[] hearts;
    // Start is called before the first frame update
    void Start()
    {
        hearts = new GameObject[gameObject.transform.childCount];
        h = Health;
        hearts[0] = gameObject.transform.GetChild(0).gameObject;
        hearts[1] = gameObject.transform.GetChild(1).gameObject;
        hearts[2] = gameObject.transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Health>3)
        {
            Health = 3;
        }
        if(Health<0)
        {
            Health = 0;
        }
        if(Health!=h)
        {
            foreach(GameObject heart in hearts)
            {
                heart.SetActive(false);
            }
            for(int a = 0; a<Health;a++)
            {
                hearts[a].SetActive(true);
            }
            h = Health;
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Health--;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Health++;
        }
    }
}
