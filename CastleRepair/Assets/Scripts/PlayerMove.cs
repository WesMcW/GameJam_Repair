using System.Collections;
using System.Collections.Generic;
using static AudioManager;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public int playerNum;

    private GameObject crosshairHolder; // The object on the center of the player that controls crosshair placement

    [SerializeField]
    private AudioClip knifeThrow, knifeDeflect;

    /*
     // If needed, a sprite that shows them dead
    [SerializeField]
    private Sprite player_dead;
    */

    public bool canDash;

    private Rigidbody2D myRigidBody;

    private BoxCollider2D[] myCollider;

    private Animator anim;

    public float movementSpeed;

    

    private bool isDead;

    private string hor, vert, rotx, roty, a, b, x, y, shoot, dash;

    /// <summary>If the inversion is on, change this to -1</summary>
    public int inversion = 1;

    [SerializeField]
    private GameObject knife;

    public float resetCD = 0.5f;
    private float fireCooldown;
    private bool canFire;

    public bool multiShot, pierceShot;

    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponents<BoxCollider2D>();
        crosshairHolder = transform.GetChild(0).gameObject;
        Debug.Assert(crosshairHolder != null); // Checks to ensure it's not null
        SetPlayerControls();
        movementSpeed = 5f;
        canFire = true;
        canDash = false;
        anim = GetComponent<Animator>();
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
        dash = SetInputString("Dash");
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
        Dash();

        if (Input.GetAxis(shoot) > 0)
        {
            Shoot();
        }        

        if (fireCooldown > 0)
        {
            fireCooldown -= Time.deltaTime;
            if (fireCooldown <= 0)
            {
                canFire = true;
            }
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
            float horizontal = Mathf.Round(Input.GetAxis(hor) * inversion);
            float vertical = Mathf.Round(Input.GetAxis(vert) * inversion);

            myRigidBody.velocity = new Vector2(horizontal * movementSpeed, myRigidBody.velocity.y);
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, vertical * movementSpeed);

            //Flip sprite
            if(horizontal > 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            else
            {
                transform.rotation = Quaternion.identity;
            }

            anim.SetFloat("Hor", horizontal);
            anim.SetFloat("Vert", vertical);
        }
    }

    /// <summary>
    /// Handles turning
    /// </summary>
    private void Turn()
    {
        float x = Input.GetAxis(rotx) * inversion;
        float y = Input.GetAxis(roty) * inversion;
       
        if (x != 0.0 || y != 0.0)
        {
            float angle = Mathf.Atan2(x, y) * Mathf.Rad2Deg;
            crosshairHolder.transform.rotation = Quaternion.AngleAxis(90 - angle, Vector3.forward);
        }
    }

    private void Dash()
    {
        bool cooldown, dashing;
        cooldown = dashing = false;
        float cooldownTime = 2.0f; // time between available dashes
        float dashDuration = 0.2f;
          // how long the move speed will stay modified before reverting

        float temp = movementSpeed; // backs-up normal move speed
        if (Input.GetAxis(dash) > 0)
        {
            if (canDash)
            {
                if (!cooldown && !dashing) // if not on cooldown, and not currently dashing
                {
                    dashing = true; // then begin dashing
                    movementSpeed = 25;

                }
                else if (dashing) // if already dashing
                {
                    dashDuration -= Time.deltaTime; // decrement from dash duration 
                    if (dashDuration <= 0) // if dash duration hits 0
                    {
                        movementSpeed = temp; // reset movement speed
                        dashing = false; // player is no longer dashing
                        dashDuration = 0.2f; // reset the duration timer
                    }
                }
                else if (cooldown) // if player is on cooldown, or currently dashing...
                {
                    cooldownTime -= Time.deltaTime; // derement cooldown timer
                    if (cooldownTime < 0) // if ccooldown has counted down all the way
                    {
                        cooldown = false; // dash is no longer on cooldown
                    }

                }

            }// End if
        }
        movementSpeed = temp;
    }


    private void Shoot()
    {

        if (canFire)
        {
            print(playerNum + " is firing");
            //AudioManager.instance.PlaySound(AudioManager.SoundClip.KnifeThrow);
            instance.soundPlayer.PlayOneShot(knifeThrow);
            canFire = false;
            fireCooldown = resetCD;

            if (!multiShot)
            {
                GameObject clone = Instantiate(knife, crosshairHolder.transform.position, crosshairHolder.transform.rotation);
                if (pierceShot) clone.GetComponent<Knife>().pierce = true;
                //Physics2D.IgnoreCollision(clone.GetComponent<BoxCollider2D>(), myCollider[0]);
            }
            else
            {
                for (int i = -1; i < 2; i++)
                {
                    GameObject clone = Instantiate(knife, crosshairHolder.transform.position, crosshairHolder.transform.rotation * Quaternion.Euler(0, 0, crosshairHolder.transform.rotation.z + 15 * i));
                    if (pierceShot) clone.GetComponent<Knife>().pierce = true;
                    //Physics2D.IgnoreCollision(clone.GetComponent<BoxCollider2D>(), myCollider[0]);
                    //GameObject newClone = Instantiate(knife, crosshairHolder.transform.position, Quaternion.Euler(new Vector3(0, 0, crosshairHolder.transform.rotation.z + 10)));
                }
            }
        }
    }
}
