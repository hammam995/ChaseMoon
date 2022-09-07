using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public Rigidbody2D rb;
    [Range(0.1f , 30)]
    public float Speed;
    public bool Up;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (Up)
        {
            rb.gravityScale *= -1 * Speed;
        }
        else
        {
            rb.gravityScale *= 1 * Speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            rb.isKinematic = false;
            Destroy(gameObject, 3f);
        }
    }
}
