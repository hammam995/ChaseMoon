using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pivot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool upOrDown; // Arriba es true y abajo es false
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private float offsetX;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("PIVOT CLICKED!");
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            float limitMin;
            float limitMax;
            MainCharacterFSM mc = null;
            foreach (var mc_aux in FindObjectsOfType<MainCharacterFSM>())
            {
                mc = mc_aux.GetCharacterUpOrDown() == upOrDown ? mc_aux : mc;
            }
            limitMin = transform.position.x - minDistance;
            limitMax = transform.position.x - maxDistance;
            if(!mc.ThrowArm.GetInTransition() && mc.transform.position.x > limitMax && mc.transform.position.x < limitMin && mc.onControl && !mc.GetOtherCharacter().onControl)
            {
                Debug.DrawRay(mc.transform.position, (transform.position - mc.transform.position).normalized * Vector3.Distance(mc.transform.position, transform.position), Color.red, 0.1f);
                mc.ThrowArm.SetStartParabola(mc.transform.position);
                mc.ThrowArm.SetPivotPosition(transform.position);
                mc.ThrowArm.SetEndParabola(new Vector2(transform.position.x + offsetX, mc.transform.position.y));
                mc.ThrowArm.SetHeightParabola(transform.position.y - mc.transform.position.y);
                mc.GetMovementState().SendEvent("ToThrowArm");
            }
        }
    }
}
