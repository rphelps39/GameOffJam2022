using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour
{

    public Animator animator;
    public int maxWaitTime = 5;
    bool moveAway = false;
    public float moveSpeed = 1f;
    public SpriteRenderer sr;
    Vector3 targetPosition;
    bool callWalkDuration = true;
    int quadrant;

    void Start()
    {
        animator.SetBool("IsWalking", true);
        targetPosition = GetRandomTargetPosition();
        //randomly pick a time to start moving
        StartCoroutine(moveAfterTime());
    }

    void Update()
    {
        if (moveAway)
        {
            //if allowed to move, move towards target position
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            if (callWalkDuration)
            {
                StartCoroutine(walkDuration());
                callWalkDuration = false;
            }
        }
    }

    Vector3 GetRandomTargetPosition()
    {
        quadrant = Random.Range(1, 5);

        switch (quadrant)
        {
            case 1:
                //top
                return new Vector3(Random.Range(-10f, 10f), 6f);
            case 2:
                //bottom
                return new Vector3(Random.Range(-10f, 10f), -6f);
            case 3:
                //left
                return new Vector3(-10f, Random.Range(-6f, 6f));
            case 4:
                //right
                return new Vector3(10f, Random.Range(-6f, 6f));
            default:
                //default to top for no good reason
                return new Vector3(Random.Range(-10f, 10f), 6f);
        }
    }

    IEnumerator walkDuration()
    {
        //walk for 2-4 seconds
        yield return new WaitForSeconds(Random.Range(2, 5));

        //stop moving
        moveAway = false;
        animator.SetBool("IsWalking", false);

        //wait half a second and quack
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("IsQuacking", true);

        //let quack animation finish
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("IsQuacking", false);

        //start walking again and repeat cycle
        animator.SetBool("IsWalking", true);

        moveAway = true;
        callWalkDuration = true;
    }

    IEnumerator moveAfterTime()
    {
        int wait_time = Random.Range(0, maxWaitTime);
        yield return new WaitForSeconds(wait_time);
        moveAway = true;
    }
}
