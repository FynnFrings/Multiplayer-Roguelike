/** HEADER
 *  player movement
 * 
 * @author  (Fynn Frings) 
 * @Created (14.10.2020)
 * @Edit    (14.10.2020)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    #region ATTRIBUTES

    //General
    public static PlayerMovement instance; //Can be called from anywhere

    //Components
    public Rigidbody2D rigidbody2d;
    private PlayerManager playermanager;
    private InputManager inputmanager;
    private PlayerAnimationManager animationmanager;

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

    // Update is called once per frame
    void Update()
    {
        #region UPDATE

        #endregion
    }

    // FixedUpdate is called a fixed time every second
    void FixedUpdate()
    {
        #region FIXED UPDATE

        //Moves the Player
        MovePlayer();

        #endregion
    }

    //Initializes important data
    public void InitializeData()
    {
        #region INITIALIZE DATA

        playermanager = PlayerManager.instance;
        inputmanager = InputManager.instance;
        animationmanager = PlayerAnimationManager.instance;

        //Sets the player to this GameObject
        playermanager.playerObject = this.gameObject;

        //Gets the rigidbody2d of player
        rigidbody2d = playermanager.playerObject.GetComponent<Rigidbody2D>();

        #endregion
    }

    //Moves the player
    public void MovePlayer()
    {
        #region MOVE PLAYER

        //moves the rigidbody2d of player
        rigidbody2d.velocity = inputmanager.movementInput * playermanager.movementSpeed;

        FlipPlayer(inputmanager.movementInput);

        #region ANIMATION STATE

        if(!isInDeadzone())
        {
            animationmanager.ChangeAnimationState(animationmanager.PLAYER_RUN);
        }
        else //standing still
        {
            animationmanager.ChangeAnimationState(animationmanager.PLAYER_IDLE);
        }

        #endregion

        #endregion
    }

    public void FlipPlayer(Vector2 movement)
    {
        #region FLIP PLAYER

        if (movement.x < 0) //walking left
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (movement.x > 0) //walking right
        {
            transform.localScale = new Vector2(1, 1);
        }
        else //standing still
        {

        }

        #endregion
    }

    private bool isInDeadzone()
    {
        if (inputmanager.movementInput.x > -playermanager.idleDeadzone && inputmanager.movementInput.x < playermanager.idleDeadzone)
        {
            if (inputmanager.movementInput.y > -playermanager.idleDeadzone && inputmanager.movementInput.y < playermanager.idleDeadzone)
            {
                return true;
            }
        }

        return false;
    }
}
