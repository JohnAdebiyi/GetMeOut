using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

//goes to objektive_Tutorial_Manager
public class Objectiv : MonoBehaviour {

    public Image objectiv_keycards;
    public Image objectiv_slots;
    public Image objectiv_laptop;
    public Image objectiv_help;
    public GameObject onPause_Panel;


    public Image pauseBackground;
    public Image objectivBackground;
    public Button closeObjectiv;
    public Button nextObjectiv;
    public Button prevObjectiv;
    public Transform Player;

    //public static bool pauseIsPressed;
    public bool objectivStarted;

    void Start ()
    {
        objectivStarted = true;
        objectivBackground.gameObject.SetActive(true);
        objectiv_keycards.gameObject.SetActive(true);

        prevObjectiv.gameObject.SetActive(false);
        closeObjectiv.gameObject.SetActive(true);
        nextObjectiv.gameObject.SetActive(true);

        Time.timeScale = 0; //stop every movement around the enviroment
        Player.GetComponent<FirstPersonController>().enabled = false; //stop the player from moving
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; // show cursor
        IsPause.escape_buttonsEnabled = false; // disable the pause button
    }

    public void CloseObjektiv()
    {
        FindObjectOfType<SFX_Manager>().Play("buttonSound");

        if (objectivStarted)//when game starts and close button is pressed
        {
            pauseBackground.gameObject.SetActive(false);//pause modus
            objectivBackground.gameObject.SetActive(false);
            objectiv_keycards.gameObject.SetActive(false);
            objectiv_slots.gameObject.SetActive(false);
            objectiv_laptop.gameObject.SetActive(false);
            objectiv_help.gameObject.SetActive(false);

            closeObjectiv.gameObject.SetActive(false);
            nextObjectiv.gameObject.SetActive(false);
            prevObjectiv.gameObject.SetActive(false);

            Time.timeScale = 1;     //allow movements around the enviroment
            Player.GetComponent<FirstPersonController>().enabled = true;// allow player movement
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            IsPause.escape_buttonsEnabled = true; // enable the pause button
            AudioListener.pause = false;
            objectivStarted = false;
        }
        else {// when tutorial exit button is pressed
            onPause_Panel.gameObject.SetActive(true);
            pauseBackground.gameObject.SetActive(true);

            objectiv_help.gameObject.SetActive(false);
            objectiv_keycards.gameObject.SetActive(false);
            objectiv_laptop.gameObject.SetActive(false);
            objectiv_slots.gameObject.SetActive(false);

            closeObjectiv.gameObject.SetActive(false);
            prevObjectiv.gameObject.SetActive(false);
            nextObjectiv.gameObject.SetActive(false);

            Time.timeScale = 0; //stop every movement around the enviroment
            Player.GetComponent<FirstPersonController>().enabled = false; //stop the player from moving
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; // show cursor 
            AudioListener.pause = true;
        }

    }

    public void NextObjektiv()
    {


        FindObjectOfType<SFX_Manager>().Play("buttonSound");

        if(objectiv_keycards.gameObject.activeInHierarchy == true)
        {
            objectiv_keycards.gameObject.SetActive(false);
            objectiv_slots.gameObject.SetActive(true);

            prevObjectiv.gameObject.SetActive(true);
            closeObjectiv.gameObject.SetActive(true);
            nextObjectiv.gameObject.SetActive(true);
        }else if (objectiv_slots.gameObject.activeInHierarchy == true)
        {
            objectiv_slots.gameObject.SetActive(false);
            objectiv_laptop.gameObject.SetActive(true);

            closeObjectiv.gameObject.SetActive(true);
            nextObjectiv.gameObject.SetActive(true);
            prevObjectiv.gameObject.SetActive(true);
        }else if (objectiv_laptop.gameObject.activeInHierarchy == true)
        {
            objectiv_laptop.gameObject.SetActive(false);
            objectiv_help.gameObject.SetActive(true);

            closeObjectiv.gameObject.SetActive(true);
            nextObjectiv.gameObject.SetActive(false);
            prevObjectiv.gameObject.SetActive(true);
        }else
        {
            //do nothing
        }



        Time.timeScale = 0; //stop every movement around the enviroment
        Player.GetComponent<FirstPersonController>().enabled = false; //stop the player from moving
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; // show cursor
    }

    public void PrevObjektiv()
    {
        FindObjectOfType<SFX_Manager>().Play("buttonSound");


        if (objectiv_slots.gameObject.activeInHierarchy == true)
        {
            objectiv_slots.gameObject.SetActive(false);
            objectiv_keycards.gameObject.SetActive(true);

            prevObjectiv.gameObject.SetActive(false);
            nextObjectiv.gameObject.SetActive(true);
        }

        if (objectiv_laptop.gameObject.activeInHierarchy == true)
        {
            objectiv_laptop.gameObject.SetActive(false);
            objectiv_slots.gameObject.SetActive(true);
        }

        if (objectiv_help.gameObject.activeInHierarchy == true)
        {
            objectiv_help.gameObject.SetActive(false);
            objectiv_laptop.gameObject.SetActive(true);

            prevObjectiv.gameObject.SetActive(true);
            nextObjectiv.gameObject.SetActive(true);
        }

        Time.timeScale = 0; //stop every movement around the enviroment
        Player.GetComponent<FirstPersonController>().enabled = false; //stop the player from moving
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; // show cursor   
    }
}
