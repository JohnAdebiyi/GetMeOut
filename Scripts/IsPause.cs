using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class IsPause : MonoBehaviour
{

    public GameObject continueGaming;
    public GameObject options_Panel;
    public GameObject onPause_Panel;
    public GameObject isDead_Panel;
    public Image pauseBackground;
    public Transform Player;


    public Image keybaordInstructions;
    public Button keyboardInstructionsExit;


    public Image objectiv_keycards; //tutorials
    public Image objectiv_slots; //tutorials
    public Image objectiv_laptop; //tutorials
    public Image objectiv_help; //tutorials
    public Button closeObjectiv;//tutorials
    public Button nextObjectiv;//tutorials
    public Button prevObjectiv;//tutorials

    public Image objectiv_pointers; //tutorials2
    public Image objectiv_map; //tutorials2
    public Image objectiv_help2; //tutorials2
    public Button closeObjectiv2;//tutorials2
    public Button nextObjectiv2;//tutorials2
    public Button prevObjectiv2;//tutorials
    public Image objectivBackground;


    public Image infoWeapon;
    public Image introGunBackground;
    public Button closeInfoWeapon;

    public static bool escape_buttonsEnabled = true;
    public static bool weapon_instructionStarted;

    void Start()
    {
        AudioListener.pause = false; //dont pause the audiolistener on start
    }

    public void ContinueGame()
    {
        FindObjectOfType<SFX_Manager>().Play("buttonSound");
        pauseBackground.gameObject.SetActive(false); //close the background image
        onPause_Panel.SetActive(false); // close the pause panel
        Time.timeScale = 1;     //allow movements around the enviroment
        Player.GetComponent<FirstPersonController>().enabled = true;// allow player movement
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        AudioListener.pause = false;
    }

    public void GameSettings()
    {
        FindObjectOfType<MusicManager>().Play("song_Settings");
        FindObjectOfType<SFX_Manager>().Play("buttonSound");
        options_Panel.SetActive(true);  //open the settings panel
        onPause_Panel.SetActive(false); //close the pause panel
    }

    public void Tutorial()
    {
        FindObjectOfType<SFX_Manager>().Play("buttonSound");
        objectiv_keycards.gameObject.SetActive(true);
        onPause_Panel.SetActive(false); //close the pause panel

        closeObjectiv.gameObject.SetActive(true);
        nextObjectiv.gameObject.SetActive(true);
    }
    public void Tutorial2()
    {
        FindObjectOfType<SFX_Manager>().Play("buttonSound");
        objectiv_pointers.gameObject.SetActive(true);
        onPause_Panel.SetActive(false); //close the pause panel

        closeObjectiv2.gameObject.SetActive(true);
        nextObjectiv2.gameObject.SetActive(true);
    }
    public void KeyBoardInstructions()
    {
        FindObjectOfType<SFX_Manager>().Play("buttonSound");
        keybaordInstructions.gameObject.SetActive(true);
        onPause_Panel.SetActive(false); //close the pause panel
    }

    public void WeaponInformation()
    {
        FindObjectOfType<SFX_Manager>().Play("buttonSound");
        // pauseBackground.gameObject.SetActive(false); //close the background image
        infoWeapon.gameObject.SetActive(true);
        introGunBackground.gameObject.SetActive(true);
        onPause_Panel.SetActive(false); //close the pause panel
    }

    public void ExitGame()
    {
        FindObjectOfType<SFX_Manager>().Play("buttonSound");
        Application.Quit();
    }

    public void ButtonExitWeaponIntro()
    {
        FindObjectOfType<SFX_Manager>().Play("buttonSound");
        if (weapon_instructionStarted)//when eye has been taken and close button is pressed
        {
            options_Panel.SetActive(false);
            onPause_Panel.gameObject.SetActive(false);
            infoWeapon.gameObject.SetActive(false);
            introGunBackground.gameObject.SetActive(false);
            pauseBackground.gameObject.SetActive(false);


            Time.timeScale = 1;     //allow movements around the enviroment
            Player.GetComponent<FirstPersonController>().enabled = true;// allow player movement
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            escape_buttonsEnabled = true;
            AudioListener.pause = false;
            weapon_instructionStarted = false;
        }
        else
        {// when weaponinstruction exit button is pressed
            options_Panel.SetActive(false);
            onPause_Panel.gameObject.SetActive(true);
            pauseBackground.gameObject.SetActive(true);
            infoWeapon.gameObject.SetActive(false);
            introGunBackground.gameObject.SetActive(false);



            Time.timeScale = 0; //stop every movement around the enviroment
            Player.GetComponent<FirstPersonController>().enabled = false; //stop the player from moving
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; // show cursor 
            AudioListener.pause = true;
        }



    }

    public void ButtonExitKeyBoardPanel()
    {
        FindObjectOfType<SFX_Manager>().Play("buttonSound");
        options_Panel.SetActive(false);
        onPause_Panel.gameObject.SetActive(true);
        pauseBackground.gameObject.SetActive(true);
        keybaordInstructions.gameObject.SetActive(false);


        Time.timeScale = 0; //stop every movement around the enviroment
        Player.GetComponent<FirstPersonController>().enabled = false; //stop the player from moving
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; // show cursor 
        AudioListener.pause = true;
    }
    void Update()
    {

        if (escape_buttonsEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                FindObjectOfType<SFX_Manager>().Play("pause");
                FindObjectOfType<MusicManager>().Stop("song_Settings");
                //pause all sounds
                if (AudioListener.pause == false)
                {
                    AudioListener.pause = true;
                }
                else//resume all sounds
                {
                    AudioListener.pause = false;
                }

                // if pause panel and option panel are not opened and the escape key is pressed
                // then open the pause panel
                if (onPause_Panel.gameObject.activeInHierarchy == false && options_Panel.gameObject.activeInHierarchy == false)
                {
                    onPause_Panel.gameObject.SetActive(true);// show the option panel
                    pauseBackground.gameObject.SetActive(true);
                    Time.timeScale = 0; //stop every movement around the enviroment
                    Player.GetComponent<FirstPersonController>().enabled = false; //stop the player from moving
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true; // show cursor           
                }
                //else if the pause panel and the option panel are opened and the escape key is pressed
                //then close the pause panel
                else
                {
                    pauseBackground.gameObject.SetActive(false); //close the background image
                    onPause_Panel.SetActive(false); // close the pause panel
                    Time.timeScale = 1;     //allow movements around the enviroment
                    Player.GetComponent<FirstPersonController>().enabled = true;// allow player movement
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }


                // if the option panel is opended and the pause panel is closed and the escape key is pressed
                //then close the option panel
                if (options_Panel.gameObject.activeInHierarchy == true && onPause_Panel.gameObject.activeInHierarchy == false)
                {
                    options_Panel.SetActive(false);
                    onPause_Panel.gameObject.SetActive(false);
                    pauseBackground.gameObject.SetActive(false);
                    Time.timeScale = 1;
                    Player.GetComponent<FirstPersonController>().enabled = true;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }

                if (infoWeapon.gameObject.activeInHierarchy == true && onPause_Panel.gameObject.activeInHierarchy == true)
                {
                    options_Panel.SetActive(false);
                    onPause_Panel.gameObject.SetActive(false);
                    infoWeapon.gameObject.SetActive(false);
                    introGunBackground.gameObject.SetActive(false);
                    pauseBackground.gameObject.SetActive(false);
                    Time.timeScale = 1;
                    Player.GetComponent<FirstPersonController>().enabled = true;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                if (keybaordInstructions.gameObject.activeInHierarchy)
                {
                    options_Panel.SetActive(false);
                    onPause_Panel.gameObject.SetActive(false);
                    keybaordInstructions.gameObject.SetActive(false);
                    pauseBackground.gameObject.SetActive(false);
                    Time.timeScale = 1;
                    Player.GetComponent<FirstPersonController>().enabled = true;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }

                if (objectiv_keycards.gameObject.activeInHierarchy == true || objectiv_laptop.gameObject.activeInHierarchy == true ||
                    objectiv_slots.gameObject.activeInHierarchy == true || objectiv_help.gameObject.activeInHierarchy == true)
                {
                    options_Panel.SetActive(false);
                    onPause_Panel.gameObject.SetActive(false);
                    pauseBackground.gameObject.SetActive(false);

                    objectiv_help.gameObject.SetActive(false);
                    objectiv_keycards.gameObject.SetActive(false);
                    objectiv_laptop.gameObject.SetActive(false);
                    objectiv_slots.gameObject.SetActive(false);

                    closeObjectiv.gameObject.SetActive(false);
                    prevObjectiv.gameObject.SetActive(false);
                    nextObjectiv.gameObject.SetActive(false);

                    Time.timeScale = 1;
                    Player.GetComponent<FirstPersonController>().enabled = true;
                    Cursor.lockState = CursorLockMode.Locked;
                }

                if (objectiv_pointers.gameObject.activeInHierarchy == true || objectiv_map.gameObject.activeInHierarchy == true ||
                    objectiv_help2.gameObject.activeInHierarchy == true)
                {
                    options_Panel.SetActive(false);
                    onPause_Panel.gameObject.SetActive(false);
                    pauseBackground.gameObject.SetActive(false);
                    objectivBackground.gameObject.SetActive(false);

                    objectiv_pointers.gameObject.SetActive(false);
                    objectiv_map.gameObject.SetActive(false);
                    objectiv_help2.gameObject.SetActive(false);

                    closeObjectiv2.gameObject.SetActive(false);
                    prevObjectiv2.gameObject.SetActive(false);
                    nextObjectiv2.gameObject.SetActive(false);


                    Time.timeScale = 1;
                    Player.GetComponent<FirstPersonController>().enabled = true;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }
    }
}
