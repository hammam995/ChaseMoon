using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public float timer = 0 ;
    public float timerSet = 3f;
    public bool Stop;
    public bool condition= false;
    public float myFloat = 0; // the time will start from 0
    public int transcurrido = 0; // the counter or the pinter which is i
    public int tiempoEntreMens = 5; // the time that which we enter it , and iit does not matter if we change it


    public float SetTime = 5;
    
    public bool thisIsTrue;

    void Start()
    {
        //timer=timerSet;
        timer = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        // TimerMethod(SetTime);
        if (thisIsTrue) TimerMethod(3);






        // CristopalTimer(2);



        //CristopalTimer(5);






    }
    

    public void TimerGeneral()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            Debug.Log("timer==" + timer);
            if (timer <= 0)
            {
                Debug.Log("lalal");
                timer = 0;
                
            }
        }
    }

    public void HamamTimer(float timer)
    {

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            Debug.Log("timer==" + timer);
            if (timer < 0)
            {
                Debug.Log("lalal");
                timer = 0;
            }
        }
    }
    public void CristopalTimer(float TargetTimer )
    {
        if (timer <= TargetTimer  )
        {
            timer += Time.deltaTime;
            Debug.Log("timer==" + timer);
                Debug.Log("lalal");
        }
        
    }

    public void PointerTimer() // is timer to controll the token in case he didn't stop
    {
        myFloat += Time.deltaTime;
        if (myFloat >= 1)    // in each seccond we will check enter to see the condition
        {
             Debug.Log(transcurrido);
             if (transcurrido == tiempoEntreMens)
              {
                  Debug.Log("the time is equals it is 5 sec for player is  ");
               
                  transcurrido = 0; //make the pointer 0 to reset it
                  return; /// it will return to the first if
              }
            transcurrido++;
            Debug.Log("transcurrido is ++ is" + transcurrido);
            myFloat = 0; //we will reset it because the transcurrido here will count the secconds assummed
        }
    }
    void TimerMethod(float timeToReach)
    {
        if (timer <= timeToReach)
        {
            timer += Time.deltaTime;
            Debug.Log("Lalalaa" + timer);
            if (timer >= timeToReach)
            {
                timer = 0;
                // do what we want
                thisIsTrue = false;
            }
        }
    }



}
