using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFall : MonoBehaviour
{

    public Rigidbody2D rb;
    [Range(0f, 3f)]
    public float TimerToBeFallen;
    [Range(0f, 3f)]
    public float TimerToBeDestroyed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("player collided");
            // 1- shake Animation , 2- fall animation , 3- make the platform falling down deactivate the kinetig rigidbody , 4- after x secconds make the platform to be destroyed
            this.Wait(1f, FallFunction);
            DestroyThePlatform();
        }
    }

    public void FallFunction()
    {
        rb.isKinematic = false;
        Debug.Log("fall function");
    }
    public void DestroyThePlatform()
    {
        Destroy(gameObject, 3f);
    }
    public void ShakeAnimation()
    {
        // when the player touch the platform it will be shaked
        // this will be the first animation
    }

    public void BreaksDownAnimation()
    {
       // this the 2nd animation it will be played after the shake animation , to show small rocks of the platform is falling
    }
}
