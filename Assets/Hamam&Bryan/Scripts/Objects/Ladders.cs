using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladders : MonoBehaviour
{
    Collider2D myCollider;
    private void Awake() 
    {
        myCollider = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            MainCharacterFSM mcFsm = collision.GetComponent<MainCharacterFSM>();
            mcFsm.CheckClimbOnAir.SetCanClimb(true);
            mcFsm.CheckClimbOnFloor.SetCanClimb(true);
            mcFsm.CheckClimbOnFloor.SetLadderCollider(myCollider);
            mcFsm.CheckClimbOnAir.SetLadderCollider(myCollider);
            mcFsm.Climb.SetLadderCollider(myCollider);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            MainCharacterFSM mcFsm = collision.GetComponent<MainCharacterFSM>();
            mcFsm.CheckClimbOnAir.SetCanClimb(false);
            mcFsm.CheckClimbOnFloor.SetCanClimb(false);
            mcFsm.CheckClimbOnFloor.SetLadderCollider(null);
            mcFsm.CheckClimbOnAir.SetLadderCollider(null);
            mcFsm.Climb.SetLadderCollider(null);
            if (mcFsm.GetOnAir())
                mcFsm.Climb.FinishEvent("ToOnAir");
        }
    }
}
