using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;


// do function have slider to modify on the zoom distance
public class Camera : MonoBehaviour
{
    [Header("Virtual Camera Values")]
    public CinemachineVirtualCamera virtualCamera;
    [SerializeField] float OrthographicSize; // to take the OrthographicSize from the Virtual Camera
    public int CameraNumber;
    private CinemachineFramingTransposer VarCinemachineFramingTransposer; // m_GroupFramingSize
    

    [Header("Character is Sleeping Values")]
    public bool IsCharacter1Sleeping; // character1.GetComponent<Rigidbody2D>().IsSleeping() 
    public bool IsCharacter2Sleeping; // character1.GetComponent<Rigidbody2D>().IsSleeping() 
    private Rigidbody2D RigCharacter1; // take the rigid body of the first character
    private Rigidbody2D RigCharacter2; // take the rigid body of the seccond character


    [Header("Zoom Out Values")]
    [SerializeField] bool ZoomOut; // the Condition to do the Zoom out Function
    [SerializeField] float ZoomOutSpeed = 5f; // the Speed of the Zoom out
    [SerializeField] float ZoomOutValueTarget = 10f; // The new OrthographicSize value when we want to do the Zoom out in Zoom Out Function

    [Header("Zoom In Values")]
    public bool ZoomIn; // the Condition to do the Zoom In Function
    [SerializeField] float ZoomInSpeed = 3f;  // the Speed of the Zoom out
    [SerializeField] float ZoomInValueTarget = 1.2f;  // The new OrthographicSize value when we want to do the Zoom In in Zoom In Function

    [Header("Origin Zoom Values")]
    [SerializeField] bool OriginZoom;  // the Condition to do the Zoom out Function
    [SerializeField] float OriginZoomSpeed = 1f;   // the Speed of the Zoom out , Currently we are not using it
    [SerializeField] float OriginZoomValue; // will be equal to the standard zoom == OrthographicSize == 5f or if we will change it , it will be used in the Standard Zoom Function


    [Header("Event Change Target")]
    [SerializeField] bool EventChangeFollowTarget;   // not usable variables
    [SerializeField] GameObject NewObjectToFollow1; //(For Event 1) to put the new object that the camera we want it to follow , from the main camera to the new object it will be blending between them with n (3) secconds
    [SerializeField] GameObject NewObjectToFollow2; // (For Event 2) to put the new object that the camera we want it to follow , from the main camera to the new object it will be blending between them with n (3) secconds


    [Header("Event Periority")]
    public bool EventChangeCamera;
    [SerializeField] int CameraCurrnetPriority;  // not usable variable only in the start function
    [SerializeField] int CameraNewPriority;  // a variable to take the Event Camera Poriority , so from it we can swap and blending between the main camera and the event camera by changing the current periority
    [SerializeField] int CameraOriginPriority; // a variable to save the camera origin priority before we change it or doing swap between the periorities of the cameras (save main Camera Priority)
    [SerializeField] GameObject NewCameraOject; // to take the seccond camera which is the Event camera , Camera Event

    [Header("Character Idle")]
    [SerializeField] bool IdleZoomOut; // the Condition to do the Idle Zoom out Function
    [SerializeField] float IdleZoomOutSpeed = 2f; // the Speed of the Idle Zoom out
    [SerializeField] float IdleZoomOutValueTarget = 0.001f; // The new OrthographicSize value when we want to do the Zoom out in Zoom Out Function
    [SerializeField] float IdleZoomOutOriginValue = 0.42f; // the origin value we can tak it from the group framin size from the start by assigning it to that variable
    public bool restofconditions;
    public GameObject character1; // To take the object of the first player character
    public GameObject character2; // To take the object of the seccond player character

    [Header("Event")]
    public int CurrentEventNumber; // inisde change camera we put if AND THE NUMBER OF THE EVENT WE WANT
    public UnityEvent FinishZoomOutEvents;

    // unnnecessarly Header
    [Header("Timer")]
    public float timer;
    public float timerSet = 10f;
    public bool timeFinish = false;
    public float timerSet2 = 6f;
    public float timer22;
    

    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        OrthographicSize = virtualCamera.m_Lens.OrthographicSize;
        OriginZoomValue = OrthographicSize;
        CameraCurrnetPriority = virtualCamera.m_Priority;
        CameraOriginPriority = virtualCamera.m_Priority;
        //
        VarCinemachineFramingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        RigCharacter1 = character1.GetComponent<Rigidbody2D>();
        RigCharacter2 = character2.GetComponent<Rigidbody2D>();
        SleepingCondition();
        
