using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public int playerNum;

    private GameObject crosshairHolder; // The object on the center of the player that controls crosshair placement

    /*
     // If needed, a sprite that shows them dead
    [SerializeField]
    private Sprite player_dead;
    */

    private Rigidbody2D myRigidBody;

    private BoxCollider2D[] myCollider;

    private Animator anim;

    public float movementSpeed;

    private bool isDead;

    private string hor, vert, rotx, roty, a, b, x, y, shoot;

    [SerializeField]
    private GameObject knife;

    public float resetCD = 0.5F;
    private float fireCooldown;
    private bool canFire;

    public bool multiShot;

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
            float horizontal = Mathf.Round(Input.GetAxis(hor));
            float vertical = Mathf.Round(Input.GetAxis(vert));

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
        float x = Input.GetAxis(rotx);
        float y = Input.GetAxis(roty);
       
        if (x != 0.0 || y != 0.0)
        {
            float angle = Mathf.Atan2(x, y) * Mathf.Rad2Deg;
            crosshairHolder.transform.rotation = Quaternion.AngleAxis(90 - angle, Vector3.forward);
        }
    }

    private void Shoot()
    {
        print(playerNum + " is firing");

        if (canFire)
        {
            canFire = false;
            fireCooldown = resetCD;

            if (!multiShot)
            {
                GameObject clone = Instantiate(knife, crosshairHolder.transform.position, crosshairHolder.transform.rotation);
                //Physics2D.IgnoreCollision(clone.GetComponent<BoxCollider2D>(), myCollider[0]);
            }
            else
            {
                for (int i = -1; i < 2; i++)
                {
                    GameObject clone = Instantiate(knife, crosshairHolder.transform.position, crosshairHolder.transform.rotation * Quaternion.Euler(0, 0, crosshairHolder.transform.rotation.z + 20 * i));
                    //Physics2D.IgnoreCollision(clone.GetComponent<BoxCollider2D>(), myCollider[0]);
                    //GameObject newClone = Instantiate(knife, crosshairHolder.transform.position, Quaternion.Euler(new Vector3(0, 0, crosshairHolder.transform.rotation.z + 10)));
                }
            }
        }
    }
}
