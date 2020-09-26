/** HEADER
 *  player movement
 * 
 * @author  (Fynn Frings) 
 * @Created (22.09.2020)
 * @Edit    (23.09.2020)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerMovement : MonoBehaviour
{
    #region ATTRIBUTES

    //Components
    private Rigidbody2D rigidbody2d;
    private UserInput userinput;

    //General
    private GameObject player;
    
    //Movement
    [Header("Movement")]
    public float movementSpeed;

    #endregion

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

    // FixedUpdate is called a fixed time in second
    void FixedUpdate()
    {
        #region FIXED UPDATE

        //Movement
        MovePlayer();

        #endregion
    }

    //Initializes important data
    public void InitializeData()
    {
        #region INITIALIZE DATA

        //Sets the player to this GameObject
        player = this.gameObject;

        //Gets the rigidbody2d of player
        rigidbody2d = player.GetComponent<Rigidbody2D>();

        //Gets the UserInput Script
        userinput = UserInput.instance;

        #endregion
    }

    //Moves the player
    public void MovePlayer()
    {
        #region MOVE PLAYER

        rigidbody2d.velocity = userinput.GetMovementInput() * movementSpeed;

        #endregion
    }
}
