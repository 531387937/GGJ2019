using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Health;
    protected Animator animator;
    public GameObject smallSlime;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void BeAttacked()
    {
        Debug.Log("www");
        Health--;
        StartCoroutine(hurt());
    }

    public void BeHooked()
    {
        animator.SetBool("Hooked", true);
        StartCoroutine(HookTime());
    }

    IEnumerator HookTime()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Hooked", false);
    }

    public void BeDead()
    {
        animator.SetTrigger("Dead");
    }

    public void CheckDead()
    {
        if (Health <= 0)
            BeDead();
    }

    public void Dead()
    {
        Destroy(gameObject);
    }

    public void BigSlime()
    {
        Instantiate(smallSlime, transform.position + Vector3.right, Quaternion.identity);
        Instantiate(smallSlime, transform.position + Vector3.left, Quaternion.identity);
    }
    IEnumerator hurt()
    {
      
        this.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.1f);
    }


}
