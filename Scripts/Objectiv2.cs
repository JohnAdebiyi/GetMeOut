using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Objectiv2 : MonoBehaviour {

    public Image pauseBackground;
    public Image objectivBackground;
    public Button closeObjectiv;
    public Button nextObjectiv;
    public Button prevObjectiv;
    public GameObject onPause_Panel;


    public Image objectiv_pointers;
    public Image objectiv_map;
    public Image objectiv_help;

    public Transform Player;

    // public static bool pauseIsPressed;
    public static bool objectiv2_Started;

    public void CloseObjektiv()
    {
        FindObjectOfType<SFX_Manager>().Play("buttonSound");
        if (objectiv2_Started)//when eye has been taken and close button is pressed
        {
            pauseBackground.gameObject.SetActive(false);//pause modus
            objectivBackground.gameObject.SetActive(false);
            objectiv_pointers.gameObject.SetActive(false);
            objectiv_map.gameObject.SetActive(false);
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
            objectiv2_Started = false;
        }
        else
        {// when tutorial exit button is pressed
            onPause_Panel.gameObject.SetActive(true);
            pauseBackground.gameObject.SetActive(true);//pause modus
            objectivBackground.gameObject.SetActive(false);
            objectiv_pointers.gameObject.SetActive(false);
            objectiv_map.gameObject.SetActive(false);
            objectiv_help.gameObject.SetActive(false);

            closeObjectiv.gameObject.SetActive(false);
            nextObjectiv.gameObject.SetActive(false);
            prevObjectiv.gameObject.SetActive(false);

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

            if (objectiv_pointers.gameObject.activeInHierarchy == true)
            {
                objectiv_pointers.gameObject.SetActive(false);
                objectiv_map.gameObject.SetActive(true);

                prevObjectiv.gameObject.SetActive(true);
                closeObjectiv.gameObject.SetActive(true);
                nextObjectiv.gameObject.SetActive(true);
            }
            else if (objectiv_map.gameObject.activeInHierarchy == true)
            {
                objectiv_map.gameObject.SetActive(false);
                objectiv_help.gameObject.SetActive(true);

                closeObjectiv.gameObject.SetActive(true);
                nextObjectiv.gameObject.SetActive(false);
                prevObjectiv.gameObject.SetActive(true);
            }           
            else
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


            if (objectiv_map.gameObject.activeInHierarchy == true)
            {
                objectiv_map.gameObject.SetActive(false);
                objectiv_pointers.gameObject.SetActive(true);

                prevObjectiv.gameObject.SetActive(false);
                nextObjectiv.gameObject.SetActive(true);

            }
            if (objectiv_help.gameObject.activeInHierarchy == true)
            {
                objectiv_help.gameObject.SetActive(false);
                objectiv_map.gameObject.SetActive(true);

                prevObjectiv.gameObject.SetActive(true);
                nextObjectiv.gameObject.SetActive(true);

            }

            Time.timeScale = 0; //stop every movement around the enviroment
            Player.GetComponent<FirstPersonController>().enabled = false; //stop the player from moving
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; // show cursor
    }

}
