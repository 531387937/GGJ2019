using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    private AudioSource AS;

    public AudioClip Attack;
    public AudioClip BeAttacked;
    public AudioClip Rope;
    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    public void PlayAttack()
    {
        AS.PlayOneShot(Attack);
    }
    public void PlayBeAttacked()
    {
        AS.PlayOneShot(BeAttacked);
    }
    public void PlayRope()
    {
        AS.PlayOneShot(Rope);
    }
}
