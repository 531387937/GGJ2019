using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHitBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            print("1331");
            collision.gameObject.SendMessage("Damage");
        }
    }
}
