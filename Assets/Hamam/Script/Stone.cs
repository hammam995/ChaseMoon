using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : ObjectInteractable
{
    public GameObject StoneObjectPanel;
    public GameObject StoneObjectText;

    // Start is called before the first frame update

    public override void Use()
    {
        Action.Invoke();
    }

    public void DestroyStonePanel()
    {
        Destroy(StoneObjectPanel);
    }
    public void DestroyStoneText()
    {
        Destroy(StoneObjectText);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
