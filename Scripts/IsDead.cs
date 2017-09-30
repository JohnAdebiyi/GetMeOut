using UnityEngine;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class IsDead : MonoBehaviour {

    public Transform Player;
    public Image bar;

    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;

    //restarts the level
    public void RestartGame()
    {
        FindObjectOfType<SFX_Manager>().Play("buttonSound");
        //Process.Start(Application.dataPath + "/../Get_Me_Out_x86.exe");
        //Application.Quit();
        StartCoroutine(LoadAsynchronously());
        ResetStaticVariables();

        FindObjectOfType<MusicManager>().Stop("song2");
        FindObjectOfType<MusicManager>().Stop("intro");
        FindObjectOfType<MusicManager>().Play("song");

        FindObjectOfType<SFX_Manager>().Stop("4min");
        FindObjectOfType<SFX_Manager>().Stop("3min");
        FindObjectOfType<SFX_Manager>().Stop("2min");
        FindObjectOfType<SFX_Manager>().Stop("1min");
        FindObjectOfType<SFX_Manager>().Stop("10sec");

        AudioListener.pause = false;
    }

    public void ExitToMainMenu()
    {
        MainMenu.exitToMenuWasPressed = true;
        FindObjectOfType<SFX_Manager>().Play("buttonSound");
        //Process.Start(Application.dataPath + "/../Get_Me_Out_x86.exe");
        //Application.Quit();
        StartCoroutine(LoadAsynchronously2());
        ResetStaticVariables();
        
        FindObjectOfType<MusicManager>().Stop("song2");
        FindObjectOfType<MusicManager>().Play("intro");
        FindObjectOfType<MusicManager>().Stop("song");


        FindObjectOfType<SFX_Manager>().Stop("4min");
        FindObjectOfType<SFX_Manager>().Stop("3min");
        FindObjectOfType<SFX_Manager>().Stop("2min");
        FindObjectOfType<SFX_Manager>().Stop("1min");
        FindObjectOfType<SFX_Manager>().Stop("10sec");
        AudioListener.pause = false;
    }

    public void ExitGame()
    {
        FindObjectOfType<SFX_Manager>().Play("buttonSound");
        Application.Quit();
    }

    void ResetStaticVariables()
    {
        DoorBathroomScript.keyCard_To_Bathroom = false;
        DoorBedroomScript.keyCardBedroom = false;
        DoorKitchenScript.keycardIsActiv = false;
        DoorKitchenScript.keyCard_To_Laptop = false;

        DoorOutsideScript.weaponObtained = false;

        EnemyAIScript.isOutside = false;
        EnemyHealthScript.enemiesDeadCounter = 5;//changed private to public

        Keypad_BedRoom_SAFE_Script.enemiesAreDead = false;

        Laptop_GameScript.putOffPanel_Game1 = false;
        Laptop_GameScript_2.putOffPanel_Game2 = false;
        Laptop_GameScript_2.keyCard_To_Laptop = false;
        Laptop_GameScript_3.keyCard_To_Laptop = false;

        WeaponSafe_CorridorScript.keycardIsActiv = false;
        WeaponSafe_CorridorScript.keyCard_To_Laptop = false;

        Terminal_1Script.keyCardTerminal1 = false;

        Timer.countKeycards = 0;

        Objectiv2.objectiv2_Started = false;

        IsPause.weapon_instructionStarted = false;
    }

    IEnumerator LoadAsynchronously()
    {

        //change scene and load the bar
        AsyncOperation operation = SceneManager.LoadSceneAsync("Game1");

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

    IEnumerator LoadAsynchronously2()
    {

        //change scene and load the bar
        AsyncOperation operation = SceneManager.LoadSceneAsync("MainMenu");

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
}
