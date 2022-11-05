using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{

    public Animator animator;
    public int maxWaitTime = 5;
    public float animationTime = 2f;

    void Start()
    {
        StartCoroutine(moveAfterTime());
    }

    IEnumerator moveAfterTime()
    {
        while (true)
        {
            int wait_time = Random.Range(0, maxWaitTime);
            yield return new WaitForSeconds(wait_time);

            //Play animation and wait for it to finish
            animator.SetBool("IsMoving", true);
            yield return new WaitForSeconds(animationTime);

            animator.SetBool("IsMoving", false);
        }
    }

}
