/** HEADER
 *  User Input is organized and can be called from every script.
 * 
 * @author  (Fynn Frings) 
 * @Created (23.09.2020)
 * @Edit    (14.10.2020)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class InputManager : MonoBehaviour
{
    #region ATTRIBUTES

    //General
    public static InputManager instance; //Can be called from anywhere

    //Movement Input
    [Foldout("Movement Input")]
    [SerializeField] [ReadOnly] internal Vector2 movementInput;
    [Foldout("Movement Input")]
    [SerializeField] [ReadOnly] internal bool attackInput;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        #region UPDATE

        AttackInput();

        #endregion
    }

    // FixedUpdate is called a fixed time in second
    void FixedUpdate()
    {
        #region FIXED UPDATE

        //Movement
        MovementInput();

        #endregion
    }

    //Checks Movement Input
    public void MovementInput()
    {
        #region MOVEMENT INPUT

        //Sets x of movementAxis to the Horizontal Input
        movementInput.x = Input.GetAxis("Horizontal");

        //Sets y of movementAxis to the Vertical Input
        movementInput.y = Input.GetAxis("Vertical");

        #endregion
    }

    //Checks Attack Input
    public void AttackInput()
    {
        #region ATTACK INPUT

        attackInput = Input.GetMouseButton(0);

        #endregion
    }

}
