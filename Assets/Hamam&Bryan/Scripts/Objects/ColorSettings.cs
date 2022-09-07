using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorSettings : MonoBehaviour
{
    [Header("Volume Profile")]
    [SerializeField] private Volume gameVolume;
    [Header("Transition Settings")]
    [SerializeField] private float speed;
    [Header("Curves of Colors")]
    [SerializeField] private TextureCurve normalCurve;
    [SerializeField] private TextureCurve inverseCurve;
    [Header("Unity Events")]
    [SerializeField] private UnityEvent OnStartActions;
    [SerializeField] private UnityEvent OnFinishActions;

    bool canInverse;
    private bool isInverse;
    private ColorCurves gameColors;
    private float time;
    private float currentValue0;
    private float currentValue1;
    private float currentInTangent;
    private float currentOutTangent;
    private void Awake()
    {
        isInverse = false;
        time = 0;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Change Weather"))
            canInverse = true;
    }
    void FixedUpdate()
    {
        if(canInverse)
        {
            InverseColors();
        } else
        {
            time = 0;
        }
    }
    private void InverseColors()
    {
        TextureCurve currentCurve;
        gameColors = (ColorCurves)gameVolume.profile.components[0];
        currentCurve = gameColors.master.value; 
        if (!isInverse)
        {
            LerpCurves(currentCurve, inverseCurve, time * speed);
        } else
        {
            LerpCurves(currentCurve, normalCurve, time * speed);
        }
        time += Time.fixedDeltaTime;
    }
    private void LerpCurves(TextureCurve from, TextureCurve to, float t)
    {
        float newValue0, newValue1, newOutTangent, newInTangent;
        if(t == 0)
        {
            currentValue0 = from[0].value;
            currentValue1 = from[1].value;
            currentOutTangent = from[0].outTangent;
            currentInTangent = from[1].inTangent;
            OnStartActions.Invoke();
        }
        newValue0 = Mathf.Lerp(currentValue0, to[0].value, t);
        newValue1 = Mathf.Lerp(currentValue1, to[1].value, t);
        newOutTangent = Mathf.Lerp(currentOutTangent, to[0].outTangent, t);
        newInTangent = Mathf.Lerp(currentInTangent, to[1].inTangent, t);
        from.MoveKey(0, new Keyframe(0, newValue0, 0f, newOutTangent));
        from.MoveKey(1, new Keyframe(1, newValue1, newInTangent, 0f));
        if(Mathf.Abs(newValue0 - to[0].value) < 0.001f && Mathf.Abs(newValue1 - to[1].value) < 0.001f)
        {
            Debug.Log("Colors Changed");
            from.MoveKey(0, new Keyframe(0, to[0].value, 0f, to[0].outTangent));
            from.MoveKey(1, new Keyframe(1, to[1].value, to[1].inTangent, 0f));
            isInverse = !isInverse;
            canInverse = false;
            OnFinishActions.Invoke();
        }
    }
}
