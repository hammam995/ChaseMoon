using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleOfInk : ObjectInteractable
{
    public Sprite EmptyInk;
    public SpriteRenderer FatherRender;

    // main Solution
    public override void Use()
    {
        Action.Invoke();
    }
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
    public void ChangeSpritePicture() // if we put it private is give error , how it make it work when it is privat null refrence
    {

        EmptyInk = Resources.Load<Sprite>("Ink Plant Empty");
        gameObject.GetComponentInParent<SpriteRenderer>().sprite = EmptyInk;
        Destroy();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
