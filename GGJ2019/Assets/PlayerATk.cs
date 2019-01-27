using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerATk : MonoBehaviour
{
    private bool damage;
    // Start is called before the first frame update
    private void OnEnable()
    {
        damage = false;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("32423423");
        if(collision.tag=="Enemy"&&!damage)
        {
            damage = true;
            Debug.Log("Dama");
            collision.gameObject.SendMessage("BeAttacked");
        }
    }
}
