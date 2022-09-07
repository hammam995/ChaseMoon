using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Bottle_Of_Ink : ObjectInteractable 
{
    // other solution
    public float InkPoint;
    public bool Bottletaken;
    public bool CharacterCollideBottle;
    public Ui_Manager mymanager;
    public GameObject mymanagerObject;
    UnityEvent ink = new UnityEvent();

    //    [SerializeField] private UnityEvent myevent;

    /*

    void Start()
    {
       if(mymanagerObject== null)
        {

         mymanagerObject = GameObject.Find("Ui_Manager");
         mymanager = mymanagerObject.GetComponent<Ui_Manager>();

        }

        ink.AddListener(Use);

        //mymanager.myevent.AddListener(Use);
        
    }

    void Update()
    {
        if (CharacterCollideBottle && Input.GetButtonDown("Interact"))
        {
            Use();
        }
    }

    
    */

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CharacterCollideBottle = true;
            // mymanager.myevent.AddListener(Use);
            // Use();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CharacterCollideBottle = false;
        }
    }*/

        /*
    public void Use()
    {
        mymanager.UpdatingVariablesValues(InkPoint);
        //mymanager.myevent.AddListener();
        //mymanager.myevent.Invoke();
        // mymanager.UpdatingVariablesValues(InkPoint);
        Destroy(this.gameObject);
    }*/
}
