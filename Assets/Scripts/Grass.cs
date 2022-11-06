using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{

    public Animator animator;
    public int maxWaitTime = 5;
    public float animationTime = 2f;
    public float moveSpeed = 1f;

    void Start()
    {
        StartCoroutine(moveAfterTime());
    }

    private void Update()
    {
        if(transform.position.x > -10f)
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position = new Vector3(10f, transform.position.y);
        }
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
