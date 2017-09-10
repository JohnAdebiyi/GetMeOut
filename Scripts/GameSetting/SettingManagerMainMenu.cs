using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public class SettingManagerMainMenu : MonoBehaviour {

    public Toggle fullscreenToggleMainMenu;
    public Dropdown resolutionDropdownMainMenu;
    public Dropdown textureQualityDropdownMainMenu;
    public Dropdown antialiasingDropdownMainMenu;
    public Dropdown vSyncDropdownMainMenu;
    //public Slider musicVolumeSliderMainMenu;
    //public Slider sfxVolumeSliderMainMenu;
    public Slider masterVolumeSliderMainMenu;
    public Button applyButtonMainMenu;
    public Button closeClickMainMenu;

    //public MusicManager musicSourceMainMenu;
    //public SFX_Manager sfxSourceMainMenu;
    public Resolution[] resolutionsMainMenu;
    public GameSettings gameSettings;

    public Image blackFadeInOUtImage;//used for fading in
    public Animator anim;// used for fading in

    private string Scene_MainMenu = "MainMenu";

    public static MusicManager instance;


    void OnEnable()
    {
        //musicSourceMainMenu = FindObjectOfType<MusicManager>();
        //sfxSourceMainMenu = FindObjectOfType<SFX_Manager>();

        gameSettings = new GameSettings();

        fullscreenToggleMainMenu.onValueChanged.AddListener(delegate { OnFullscreenToggleMainMenu(); });
        resolutionDropdownMainMenu.onValueChanged.AddListener(delegate { OnResolutionChangeMainMenu(); });
        textureQualityDropdownMainMenu.onValueChanged.AddListener(delegate { OnTextureQualityChangeMainMenu(); });
        antialiasingDropdownMainMenu.onValueChanged.AddListener(delegate { OnAntialiasingChangeMainMenu(); });
        vSyncDropdownMainMenu.onValueChanged.AddListener(delegate { OnVSyncChangeMainMenu(); });
        //musicVolumeSliderMainMenu.onValueChanged.AddListener(delegate { OnMusicVolumeMainMenu(); });
        //sfxVolumeSliderMainMenu.onValueChanged.AddListener(delegate { OnSfxVolumeMainMenu(); });
        masterVolumeSliderMainMenu.onValueChanged.AddListener(delegate { OnMasterVolumeMainMenu(); });
        applyButtonMainMenu.onClick.AddListener(delegate { OnApplyButtonClickMainMenu(); });
        closeClickMainMenu.onClick.AddListener(delegate { OnCloseClickMainMenu(); });

        resolutionsMainMenu = Screen.resolutions;
        foreach (Resolution resolution in resolutionsMainMenu)
        {
            resolutionDropdownMainMenu.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }

        LoadSettings();
    }



    public void OnFullscreenToggleMainMenu()
    {
        gameSettings.fullscreen = Screen.fullScreen = fullscreenToggleMainMenu.isOn;
    }

    public void OnResolutionChangeMainMenu()
    {
        Screen.SetResolution(resolutionsMainMenu[resolutionDropdownMainMenu.value].width, resolutionsMainMenu[resolutionDropdownMainMenu.value].height, Screen.fullScreen);
        gameSettings.resolutionIndex = resolutionDropdownMainMenu.value;
    }

    public void OnTextureQualityChangeMainMenu()
    {
        QualitySettings.masterTextureLimit = gameSettings.textureQuality = textureQualityDropdownMainMenu.value;
        
    }

    public void OnAntialiasingChangeMainMenu()
    {
        QualitySettings.antiAliasing = gameSettings.antialiasing = (int)Mathf.Pow(2, antialiasingDropdownMainMenu.value); 
    }

    public void OnVSyncChangeMainMenu()
    {
        QualitySettings.vSyncCount = gameSettings.vSync = vSyncDropdownMainMenu.value;
    }
    /**
    public void OnMusicVolumeMainMenu()
    {
        musicSourceMainMenu.GetComponent<AudioSource>().volume = gameSettings.musicVolume = musicVolumeSliderMainMenu.value;

    }

    public void OnSfxVolumeMainMenu()
    {
        sfxSourceMainMenu.GetComponent<AudioSource>().volume = gameSettings.sfxVolume = sfxVolumeSliderMainMenu.value;
    }
    **/
    public void OnMasterVolumeMainMenu()
    {
        AudioListener.volume = gameSettings.masterVolume = masterVolumeSliderMainMenu.value;
    }

    public void OnApplyButtonClickMainMenu()
    {
        FindObjectOfType<SFX_Manager>().Play("buttonSound");
        SceneManager.LoadScene(Scene_MainMenu);
        SaveSettingsMainMenu();
    }

    public void OnCloseClickMainMenu()
    {
        FindObjectOfType<SFX_Manager>().Play("buttonSound");
        SceneManager.LoadScene(Scene_MainMenu);
    }


    public void SaveSettingsMainMenu()
    {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesetting.json", jsonData);
    }

    public void LoadSettings()
    {
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesetting.json"));

        masterVolumeSliderMainMenu.value = gameSettings.masterVolume;
       //musicVolumeSliderMainMenu.value = gameSettings.musicVolume;
        //sfxVolumeSliderMainMenu.value = gameSettings.sfxVolume;
        antialiasingDropdownMainMenu.value = gameSettings.antialiasing;
        vSyncDropdownMainMenu.value = gameSettings.vSync;
        textureQualityDropdownMainMenu.value = gameSettings.textureQuality;
        resolutionDropdownMainMenu.value = gameSettings.resolutionIndex;
        fullscreenToggleMainMenu.isOn = gameSettings.fullscreen;
        Screen.fullScreen = gameSettings.fullscreen;

        resolutionDropdownMainMenu.RefreshShownValue();
    }
}
