using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Character : MonoBehaviour   
    // the base class 
    // father of all characters
{
    public string PersonName;
    public float HP; //health
    //for jumping
    public int numj; //for jumping
     // for jumping
    public float jumpForce = 500f, moveSpeed = 5f;
   public Rigidbody2D rb; //for jumping
    public bool doubleJumpAllowed = false;
    public bool onTheGround = false;
    protected BoxCollider2D charCollider; // we are only here identify the variable
                                          //[SerializeField] protected Transform objective;

    // the Deafault method for any character
    public Character()
    {
        PersonName = "Player1";
        HP = 3;
        jumpForce = 500f;
        moveSpeed = 5f;
    }

    public Character(string personName, float hP)
    {
        PersonName = personName;
        HP = hP;
    }

    protected virtual void Awake()
    {
        if (GetComponent<BoxCollider2D>())
        {
            charCollider = GetComponent<BoxCollider2D>(); //to make it take the componnent from the character
        }

    }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal"), rb.velocity.y);


    }

   public void Jump()
    {
        // jump
        if (rb.velocity.y == 0)
        {
            onTheGround = true;
            numj = 3;
        }
        else
            onTheGround = false;
        if (onTheGround)
            doubleJumpAllowed = true;
        if (onTheGround && Input.GetButtonDown("Jump") && numj > 0)
        {
            numj = numj - 1;
            Jumpup();
        }
        else if (doubleJumpAllowed && Input.GetButtonDown("Jump") && numj > 0)
        {
            numj = numj - 1;

            Jumpup();
            if (numj == 0)
            {

                doubleJumpAllowed = false;
            }
        }
    }

    public void movement(float direction ,float moveSpeed)
    {
        // x axis
        transform.Translate(new Vector2(1, 0) * direction * Time.fixedDeltaTime * moveSpeed);
        if (direction != 0)
        {
            transform.Translate(new Vector2(1, 0) * direction * Time.deltaTime * moveSpeed);
        }

        if (direction < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;

        }

        if (direction > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;

        }
    }

    public virtual void HorizontalMove(float value)
    {
        // vakue is the direction
        transform.Translate(new Vector2(1, 0) * value * Time.fixedDeltaTime * moveSpeed);
        if (value < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;

        }

        if (value > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;

        }
    }

   
    //n jump
    protected void Jumpup()
    {
         // to put force in y axis
        rb.velocity = new Vector2(rb.velocity.x, 0f); ;
        rb.AddForce(Vector2.up * jumpForce);
    }

    public virtual void TakeDamage()
    {
        this.HP -= 1; 
    }

    //another jump
    public virtual void jj()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed / 2, jumpForce);
    }

}











