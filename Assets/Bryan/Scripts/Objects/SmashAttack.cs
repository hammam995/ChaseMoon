using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashAttack : MonoBehaviour, IKillable
{
    public void Kill()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MainCharacterFSM character = collision.GetComponent<MainCharacterFSM>();
        if(collision.tag == "Player" && character != null)
        {
            if(character.IsSmashing())
                Kill();
        }
    }
}
