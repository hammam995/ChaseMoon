using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events; // or coroutine


public class Ui_Manager : MonoBehaviour
{

    [Header("Ink Bar Variables")]
    [Range(0, 100)]
    public float CurrentBarValue;
    public float MaxBarValue = 100;
    public float lerpspeed;
    public float updatespeedpersecconds ;
    public float Elapsed ; // elapsed time , that contain and have the time.delta time
    public float Prechangepct ; // to save the value of the bar before we change it , we can put fill amount and it will work well withh the lerp not problem , Extra
    public GameObject ObjectInkBarPercentage;
    public Image UiInkBar;
    private Text InkBarPercentage; // Text have the percentage value when it is changing
    public float ValueMultipliedBtMinusOne;
    private bool isPaused = false;

    [Header("Tear Variables")]
    public GameObject ObjectTearNumber;
    public Text TextTearNumber;
    [Range(0, 100)]
    public float TearNumber;

    public UnityEvent EnterPause;
    public UnityEvent ExitPause;

    public void SetIsPaused(bool isPaused)
    {
        this.isPaused = isPaused;
    }
    void Start()
    {
        SettingVariablesValues();
    }
    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            Debug.Log(isPaused);
            if (!isPaused)
                EnterPause.Invoke();
            else
                ExitPause.Invoke();
            isPaused = !isPaused;
        }
    }

    public void SettingVariablesValues() // at the start
    {
        UiInkBar.fillAmount = CurrentBarValue / MaxBarValue;
        if(InkBarPercentage == null)
        {
            ObjectInkBarPercentage = GameObject.Find("inkPercentage");
            InkBarPercentage = ObjectInkBarPercentage.GetComponent<Text>();
            InkBarPercentage.text = "%" + (UiInkBar.fillAmount * 100).ToString();
        }
        InkBarPercentage.text = "%" + (UiInkBar.fillAmount * 100).ToString();
        if (ObjectTearNumber == null)
        {
            ObjectTearNumber = GameObject.Find("TearsNumber");
           TextTearNumber = ObjectTearNumber.GetComponent<Text>();
            TextTearNumber.text = TearNumber.ToString();
        }
    }
   
    public void UpdatingTearValue(float addpoints) // find better solution by limitide the boundries
    {
         float inverspoints = (addpoints)*-1;
        ValueMultipliedBtMinusOne = inverspoints;
        if (addpoints < 0) // minus
        {
            if (TearNumber >= (addpoints) * -1)
            {
                TearNumber += addpoints;
                TextTearNumber.text = TearNumber.ToString();
                Debug.Log("TearNumber >= inverspoints");
            }

            else
            {
                TearNumber = TearNumber;
                TextTearNumber.text = TearNumber.ToString();
                Debug.Log("is falt the amount remaining is few"); // in case it was taking more than what we have
            }
        }
        else
        {
            TearNumber += addpoints;
            TextTearNumber.text = TearNumber.ToString();
            Debug.Log("is positive value added");
        }

        /* if(addpoints < 0 && TearNumber > inverspoints) // we do that because the range will be zero
         {
             TearNumber += addpoints;
             TearNumber = Mathf.Clamp(TearNumber, 0, 100);
             TextTearNumber.text = TearNumber.ToString();
         }
         else
         {
             TearNumber += addpoints;
             TearNumber = Mathf.Clamp(TearNumber, 0, 100);
             TextTearNumber.text = TearNumber.ToString();
         }*/
        /*if(TearNumber > addpoints)
            {
                TearNumber += addpoints;
                TearNumber = Mathf.Clamp(TearNumber, 0, 100);
                TextTearNumber.text = TearNumber.ToString();
            }
            else
            {

            }*/


        // TearNumber = Mathf.Clamp(TearNumber, 0, 100);
        //solution one 1
        /* if(addpoints < 0)
          {
              if(TearNumber <=0)
              {
                  TearNumber = 0;
                  TextTearNumber.text = TearNumber.ToString();
                  Debug.Log("is minus -1 , so reset it to zero");
              }
              else
              {
                  TearNumber += addpoints;
                  TextTearNumber.text = TearNumber.ToString();
              }
          }
          else
          {
              TearNumber += addpoints;
              TextTearNumber.text = TearNumber.ToString();
          }*/
    }

    public void UpdatingVariablesValues(float addedpoints) // first event for the ink
    {
            CurrentBarValue += addedpoints;
            StartCoroutine(UpdateBarValue(CurrentBarValue));
    }
    public IEnumerator UpdateBarValue(float NewCurrnetValue) // then automatically is called , if it was positive , if it was negative
    {
        // trying to know if it is the same if we put the newcurrent value rather than the current bar value
        Prechangepct = UiInkBar.fillAmount;
        Elapsed = 0; // at the begenning it will be zero
        while(Elapsed< updatespeedpersecconds)
        {
            Elapsed += Time.deltaTime;
            UiInkBar.fillAmount = Mathf.Lerp(Prechangepct, CurrentBarValue/ MaxBarValue, Elapsed / updatespeedpersecconds);
            InkBarPercentage.text = "%" + (UiInkBar.fillAmount * 100).ToString();
            yield return null;
        }
          UiInkBar.fillAmount = CurrentBarValue / MaxBarValue;
         //UiInkBar.fillAmount = CurrentBarValue / MaxBarValue;
        // UiInkBar.fillAmount = Mathf.Lerp(UiInkBar.fillAmount, CurrentBarValue / MaxBarValue, Time.deltaTime * 0.3f);
        //UiInkBar.fillAmount = Mathf.Lerp(UiInkBar.fillAmount, CurrentBarValue / MaxBarValue, lerpspeed);
    }
}
