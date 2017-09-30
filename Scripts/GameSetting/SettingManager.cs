using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class SettingManager : MonoBehaviour {
    public Toggle fullscreenToggle;
    public Dropdown resolutionDropdown;
    public Dropdown textureQualityDropdown;
    public Dropdown antialiasingDropdown;
    public Dropdown vSyncDropdown;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider masterVolumeSlider;
    public Button applyButton;
    public Button closeClick;

    public MusicManager musicSource;
    public SFX_Manager sfxSource;
    public Resolution[] resolutions;
    public GameSettings gameSettings;

    public GameObject options_panel;
    public GameObject onPause_Panel;

    void OnEnable()
    {
        musicSource = FindObjectOfType<MusicManager>();
        sfxSource = FindObjectOfType<SFX_Manager>();
        gameSettings = new GameSettings();

        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        textureQualityDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        antialiasingDropdown.onValueChanged.AddListener(delegate { OnAntialiasingChange(); });
        vSyncDropdown.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolume(); });
        sfxVolumeSlider.onValueChanged.AddListener(delegate { OnSfxVolume(); });
        masterVolumeSlider.onValueChanged.AddListener(delegate { OnMasterVolume(); });
        applyButton.onClick.AddListener(delegate { OnApplyButtonClick(); });
        closeClick.onClick.AddListener(delegate { OnCloseClick(); });

        resolutions = Screen.resolutions;
        foreach(Resolution resolution in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }

        LoadSettings();

    }

    public void OnFullscreenToggle()
    {

        gameSettings.fullscreen = Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void OnResolutionChange()
    {

        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
        gameSettings.resolutionIndex = resolutionDropdown.value;
    }

    public void OnTextureQualityChange()
    {

        QualitySettings.masterTextureLimit = gameSettings.textureQuality = textureQualityDropdown.value;        
    }

    public void OnAntialiasingChange()
    {
   
        QualitySettings.antiAliasing = gameSettings.antialiasing = (int)Mathf.Pow(2, antialiasingDropdown.value);
    }

    public void OnVSyncChange()
    {

        QualitySettings.vSyncCount = gameSettings.vSync = vSyncDropdown.value;
    }
    
    public void OnMusicVolume()
    {

        float _volume = gameSettings.musicVolume = musicVolumeSlider.value;
        musicSource.Volume(_volume);

    }

    public void OnSfxVolume()
    {
        FindObjectOfType<SFX_Manager>().Play("buttonSound");
        float _volume = gameSettings.sfxVolume = sfxVolumeSlider.value;
        sfxSource.Volume(_volume);
    }
    
    public void OnMasterVolume()
    {
        AudioListener.volume = gameSettings.masterVolume = masterVolumeSlider.value;
    }

    public void OnApplyButtonClick()
    {
        FindObjectOfType<SFX_Manager>().Play("buttonSound");
        SaveSettings();
        FindObjectOfType<MusicManager>().Stop("song_Settings");
    }
    public void OnCloseClick()
    {
        FindObjectOfType<SFX_Manager>().Play("buttonSound");
        options_panel.SetActive(false);
        onPause_Panel.SetActive(true);
        FindObjectOfType<MusicManager>().Stop("song_Settings");
    }

    public void SaveSettings()
    {        
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesetting.json", jsonData);
        options_panel.SetActive(false);
        onPause_Panel.SetActive(true); 
    }

    public void LoadSettings()
    {
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesetting.json"));

        masterVolumeSlider.value = gameSettings.masterVolume;
        sfxVolumeSlider.value = gameSettings.sfxVolume;
        musicVolumeSlider.value = gameSettings.musicVolume;
        antialiasingDropdown.value = gameSettings.antialiasing;
        vSyncDropdown.value = gameSettings.vSync;
        textureQualityDropdown.value = gameSettings.textureQuality;
        resolutionDropdown.value = gameSettings.resolutionIndex;
        fullscreenToggle.isOn = gameSettings.fullscreen;
        Screen.fullScreen = gameSettings.fullscreen;

        resolutionDropdown.RefreshShownValue();
    }
}
