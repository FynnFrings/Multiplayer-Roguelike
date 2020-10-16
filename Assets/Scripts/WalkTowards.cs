using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkTowards : MonoBehaviour
{

    #region ATTRIBUTES

    //General
    Rigidbody2D rigidbody2d;
    public CutSceneTrigger cutscenetrigger;

    public GameObject targetGameobject;
    public Transform targetPosition;
    public Vector2 movement;
    public float moveSpeed;
    public float stopRadius;
    public LayerMask targetLayer;
    public bool reachedDestination;


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region START

        InitializeData();

        #endregion
    }

    //Initializes important data
    public void InitializeData()
    {
        #region INITIALIZE DATA

        rigidbody2d = targetGameobject.GetComponent<Rigidbody2D>();

        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        CalculateDirection();

        reachedDestination = Physics2D.OverlapCircle(targetGameobject.transform.position, stopRadius, targetLayer);

        if(reachedDestination)
        {
            if (cutscenetrigger != null)
            {
                cutscenetrigger.CutSceneFinished();
            }

            this.enabled = false;
            targetGameobject.GetComponent<PlayerMovement>().enabled = true;
        }
    }

    private void FixedUpdate()
    {
        WalkTowardsPoint(movement);
    }

    //moves towards that point
    public void WalkTowardsPoint(Vector2 direction)
    {
        targetGameobject.GetComponent<PlayerMovement>().FlipPlayer(direction);

        targetGameobject.GetComponent<PlayerMovement>().enabled = false;

        rigidbody2d.MovePosition((Vector2)targetGameobject.transform.position + (direction * moveSpeed * Time.deltaTime));

        PlayAnimation();
    }

    //Calculates the direction to move towards
    private void CalculateDirection()
    {
        Vector3 direction = targetPosition.position - targetGameobject.transform.position;
        direction.Normalize();

        movement = direction;
    }

    //plays animation
    private void PlayAnimation()
    {
        targetGameobject.GetComponentInChildren<PlayerAnimationManager>().ChangeAnimationState("Player_Run");
    }

}
