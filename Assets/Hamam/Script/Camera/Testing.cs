using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    // depends on the event number we do what we want

    // This Script when the Character be inside the box collider we do [ change the priority of the current camera to go to the Event Camera , then we do zoom in , and when we finishing zoom in we change the periority to the main camera and we reset the event camera values, in case we want to do the event again]
    public GameObject MainCamera; // the main camera object because from it we control the poriorities and the functions of the camera
    // Start is called before the first frame update
    public int EventNumber;
    void Start()
    {
        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (EventNumber == 1)
        {
            if (collision.tag == "Player")
            {
                EventNumber = MainCamera.GetComponent<Camera>().CurrentEventNumber = EventNumber;
                MainCamera.GetComponent<Camera>().EventChangeCamera = true;
                MainCamera.GetComponent<Camera>().StartCoroutine("MyTesting");
            }
        }
        if (EventNumber == 2)
        {
            EventNumber = MainCamera.GetComponent<Camera>().CurrentEventNumber = EventNumber;
            MainCamera.GetComponent<Camera>().EventChangeCamera = true;
            MainCamera.GetComponent<Camera>().StartCoroutine("MyTesting");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (EventNumber == 1)
        {
            if (collision.tag == "Player")
            {
                EventNumber = MainCamera.GetComponent<Camera>().CurrentEventNumber = EventNumber;
                MainCamera.GetComponent<Camera>().EventChangeCamera = false;
                Debug.Log(" player is outside1");
                MainCamera.GetComponent<Camera>().StartCoroutine("MyTesting");
            }
        }
        if (EventNumber == 2)
        {
            EventNumber = MainCamera.GetComponent<Camera>().CurrentEventNumber = EventNumber;
            MainCamera.GetComponent<Camera>().EventChangeCamera = false;
            Debug.Log(" player is outside2");
            MainCamera.GetComponent<Camera>().StartCoroutine("MyTesting");
        }
    }
    // Update is called once per frame
    void Update()
    {
       
        
    }
}
