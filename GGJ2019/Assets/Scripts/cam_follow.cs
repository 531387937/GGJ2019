using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_follow : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 speed = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(Player.transform.position.x, Player.transform.position.y,this.transform.position.z), ref speed, 0.1f);
    }
}

