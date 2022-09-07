using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour, IKillable
{
    private CapsuleCollider2D myCollider;
    private void Awake() 
    {
        myCollider = GetComponent<CapsuleCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IKillable killable = collision.GetComponent<IKillable>();
        if(killable != null)
        {
            killable.Kill();
        }
    }
    public void Kill()
    {
        myCollider.isTrigger = true;
        Destroy(gameObject);
    }
}
