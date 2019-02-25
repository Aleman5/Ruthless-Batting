using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

    [SerializeField] private Dropdown resolutionDropdown;

    Resolution[] resolutions;

    [SerializeField] Slider slider;
    [SerializeField] Slider slidersfx;
    [SerializeField] Toggle toggle1;
    [SerializeField] Toggle toggle2;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            slider.value = PlayerPrefs.GetFloat("volume");
            AkSoundEngine.SetRTPCValue("volumen_musica", PlayerPrefs.GetFloat("volume"));   
        }

        if (PlayerPrefs.HasKey("volumesfx"))
        {
            slidersfx.value = PlayerPrefs.GetFloat("volumesfx");
            AkSoundEngine.SetRTPCValue("volumen_sfx", PlayerPrefs.GetFloat("volumesfx"));
        }

       /* if (PlayerPrefs.HasKey("music"))
        {
            if (PlayerPrefs.GetInt("music") == 1)
            {
                AudioManager.Instance.ChangeSoundState(true);
                toggle1.isOn = true;
            }
            else
            {
                AudioManager.Instance.ChangeSoundState(false);
                toggle1.isOn = false;
            }
        }*/

        if (PlayerPrefs.HasKey("screen"))
        {
            if (PlayerPrefs.GetInt("screen") == 1)
            {
                Screen.fullScreen = true;
                toggle2.isOn = true;
            }
            else
            {
                Screen.fullScreen = false;
                toggle2.isOn = false;
            }
        }

        PlayerPrefs.SetInt("restarted", 0);
    }

    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        AkSoundEngine.SetRTPCValue("volumen_musica", volume);
    }

    public void SetVolumeSFX(float volume)
    {
        PlayerPrefs.SetFloat("volumesfx", volume);
        AkSoundEngine.SetRTPCValue("volumen_sfx", volume);
    }

    /*public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }*/

    /*public void SetMusic(bool isActive)
    {
        AudioManager.Instance.ChangeSoundState(isActive);
        if (isActive)
            PlayerPrefs.SetInt("music", 1);
        else
            PlayerPrefs.SetInt("music", 0);
    }*/

    public void SetFullScreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        if (isFullscreen)
            PlayerPrefs.SetInt("screen", 1);
        else
            PlayerPrefs.SetInt("screen", 0);
    }

    public void SavePlayerPrefs()
    {
        PlayerPrefs.Save();
    }
}
