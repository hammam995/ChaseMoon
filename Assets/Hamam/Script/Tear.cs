using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tear : ObjectInteractable
{
    // Start is called before the first frame update

    public override void Use()
    {
        Action.Invoke();
        // Destroy(this.gameObject);
        //Destroy();
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
