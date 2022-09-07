using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] private Text textKey;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void EnableTextInteraction(bool enable)
    {
        textKey.gameObject.SetActive(enable);
    }
}