using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInsideTrigger : PlatformBase
{
    // is the only state machine we have for thr platform
    // making the abstract classe functions as a methods have body even if we don't use it we make it empty body , we declare it is body
    // put the variables values here not in the main

    public override void EnterState(PlatformManager MainPlatform) // the start variables the same from the start of the monobehavior
    {
         // is working well only change between the cube and platform , make the color is of the platform
        Debug.Log("Player is in Standby sitiuation inside the first state");
        MainPlatform.myUi = GameObject.Find("Ui_Manager").GetComponent<Ui_Manager>();
        MainPlatform.myc = MainPlatform.Platform.GetComponent<SpriteRenderer>().color;

        MainPlatform.myc.r = MainPlatform.Platform.GetComponent<SpriteRenderer>().color.r;
        MainPlatform.myc.g = MainPlatform.Platform.GetComponent<SpriteRenderer>().color.g;
        MainPlatform.myc.b = MainPlatform.Platform.GetComponent<SpriteRenderer>().color.b;
        MainPlatform.myc.a = Mathf.Clamp(MainPlatform.Platform.GetComponent<SpriteRenderer>().color.a, 0, 1);

    }
    public override void UpdateState(PlatformManager MainPlatform)
    {



        if (MainPlatform.Playerinside)
        {
            A1tTheMomentOfTheClick(MainPlatform);
            A2tTheWhileHoldingTime(MainPlatform);
            A3tTheReleseUpMoment(MainPlatform);
            if (MainPlatform.buttonrelesed == true) // to do it only once
            {
                MainPlatform.myUi.UpdatingVariablesValues(-(MainPlatform.TotalSecconds));
                MainPlatform.buttonrelesed = false;
            }
        }
        else
        {
            if (MainPlatform.timer > MainPlatform.startTime)
            {
                MainPlatform.timer = MainPlatform.startTime;
            }
        }
        TimerGeneral(MainPlatform);
        if (MainPlatform.timer2 > 0)
        {
            MainPlatform.Platform.SetActive(true);


            MainPlatform.myc.a = (MainPlatform.timer2 / 10);
            MainPlatform.Platform.GetComponent<SpriteRenderer>().color = MainPlatform.myc;
        }
        else
        {
            MainPlatform.Platform.SetActive(false);

        }
    }
    public override void OnTriggerEnter(PlatformManager MainPlatform , Collider2D other) // complete
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player is inside the trigger state machine");
            MainPlatform.Playerinside = true;
        }
    }
    public override void OnTriggerExit(PlatformManager MainPlatform , Collider2D other) // complete
    {
        Debug.Log("Player is outttside the trigger state machine");
        MainPlatform.Playerinside = false;
    }

    public void Formoula(PlatformManager MainPlatform) // formoula from the monobehavior class
    {
        // % of the time when we press on the key = ( duration of the holding time / max holding time) * 100 = %
        MainPlatform.ThePercentageOfTheHoldingTime = (MainPlatform.DurationOfHoldingTime / MainPlatform.holdTime) * 100;
        // the total secconds the platform will stand = (% the percentage of the holding time / 100 ) * max holding time = the amount of secconds we make the platform appear
        MainPlatform.TotalSecconds = (MainPlatform.ThePercentageOfTheHoldingTime / 100) * MainPlatform.holdTime;
        MainPlatform.timer2 += MainPlatform.TotalSecconds;
    }

    public void A1tTheMomentOfTheClick(PlatformManager MainPlatform) // At1 from the monobehavior
    {
        // Starts the timer from when the key is pressed
        if (Input.GetButtonDown("Interact")) // at the moment of the clicking the button
        {
            MainPlatform.startTime = Time.time;
            MainPlatform.timer = MainPlatform.startTime;
            MainPlatform.counter++;
        }
    }

    public void A2tTheWhileHoldingTime(PlatformManager MainPlatform) // At2 from the monobehavior
    {
        // Adds time onto the timer so long as the key is pressed , as long we are holding down the button
        if (Input.GetButton("Interact") && MainPlatform.held == false) // thhe false condition is mean we didnt reach to the complete holding time
        {
            MainPlatform.timer += Time.deltaTime;
            MainPlatform.DurationOfHoldingTime = Mathf.Clamp(MainPlatform.timer - MainPlatform.startTime, 0, MainPlatform.holdTime);
            // Once the timer float has added on the required holdTime, changes the bool (for a single trigger), and calls the function
            if (MainPlatform.timer >= (MainPlatform.startTime + MainPlatform.holdTime + 0.0f)) // when the player press the full maximmum duration of holding time
            {
                MainPlatform.held = true; // we completed and reached the max holding time , condition true , then Automatically we do the Action we want
                ButtonHeld(MainPlatform);
            }
        }
    }

    public void A3tTheReleseUpMoment(PlatformManager MainPlatform) // At3 from the monobehavior
    {
        // For single effects. Remove if not needed
        if (Input.GetButtonUp("Interact")) // if we released the button we check from the DurationHoldingTime
        {
            MainPlatform.held = false;
            if (MainPlatform.DurationOfHoldingTime < MainPlatform.holdTime)
            {
                Debug.Log("The Holding time is less than the goal " + MainPlatform.holdTime);
                Formoula(MainPlatform);
                MainPlatform.buttonrelesed = true;
            }
        }
    }












    // Method called after held for required time
    public void ButtonHeld(PlatformManager MainPlatform) // Buttonheld from the monobehavior
    {
        Debug.Log("You reached the maximmum held time for " + MainPlatform.holdTime + " seconds");
        Formoula(MainPlatform);
        MainPlatform.buttonrelesed = true;
    }

    public void TimerGeneral(PlatformManager MainPlatform) // TimerGeneral from the monobehavior
    {
        if (MainPlatform.timer2 > 0)
        {
            MainPlatform.timer2 -= Time.deltaTime;
            // Debug.Log("timer==" + timer2);
            if (MainPlatform.timer2 <= 0)
            {
                Debug.Log("times is up  secconds gone");
                MainPlatform.timer2 = 0; // maybe delete it no need for it because in the enable we are resettting it
            }
        }
    }



    /*public void OnTriggerEnter2D(Collider2D other , PlatformManager MainPlatform)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player is inside the trigger state");
            MainPlatform.Playerinside = true;
            MainPlatform.counter += 1;
        }
    }
    public void OnTriggerExit2D(Collider2D other , PlatformManager MainPlatform)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player is outttside the trigger state");
            MainPlatform.Playerinside = false;
        }
    }
    */





}
