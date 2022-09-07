using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corutine : MonoBehaviour
{
    // Start is called before the first frame update
    Coroutine TimeFunction;
    public bool timeFinish = false;

    public float timer;
    public float timerSet = 3f;
    public int i;
    public bool fCondition;
    public bool print;
    Coroutine c;
    void Start()
    {
        //timeFinish = false;
        //TimeFunction = CourotineTimer(3);
        // TimeFunction = StartCoroutine(CourotineTimer(3));
        StartCoroutine(firstFunction());
    }

    // Update is called once per frame
    void Update()
    {
        /* if (fCondition)
         {
             for (int i=0 ; i < 1; i++)
             {
                 Debug.Log("Before Coroutine timer");


                 c = StartCoroutine(StandardTimer());
                 Debug.Log("aFTER Coroutine timer");
                 i++;
             }
         }*/


        /* if (timeFinish == false)
         {
             TimeFunction = StartCoroutine(CourotineTimer(3)); // this will make it automatically work , storing and starting , the type is coroutine so is not gonna save it is gonna make it work
            timeFinish = true;
        }*/

        /*if (timeFinish == false)
        {
            TimeFunction = StartCoroutine(StandardTimer()); // this will make it automatically work , storing and starting , the type is coroutine so is not gonna save it is gonna make it work
            timeFinish = true;
        }*/
    }

    IEnumerator CourotineTimer(float t)
    {
        yield return new WaitForSeconds(t);        
            Debug.Log("the timer == " + t +"Secconds is finished");
        yield return new WaitForSeconds(1);
        Debug.Log("one seccond finished");
        Debug.Log("nn");
        StartCoroutine(StopFunction(TimeFunction));
      
        // StartCoroutine(SS());
    }
    IEnumerator StopFunction(Coroutine c)
    {
        yield return new WaitForSeconds(2);
        Debug.Log("the timer == 2  Secconds is finished");
        Debug.Log("Function will stop");
        StopCoroutine(c);


    }
    IEnumerator SS()
    {
        yield return new WaitForSeconds(0.01f);
        Debug.Log("Stop");
       
    }
    IEnumerator StandardTimer()
    {
        yield return new WaitForSeconds(5);


    }
    IEnumerator firstFunction()
    {
        if (print)
        {

            Debug.Log("printing");
        }
        yield return null;

    }

}
