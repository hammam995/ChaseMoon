using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CloudInteraction : MonoBehaviour, IInteractable
{
    [Header("Settings Cloud")]
    [SerializeField] private bool upOrDown;
    [SerializeField] private bool characterCanMove;
    [SerializeField] private float offsetY;
    [SerializeField] private GameObject CloudPrefab;
    public void Use()
    {
        Vector3 posCloud;
        posCloud = upOrDown ? new Vector3(transform.position.x, transform.position.y + offsetY) : new Vector3(transform.position.x, transform.position.y - offsetY);
        GameObject Cloud = Instantiate(CloudPrefab, posCloud, CloudPrefab.transform.rotation);
    }
    public bool canMove 
    { 
        get { return characterCanMove;} 
    }
    public float coefficientSpeed
    {
        get { return 0f; }
    }
}