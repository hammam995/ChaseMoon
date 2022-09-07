using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraIdleSitiuation : MonoBehaviour
{
    public GameObject MainCamera;
    public bool CharacterisIdle;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera.GetComponent<Camera>().StartCoroutine("CameraCharacterIdle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
