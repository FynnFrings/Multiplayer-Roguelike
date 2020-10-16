/** HEADER
 *  player manager stores all attributes
 * 
 * @author  (Fynn Frings) 
 * @Created (22.09.2020)
 * @Edit    (14.10.2020)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerManager : MonoBehaviour
{
    #region ATTRIBUTES

    #region GENERAL

    //General
    public static PlayerManager instance; //Can be called from anywhere

    private PlayerAnimationManager animationmanager;
    private InputManager inputmanager;
    private PlayerMovement playermovement;

    #endregion

    #region PLAYER MOVEMENT

    //General
    [Foldout("Player Movement")] 
    [SerializeField] [ReadOnly] internal GameObject playerObject;

    //Movement
    [Foldout("Player Movement")]
    [SerializeField] internal float movementSpeed = 4f;
    [SerializeField] internal float idleDeadzone = 0.25f;

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

        animationmanager = PlayerAnimationManager.instance;
        inputmanager = InputManager.instance;
        playermovement = PlayerMovement.instance;

        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region UPDATE

        #endregion
    }

    // FixedUpdate is called a fixed time every second
    private void FixedUpdate()
    {
        #region FIXED UPDATE

        #endregion
    }
}
