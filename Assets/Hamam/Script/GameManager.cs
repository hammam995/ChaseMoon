using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private AudioSource audioSrc;
    private float musicVolume = 0.5f;
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;
    public Text entmsg;
    public string NextLevelName;
    AsyncOperation loadAsync;
    public Slider slide;
    // we check later if the object is null we dont do it , to avoid the error
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        if(entmsg != null)
        {
            entmsg.gameObject.SetActive(false);
        }
        // Assign Audio Source component to control it
        if (audioSrc != null)
        {
            audioSrc = GetComponent<AudioSource>();
        }
        if(resolutionDropdown != null)
        {
            Start_Resolution();
        }
        if (string.IsNullOrEmpty(NextLevelName))
        {
            // if the string of the next level is empty or null we dont do something becuase we will not gonna use it
        }
        else
        {
            StartCoroutine(nextsc2()); // if it is have value then we do what we want
        }
    }
    void Update()
    {
        if(audioSrc != null)
        {
            // Setting volume option of Audio Source to be equal to musicVolume
            audioSrc.volume = musicVolume;
        }
    }

    public void NextScene(string Game)
    {
        SceneManager.LoadScene(Game);
    }
   
    public void ChangeRes(Dropdown drop)
    {
        switch (drop.value)
        {
            case 0:
                Screen.SetResolution(720, 480, true);
                //Screen.SetResolution(1024, 768, true);
                break;
            case 1:
                Screen.SetResolution(854, 480, true);
                // Screen.SetResolution(1280, 720, true);
                break;

            case 2:
                Screen.SetResolution(1280, 720, true);
                break;
            case 3:
                Screen.SetResolution(1920, 1080, true);
                break;
        }
    }
    public void Start_Resolution ()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions(); // first we clear the values of the dropdown we have , then we put the valuues we want
        List<string> options = new List<string>();  // is the list of string we will put the values of the resoulution , it have all the options we want to put , this for whole the list
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "X" + resolutions[i].height + " (" + resolutions[i].refreshRate + ")"; // option , is astring value , every option will have this value , this inside the list every eleemnt will have this value
            options.Add(option); // then the list will add to her the every single value
            if (resolutions[i].width == Screen.currentResolution.width &&
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
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void FullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    public void changeVolume(Slider slider)
    {

        AudioListener.volume = slider.value;
    }
    // Method that is called by slider game object
    // This method takes vol value passed by slider
    // and sets it as musicValue
    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }
    public void CloseApp()
    {
        Application.Quit();
    }


    IEnumerator nextsc2()
    {
        if (string.IsNullOrEmpty(NextLevelName))
        {
        }
        else
        {
            loadAsync = SceneManager.LoadSceneAsync(NextLevelName);
            loadAsync.allowSceneActivation = false;
            while (loadAsync.progress < 0.9f)
            {
                slide.value = loadAsync.progress;
                yield return null;
            }
            yield return new WaitForSeconds(3);
            entmsg.gameObject.SetActive(true);
            //  slide.value = 1;
            slide.value = loadAsync.progress;
            slide.value = 1;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space)); // when the loading  finish you must press space button to go to the next scene or the stage everything
            loadAsync.allowSceneActivation = true;
        }
    }
}
