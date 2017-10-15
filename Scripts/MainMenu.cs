using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainMenu : MonoBehaviour {

    public Animator anim;// used for fading in

    public Button ButtonKeyboardInstr;

    public GameObject loadingScreen;

    public Image keybaordInstructionsMainmenu;
    public Image logo;
    public Image blackFadeInOUtImage;//used for fading in

    public Slider slider;

    private SettingManagerMainMenu settingManagerMainMenu;

    public Text progressText;

    public static bool exitToMenuWasPressed;// no need to reset in ResetStaticVariables()// pressed from game
    public bool fullScreenIsOn;


    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        anim = GetComponent<Animator>();
        AudioListener.pause = false;

        //if fullscreen on splash screen is toggled on, then set fullscreen in the json file to true else to false using bool fullScreenIsOn
        if(Screen.fullScreen)
        {
            fullScreenIsOn = true;
        }else
        {
            fullScreenIsOn = false;
        }

        //if gamesetting.json doesnt exist, then create one with the settings below
        if (File.Exists(Application.persistentDataPath + "/gamesetting.json"))
        {
            Debug.Log("YES -> File exists");
        }
        else
        {
            Debug.Log("NO -> created json file");
            settingManagerMainMenu = GetComponent<SettingManagerMainMenu>();

            settingManagerMainMenu.gameSettings.fullscreen = Screen.fullScreen = fullScreenIsOn;

            QualitySettings.masterTextureLimit = settingManagerMainMenu.gameSettings.textureQuality = 2;//0-high,1-medium,2-low
            QualitySettings.antiAliasing = settingManagerMainMenu.gameSettings.antialiasing = 1;//2-high,1-medium,0-low
            QualitySettings.vSyncCount = settingManagerMainMenu.gameSettings.vSync = 0;//2-high,1-medium,0-low

            float _volumeMusic = settingManagerMainMenu.gameSettings.musicVolume = 1;
            settingManagerMainMenu.musicSourceMainMenu.Volume(_volumeMusic);

            float _volumeSFX = settingManagerMainMenu.gameSettings.sfxVolume = 1;
            settingManagerMainMenu.sfxSourceMainMenu.Volume(_volumeSFX);

            AudioListener.volume = settingManagerMainMenu.gameSettings.masterVolume = 1;

            settingManagerMainMenu.SaveSettingsMainMenu();// creates one

        }
    }



    public void PlayGame(string newGame)
    {
        FindObjectOfType<MusicManager>().Stop("intro");
        FindObjectOfType<MusicManager>().Play("song");
        FindObjectOfType<SFX_Manager>().Play("buttonSound");
        

        StartCoroutine(LoadAsynchronously1(newGame));        
        //SceneManager.LoadScene(newGame);
    }

    public void GameSettings(string settings)
    {
        FindObjectOfType<SFX_Manager>().Play("buttonSound");
        StartCoroutine(LoadAsynchronously2(settings));
        //SceneManager.LoadScene(settings);
    }

    public void KeyboardIntructionsMainmenu()
    {
        FindObjectOfType<SFX_Manager>().Play("buttonSound");
        keybaordInstructionsMainmenu.gameObject.SetActive(true);
        logo.gameObject.SetActive(false);
    }
    public void Close_KeyboardIntructionsMainmenu()
    {
       FindObjectOfType<SFX_Manager>().Play("buttonSound");
       if(keybaordInstructionsMainmenu.gameObject.activeInHierarchy == true)
        {
            logo.gameObject.SetActive(true);
            keybaordInstructionsMainmenu.gameObject.SetActive(false);
        }        
    }
    public void ExitGame()
    {
        FindObjectOfType<SFX_Manager>().Play("buttonSound");
        Application.Quit();
    }


    IEnumerator LoadAsynchronously2(string settings)
    {
        //change scene and load the bar
        AsyncOperation operation = SceneManager.LoadSceneAsync(settings);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            //Debug.Log(progress);
            slider.value = progress;
            progressText.text = (Mathf.RoundToInt(progress)) * 100f + "%";
            yield return null;
        }
    }

    IEnumerator LoadAsynchronously1(string newGame)
    {
        
        //change scene and load the bar
        AsyncOperation operation = SceneManager.LoadSceneAsync(newGame);

        loadingScreen.SetActive(true);


        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            //Debug.Log(progress);
            slider.value = progress;
            progressText.text = (Mathf.RoundToInt(progress)) * 100f + "%";
            yield return null;
        }                   
    }


    void Update()
    {
        if (exitToMenuWasPressed)
        {            
            blackFadeInOUtImage.gameObject.SetActive(false);
            //exitToMenuWasPressed = false;
        }
    }    
}
