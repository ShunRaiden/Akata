using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResolutionManeger : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;

    public Toggle fullscreenToggle;

    void Start()
    {
        List<string> options = new List<string> { "1920 x 1080", "1280 x 720", "720 x 480" };

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);

        fullscreenToggle.isOn = Screen.fullScreen;

    }

    public void SetResolution(int resolutionIndex)
    {
        string[] resolutionStr = resolutionDropdown.options[resolutionIndex].text.Split('x');
        int width = int.Parse(resolutionStr[0].Trim());
        int height = int.Parse(resolutionStr[1].Trim());
        Screen.SetResolution(width, height, false); // Set fullscreen to false
        fullscreenToggle.isOn = false; // Make sure toggle reflects the change
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

}
