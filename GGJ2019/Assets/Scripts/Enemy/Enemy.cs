using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected int Health;
    protected Animator animator;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void BeAttacked()
    {
        Health--;
    }

    public void BeHooked(Animator animator)
    {
        animator.SetBool("Hooked", true);
        StartCoroutine(HookTime(animator));
    }

    IEnumerator HookTime(Animator animator)
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Hooked", false);
    }

    public void BeDead(Animator animator)
    {
        animator.SetBool("Dead", true);
    }

    public void CheckDead()
    {
        if (Health <= 0)
            BeDead(GetComponent<Animator>());
    }
}
