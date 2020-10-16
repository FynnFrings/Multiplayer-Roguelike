/** HEADER
 *  player animation manager
 * 
 * @author  (Fynn Frings) 
 * @Created (14.10.2020)
 * @Edit    (14.10.2020)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerAnimationManager : MonoBehaviour
{
    #region ATTRIBUTES

    public static PlayerAnimationManager instance; //Can be called from anywhere

    //General
    [SerializeField] private Animator animator;
    [SerializeField] [ReadOnly] internal string currentState = "";

    #region PLAYER ANIMATION

    //Animation States
    [SerializeField] [ReadOnly] internal string PLAYER_IDLE = "Player_Idle";
    [SerializeField] [ReadOnly] internal string PLAYER_RUN = "Player_Run";

    #endregion

    #endregion

    private void Awake()
    {
        #region INSTANCE

        //If instance does not already exists
        if (instance == null)
        {
            //This is the instance now
            instance = this;
        }
        else
        {
            //Instance already exists
            Debug.Log($"Instance already exists ({instance}), destroying this new Instance!");

            //Destroy this. because it is already available
            Destroy(this);
        }

        #endregion
    }

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

        animator = gameObject.GetComponent<Animator>();

        #endregion
    }

    //Changes the state of the animation
    public void ChangeAnimationState(string newState)
    {
        //Checks if the new Animation State is already the current Animation State
        if (currentState.Equals(newState)) return;

        //Plays the new Animation State
        animator.Play(newState);

        //Sets the new Animation State
        currentState = newState;

    }
}
