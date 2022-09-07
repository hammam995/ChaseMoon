using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resolution_DropBox : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions(); // first we clear the values of the dropdown we have , then we put the valuues we want
        List<string> options = new List<string>();  // is the list of string we will put the values of the resoulution , it have all the options we want to put , this for whole the list
        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "X" + resolutions[i].height + " (" + resolutions[i].refreshRate + ")"; // option , is astring value , every option will have this value , this inside the list every eleemnt will have this value
            options.Add(option); // then the list will add to her the every single value
            if(resolutions[i].width==Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
                //if both match up we save it to our current resolution index
            {
                currentResolutionIndex = i;
            }
        }
        // to update the values on the dropbox to be shown
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    void Update()
    {
        
    }
}
