using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creedito : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] float timeToClose = 2;
    GameObject creditPanel;
    float timer;
    Vector3 initialPosition;
    private void OnEnable()
    {
        creditPanel = transform.parent.gameObject; // to make the object in the text be the child of the parent and take his transform value from the father which is the panale
        initialPosition = transform.localPosition; // to make the original position move relative to father position how long it far or how long it is distance from the father
        //the same position  but it will tell us how is the distance between him and his father
    }
    private void OnDisable()
    {
        transform.localPosition = initialPosition; // to reset it to the original point
    }
    void Update()
    {
        if(transform.position.y< creditPanel.transform.position.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, creditPanel.transform.position, speed);
        }
        else
        {
            StartCoroutine(CloseCreditPanel(1));
        }
        if (Input.GetKey(KeyCode.Escape))
            {
            StartToClose(timeToClose);
              }
        if (Input.GetKeyUp(KeyCode.Escape)) // after we relese leave the key
        {
            timer = 0; 
        }
    }
    void StartToClose(float time)
    {
        timer += Time.deltaTime;
        if(timer>= time)
        {
            creditPanel.SetActive(false);
            timer = 0;
        }
    }
    IEnumerator CloseCreditPanel(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        creditPanel.SetActive(false);
    }
}
