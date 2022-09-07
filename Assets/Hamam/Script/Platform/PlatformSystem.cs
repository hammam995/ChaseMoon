using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlatformSystem : MonoBehaviour
{
    // Timer controls
    public float startTime = 0f; // follower
    public float timer = 0f; // main
    public float holdTime = 3.0f; // how long you need to hold to trigger the effect , Max Holding Time
    // Use if you only want to call the method once after holding for the required time
    private bool held = false;
    public string key = "Interact"; // Whichever key you're using to control the effects. Just hardcode it in if you want

    public float counter;
    public float timer2=5; // for my timer
    public float DurationOfHoldingTime;

    public float ThePercentageOfTheHoldingTime;
    public float TotalSecconds;

    public GameObject Platform; // to put the platform
    public BoxCollider2D areaToCreateThePlatform; // to put the box collider to create the platform
    //public UnityEvent FinishEvents;
    public bool Playerinside;
    public Ui_Manager myUi;
    public bool buttonrelesed;
    public GameObject cube;
    public Color myc;
    public float pc;



    private void OnEnable()
    {
        counter = 0; // remove it
    }
    private void Start()
    {
        myUi = GameObject.Find("Ui_Manager").GetComponent<Ui_Manager>();
        myc = cube.GetComponent<SpriteRenderer>().color;

        myc.r = cube.GetComponent<SpriteRenderer>().color.r;
        myc.g = cube.GetComponent<SpriteRenderer>().color.g;
        myc.b = cube.GetComponent<SpriteRenderer>().color.b;
        myc.a = Mathf.Clamp(cube.GetComponent<SpriteRenderer>().color.a, 0, 1);
       // myc.a = 0.5f;

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Playerinside = true;

        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Playerinside = false;
        }
    }

    public void Formoula()
    {
        // % of the time when we press on the key = ( duration of the holding time / max holding time) * 100 = %
        ThePercentageOfTheHoldingTime = (DurationOfHoldingTime / holdTime) * 100;
        // the total secconds the platform will stand = (% the percentage of the holding time / 100 ) * max holding time = the amount of secconds we make the platform appear
        TotalSecconds = (ThePercentageOfTheHoldingTime / 100) * holdTime;
        timer2 += TotalSecconds;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {

           

        }


        if (Playerinside)
        {
            A1tTheMomentOfTheClick();
            A2tTheWhileHoldingTime();
            A3tTheReleseUpMoment();
            if(buttonrelesed == true) // to do it only once
            {
               myUi.UpdatingVariablesValues(-(TotalSecconds));
                buttonrelesed = false;
            }
        }
        else
        {
            if (timer > startTime)
            {
                timer = startTime;
            }
        }
        TimerGeneral();
        if (timer2 > 0)
        {
            Platform.SetActive(true);

           
            myc.a = (timer2/10);
            cube.GetComponent<SpriteRenderer>().color = myc;
        }
        else
        {
            Platform.SetActive(false);

        }
    }
    // before all of them were in the update function now putting them in the Events
    public void A1tTheMomentOfTheClick()
    {
        // Starts the timer from when the key is pressed
        if (Input.GetButtonDown("Interact")) // at the moment of the clicking the button
        {
            startTime = Time.time;
            timer = startTime;
            counter++;
        }
    }

    public void A2tTheWhileHoldingTime()
    {
        // Adds time onto the timer so long as the key is pressed , as long we are holding down the button
        if (Input.GetButton("Interact") && held == false) // thhe false condition is mean we didnt reach to the complete holding time
        {
            timer += Time.deltaTime;
            DurationOfHoldingTime = Mathf.Clamp(timer - startTime, 0, holdTime);
            // Once the timer float has added on the required holdTime, changes the bool (for a single trigger), and calls the function
            if (timer >= (startTime + holdTime + 0.0f)) // when the player press the full maximmum duration of holding time
            {
                held = true; // we completed and reached the max holding time , condition true , then Automatically we do the Action we want
                ButtonHeld();
            }
        }
    }
    public void A3tTheReleseUpMoment()
    {
        // For single effects. Remove if not needed
        if (Input.GetButtonUp("Interact")) // if we released the button we check from the DurationHoldingTime
        {
            held = false;
            if (DurationOfHoldingTime < holdTime)
            {
                Debug.Log("The Holding time is less than the goal " + holdTime);
                Formoula();
                buttonrelesed = true;
            }
        }
    }
    // Method called after held for required time
    public void ButtonHeld()
    {
        Debug.Log("You reached the maximmum held time for " + holdTime + " seconds");
        Formoula();
        buttonrelesed = true;
    }
    public void TimerGeneral()
    {
        if (timer2 > 0)
        {
            timer2 -= Time.deltaTime;
            // Debug.Log("timer==" + timer2);
            if (timer2 <= 0)
            {
                Debug.Log("times is up  secconds gone");
                timer2 = 0; // maybe delete it no need for it because in the enable we are resettting it
            }
        }
    }
}
