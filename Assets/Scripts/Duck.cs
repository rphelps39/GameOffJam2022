using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour
{
    //Animator
    public Animator animator;

    //Time
    public int maxWaitTime = 6;
    bool callWalkDuration = true;

    //Move
    bool moveAway = false;
    public float moveSpeed = 1f;
    Vector3 targetPosition;
    int quadrant;

    //Sprite
    public SpriteRenderer sr;
    bool shouldFlipX = false;

    //Mouse
    Vector2 difference = Vector2.zero;


    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        //Small ducks walk a little faster, increase animation speed
        animator.speed = 2f;

        //Get the first random target position
        targetPosition = GetRandomTargetPosition();

        //On fresh game start, wait at least 3 seconds before moving. Then anytime after that, randomly start moving.
        StartCoroutine(beginningWaitFor());
    }

    void Update()
    {
        if (moveAway)
        {
            if (shouldFlipX)
            {
                sr.flipX = true;
                shouldFlipX = false; //Set to false bc we only need this to run once
            }

            //If allowed to move, move towards target position
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            //When they start moving, we want them to periodically stop, quack, and resume moving towards a different target position (this adds some variation)
            //if (callWalkDuration)
            //{
            //    StartCoroutine(walkDuration());
            //}
        }
    }

    Vector3 GetRandomTargetPosition()
    {
        quadrant = Random.Range(1, 5);
        Vector3 tPosition;

        switch (quadrant)
        {
            case 1:
                //top
                tPosition =  new Vector3(Random.Range(-10f, 10f), 6f);
                break;
            case 2:
                //bottom
                tPosition =  new Vector3(Random.Range(-10f, 10f), -6f);
                break;
            case 3:
                //left
                tPosition =  new Vector3(-10f, Random.Range(-6f, 6f));
                break;
            case 4:
                //right
                tPosition =  new Vector3(10f, Random.Range(-6f, 6f));
                break;
            default:
                //default to top for no good reason
                tPosition =  new Vector3(Random.Range(-10f, 10f), 6f);
                break;
        }

        //If the target position x is less than the gameobjects current x, we want to flip the sprite when it starts moving bc its walking to the left
        if (tPosition.x < transform.position.x)
        {
            shouldFlipX = true;
        }

        return tPosition;
    }

    IEnumerator walkDuration()
    {
        //Don't allow this method to be called again until its complete
        callWalkDuration = false;

        //Walk for 2-4 seconds
        yield return new WaitForSeconds(Random.Range(2, 5));

        //Stop moving
        moveAway = false;
        animator.SetBool("IsWalking", false);

        //Wait half a second and quack
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("IsQuacking", true);

        //Let quack animation finish
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("IsQuacking", false);

        //Start walking again
        animator.SetBool("IsWalking", true);

        //Allow to move and call walk duration again
        moveAway = true;
        callWalkDuration = true;
    }

    IEnumerator beginningWaitFor()
    {
        //After waiting the default 3 seconds, randomly start moving after 0 - maxWaitTime seconds
        int wait_time = Random.Range(3, maxWaitTime);
        yield return new WaitForSeconds(wait_time);

        //When that time is up, allow movement
        moveAway = true;
    }

    private void OnMouseDown()
    {
        //When mouse is pressed down, find location of mouse press
        difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;

        //Stop walking or quacking
        moveAway = false;
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsQuacking", false);

        //Squirm indefinitely
        animator.SetBool("IsSquirming", true);
    }

    private void OnMouseDrag()
    {
        //When dragging, move position of this duck to mouse position
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
    }

    private void OnMouseUp()
    {
        //On mouse button up, duck is free, stop squirming
        animator.SetBool("IsSquirming", false);

        //WANT A NEW LOCATION BUT IT NEEDS TO KINDA BE IN THE SAME DIRECTION
        //Get a new random target position
        //targetPosition = GetRandomTargetPosition();

        //Allow to move again
        moveAway = true;
        //callWalkDuration = true;
        animator.SetBool("IsWalking", true);

    }
}
