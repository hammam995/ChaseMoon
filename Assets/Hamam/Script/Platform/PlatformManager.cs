using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    // is the main class to control the states machine of an object , initialize all states in the monobehavior script , to have full controll on them
    // the easy way which is we put the variables in the monobehavior script , then from the states we control it and change it is values
    // the other way is to put the numeric ( the numbers) in the state machine we need to use it rather than putting it in the monobehhavior script, (only put in the monobehavior the variable needs a to be refrenced from object

    // Timer controls
    public float startTime = 0f; // follower
    public float timer = 0f; // main
    public float holdTime = 3.0f; // how long you need to hold to trigger the effect , Max Holding Time
    // Use if you only want to call the method once after holding for the required time
    public bool held = false;
    public string key = "Interact"; // Whichever key you're using to control the effects. Just hardcode it in if you want

    public float counter;
    public float timer2 = 5; // for my timer
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



    public PlatformBase CurentState; // the state name
    public PlayerInsideTrigger PlayerinsidetriggerState = new PlayerInsideTrigger();




    // updatings the functions here , then in the state we override it is abstract


    void Start()
    {
        CurentState = PlayerinsidetriggerState;
        CurentState.EnterState(this); // we Automatically start the state machine from the start , and the rest will be updating from the states machine*/
    }

    void Update()
    {
        CurentState.UpdateState(this);
    }
    public void OnTriggerEnter2D(Collider2D other )
    {
        CurentState.OnTriggerEnter(this , other); // here we will pass this script the monobehavior and the Collider2d
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        CurentState.OnTriggerExit(this, other);
    }
   


}
