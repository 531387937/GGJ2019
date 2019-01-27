using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hook : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Floor")
        {
           Player.SendMessage("SwitchRopeState");
            
        }
        if(collision.tag == "Enemy")
        {
            Player.SendMessage("HookChildBack",collision.gameObject);
            //collision.gameObject.transform.SetParent(this.gameObject.transform);
        }
        else
        {

            Player.SendMessage("HookBack");
        }
      
    }
}
