using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookEnemy : MonoBehaviour
{
    public GameObject Hook;
    public GameObject Player;
    private void Update()
    {
        this.transform.position = Player.transform.position;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //print("1");
        if(collision.tag=="Enemy")
        {
           
                collision.transform.SetParent(null);           
        }
    }
}
