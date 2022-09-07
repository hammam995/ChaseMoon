using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IKillable enemy = collision.GetComponent<IKillable>();
        if (enemy != null)
            enemy.Kill();
    }
}
