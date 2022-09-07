using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Vegeta : Character
{
    // Main character
    // to create damage in character if he hit enemy 3 times in the moment of the collision
    GameObject V;
    //Rigidbody2D rb;
    [SerializeField] protected string GAMEOVER;
    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void Update()
    {
        movement(Input.GetAxis("Horizontal"), moveSpeed);
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    public override void TakeDamage()
    {
        this.HP -= 1;
        if (this.HP > 0)
        {
            Debug.Log("this is the HP" + HP);
        }
        else 
        {

            //Debug.Log("Dead");
            Debug.Log("Health is  " + HP);
           // V = GameObject.Find("Character");
          // SceneManager.LoadScene(GAMEOVER);
            // Destroy(V);

        }
    }
    // to move between the scenes after we die Game over scenes
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
            this.TakeDamage();

        if (this.HP <= 0 && collision.gameObject.tag == "enemy")
            Invoke("GameOver", 1.5f);  //by using invoke we will delay the calling methode for 1.5 secconds
        
    }


    void GameOver()
    {

        SceneManager.LoadScene(GAMEOVER);

        
    }

}
