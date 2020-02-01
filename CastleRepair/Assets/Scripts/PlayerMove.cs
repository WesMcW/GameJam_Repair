using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    [SerializeField]
    private int playerNum;

    [SerializeField]
    private GameObject knife;

    /*
     // If needed, a sprite that shows them dead
    [SerializeField]
    private Sprite player_dead;
    */

    private Rigidbody2D myRigidBody;

    public float movementSpeed;

    private bool isDead;

    private string hor, vert, rotx, roty, a, b, x, y, shoot;

    Animator anim;

    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SetPlayerControls();
        movementSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Initializes the player controls
    /// </summary>
    private void SetPlayerControls()
    {
        //Use format p1_horizontal, p2_horizontal, so player number is used to set controls  
        hor = SetInputString("Horizontal");
        vert = SetInputString("Vertical");
        rotx = SetInputString("RotateX");
        roty = SetInputString("RotateY");
        shoot = SetInputString("Fire");
    }

    /// <summary>Cleaner function for setting the string, it just sets it as "inputtype" + playernum</summary>
    /// <param name="inputStart">Input Type String first part</param>
    /// <returns>Input String in its entirety</returns>
    private string SetInputString(string inputStart)
    {
        return inputStart + playerNum.ToString();
    }

    void FixedUpdate()
    {

        if (Input.GetAxis(shoot) > 0)
        {
            Shoot();
        }


        // If they are dead, freeze them (movement speed set to 0 if we want to switch to dead sprite)
        if (isDead == true)
        {
            movementSpeed = 0;
            myRigidBody.velocity = Vector2.zero;
        }

        if (isDead == false)
        {
            Turn();
            float horizontal = Mathf.Round(Input.GetAxis(hor));
            float vertical = Mathf.Round(Input.GetAxis(vert));

            myRigidBody.velocity = new Vector2(horizontal * movementSpeed, myRigidBody.velocity.y);
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, vertical * movementSpeed);

            if (horizontal > 0) transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            else transform.rotation = Quaternion.identity;
            anim.SetFloat("Hor", horizontal);
            anim.SetFloat("Vert", vertical);
        }
    }

    /// <summary>
    /// Handles turning
    /// </summary>
    private void Turn()
    {
        float x = Input.GetAxis(rotx);
        float y = Input.GetAxis(roty);
       
        if (x != 0.0 || y != 0.0)
        {
            float angle = Mathf.Atan2(x, y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(90 - angle, Vector3.forward);
        }
    }

    private void Shoot()
    {
        print(playerNum + " is firing");
        GameObject clone = Instantiate(knife, transform.position, transform.rotation);
        // Knife needs velocity on its own script
    }

}