        //
        timer = timerSet;
        timer22 = timerSet2;
        if (CameraNumber == 1)
        {
            StartCoroutine("CameraCharacterIdle");
        }
    }

    void Update()
    {
         IdleFunction();
        
    }


    public IEnumerator CameraCharacterIdle()
    {
        if (CameraNumber == 1) // the condition to check we are using which camera the character or the event camera , 1 is Character camera , 2 is Event Camera , we do that in case the periority change also
        {
            if (SleepingCondition()) // the condition to check if both characters are not moving , so they are in the sleeping state
            {
                Debug.Log("insid idle corotine before the timer");
                yield return new WaitForSeconds(10f); // the timer to wait before we activate the IdleZoom , it means after n secconds if the character is not moving then we do what we want
                Debug.Log("the timer 10 secconds finished");
                Debug.Log("now checking if the conditions of the idle Camera is working else dont do it"); // when no event happen from the camera
                if (!ZoomOut && !ZoomIn && !OriginZoom && !EventChangeFollowTarget && !EventChangeCamera  ) // it is mean that we are not doing any Event in the Seccond Camera then we do the IdleZoom out
                {
                        IdleZoomOut = true;
                        StartCoroutine("CameraCharacterIdle");
                }
            }
            else
            {
                IdleZoomOut = false;
                Debug.Log("Conditions are false not doing the idle zoom out");
                Debug.Log("C now going to see conditions are characters are moving");
                yield return new WaitForSeconds(1f);
                StartCoroutine("CameraCharacterIdle");
            }
        }
        else
        {

        }
    }

    public void IdleFunction()
    {
        if (CameraNumber == 1)
        {
            if(EventChangeCamera ==  true) // if we swapping a camera dont do it
            {
                IdleZoomOut = false;
            }
            if (IdleZoomOut) 
            {
                virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_GroupFramingSize = Mathf.Lerp(virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_GroupFramingSize, 0.001f, (Time.deltaTime / 2f));
            }
            if (!SleepingCondition()) // the condition if the character axis is not moving for long time is true , is mean the state is Awaking which is moving , so if one of the characters are moving then don't do the idle zoom
            {
                IdleZoomOut = false;
                if (IdleZoomOut == false && !ZoomOut && !ZoomIn && !OriginZoom && !EventChangeFollowTarget && !EventChangeCamera ) // when it is false we reset the value of the framing size to the origin size before we do the zoom out functiont
                {
                    virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_GroupFramingSize = IdleZoomOutOriginValue;
                }
            }
        }
    }
    public bool SleepingCondition() // a function to save inside it the boolean variables of the characters rigidbody to check if they are sleeping or awaking
    {
        IsCharacter1Sleeping = RigCharacter1.IsSleeping();
        IsCharacter2Sleeping = RigCharacter2.IsSleeping();
        if (IsCharacter1Sleeping == true && IsCharacter2Sleeping == true)
        {
            return true;
        }
        else
            return false;
    }



    public void CameraCharIdle() // in the update
    {
        if (CameraNumber == 1)
        {
            if (IdleZoomOut) 
            {                
                // if we are not doing event in the camera event , then we can do the idle zoom out because it will be focused in the player only
                if (!ZoomOut && !ZoomIn && !OriginZoom && !EventChangeFollowTarget && !EventChangeCamera) // if no event happend then we are focusing in the player character camera , then we do this
                {
                    StartCoroutine("CameraCharacterIdle");
                }
                else
                {
                    Debug.Log("Conditions are false not starting corotine");
                }
            }
        }
    }
    // the Enumerator it will be as setting values and trigger to the function in the update
    public IEnumerator MyTesting() // basic corouine from it we keep contimue doing like it for other functions
    {
        if(CurrentEventNumber == 1)
        {
            if (EventChangeCamera)  // the condition to change between the cameras Periorities
            {
                NewCameraOject.GetComponent<CinemachineVirtualCamera>().m_Follow = NewObjectToFollow1.transform; // we can make it also object dot find to change it is refrence
                CameraNewPriority = NewCameraOject.GetComponent<CinemachineVirtualCamera>().m_Priority - 5; // change camera periority to the new one
                virtualCamera.m_Priority = CameraNewPriority; // setting the values
                Debug.Log("first timer start 4 secconds to start zoom in");
                yield return new WaitForSeconds(4f);
                Debug.Log("first timer finish after 4 secconds now zoom in");
                NewCameraOject.GetComponent<Camera>().ZoomIn = true; // to make the new camera do the zoom in event

                Debug.Log("secconds timer start after 6 secconds  is false");
                yield return new WaitForSeconds(6f);
                Debug.Log("secconds timer finish after 6 secconds now zoom in  condition is false");
                EventChangeCamera = false;
                StartCoroutine("MyTesting");
            }
            else
            {
                CameraNewPriority = CameraOriginPriority;
                virtualCamera.m_Priority = CameraNewPriority;
                NewCameraOject.GetComponent<Camera>().ZoomIn = false;
                yield return new WaitForSeconds(3f);
                NewCameraOject.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = OrthographicSize;
                FinishZoomOutEvents.Invoke();
                StartCoroutine("CameraCharacterIdle"); // when it is falls we restart make the start coroutin working again

            }
        }
        if(CurrentEventNumber == 2)
        {
            if (EventChangeCamera)
            {
                NewCameraOject.GetComponent<CinemachineVirtualCamera>().m_Follow = NewObjectToFollow2.transform;
                yield return new WaitForSeconds(0.5f);
                CameraNewPriority = NewCameraOject.GetComponent<CinemachineVirtualCamera>().m_Priority - 5; // change camera periority to the new one
                virtualCamera.m_Priority = CameraNewPriority; // setting the values
                Debug.Log("Entering Event Number ==2 , Changing the follow and priority");
                yield return new WaitForSeconds(5f);
                EventChangeCamera = false;
                StartCoroutine("MyTesting");
            }
            else
            {
                CameraNewPriority = CameraOriginPriority;
                virtualCamera.m_Priority = CameraNewPriority;

                Debug.Log("Event Number ==2 && Exiting");
                yield return new WaitForSeconds(0.5f);
                StartCoroutine("CameraCharacterIdle"); // when it is falls we restart make the start coroutin working again
            }
        }
    }
    
    private void LateUpdate()
    {
        ZoomOutTest2();
        OriginZoomTest1();
        ZoomInTest2();
    }
    public void ZoomOutTest1()
    {
        if (ZoomOut)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(OrthographicSize, 10, 0.0125f * Time.deltaTime);
            OrthographicSize = Mathf.Lerp(OrthographicSize, 10, 0.001f);
        }
    }

    public void ZoomOutTest2() // the Main Function to do the Zoom Out
    {
        if (ZoomOut)
        {
            if (virtualCamera.m_Lens.OrthographicSize > (ZoomOutValueTarget - 0.2f))
            {
                virtualCamera.m_Lens.OrthographicSize = ZoomOutValueTarget;
            }
            else
            {
                virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, ZoomOutValueTarget, Time.deltaTime / ZoomOutSpeed);
            }
        }
    }


    public void ZoomInTest2() // the Main Function to do the Zoom In
    {
        if (ZoomIn)
        {
            if (virtualCamera.m_Lens.OrthographicSize < ZoomInValueTarget + 0.09f)
            {
                virtualCamera.m_Lens.OrthographicSize = ZoomInValueTarget;
            }
            else
            {
                virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, ZoomInValueTarget, Time.deltaTime / ZoomInSpeed);
            }
        }
    }
    public void OriginZoomTest1() // the Main Function to do the Standard origin Zoom In , whether was coming from the zoom out or the zoom in
    {
        if (OriginZoom)
        {
            if (virtualCamera.m_Lens.OrthographicSize > OriginZoomValue) // when we do it from zoom out the nmber will come from 10 to 5
            {
                virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, OriginZoomValue, Time.deltaTime / 1f);
                if (virtualCamera.m_Lens.OrthographicSize <= OriginZoomValue + 0.05f)
                {
                    virtualCamera.m_Lens.OrthographicSize = OriginZoomValue;
                }
            }
            if (virtualCamera.m_Lens.OrthographicSize < OriginZoomValue) // when we do it from zoom out the nmber will come from 10 to 5
            {
                virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, OriginZoomValue, Time.deltaTime / 1f);
                if (virtualCamera.m_Lens.OrthographicSize >= OriginZoomValue - 0.05f)
                {
                    virtualCamera.m_Lens.OrthographicSize = OriginZoomValue;
                }
            }
        }
    }
    public void EventPeriority(GameObject NewCamePriorityTarget)
    {
        if (EventChangeCamera)
        {
            CameraNewPriority = NewCamePriorityTarget.GetComponent<CinemachineVirtualCamera>().m_Priority - 5;
            virtualCamera.m_Priority = CameraNewPriority;
            if (timer22 > 0)
            {
                timer22 -= Time.deltaTime;
                Debug.Log("timer==" + timer22);
                if (timer22 <= 0)
                {
                    Debug.Log("first timer finish after 6 secconds now zoom in");
                    NewCameraOject.GetComponent<Camera>().ZoomIn = true; 
                    timer22 = 0;
                }
                }
            if (timer22 == 0)
            {
                if (timer22 == 0)
                {
                    TimerGeneral();
                    if (timer == 0)
                    {
                        Debug.Log("secconds timer finish after 10 secconds now condition is false");
                        EventChangeCamera = false;
                    }
                }
            }
        }
        else
        {
            CameraNewPriority = CameraOriginPriority;
            virtualCamera.m_Priority = CameraNewPriority;
            NewCameraOject.GetComponent<Camera>().ZoomIn = false;
            timer = 10f;
            timer22 = 6f;
            timeFinish = false;
        }
    }
    public void TimerGeneral()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            Debug.Log("timer==" + timer);
            if (timer <=  0 )
            {
                Debug.Log("lalal");
                timer = 0;
            }
        }
    }
    
}
