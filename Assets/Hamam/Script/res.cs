using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class res : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;
    void OnEnable()
    {
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange();});
        resolutions = Screen.resolutions;
        foreach(Resolution resolution in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        } 
    }
    public void SetResulution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void OnResolutionChange()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
