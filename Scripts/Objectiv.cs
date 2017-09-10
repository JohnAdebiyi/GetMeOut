using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Objectiv : MonoBehaviour {

    public Image objectiv;
    public Image objectivBackground;
    public Button closeObjectiv;
    public Transform Player;

    void Start ()
    {

        objectivBackground.gameObject.SetActive(true);
        objectiv.gameObject.SetActive(true);
        Time.timeScale = 0; //stop every movement around the enviroment
        Player.GetComponent<FirstPersonController>().enabled = false; //stop the player from moving
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; // show cursor
        IsPause.escape_buttonsEnabled = false; // disable the pause button
    }

    public void CloseObjektiv()
    {
        FindObjectOfType<SFX_Manager>().Play("buttonSound");

        objectivBackground.gameObject.SetActive(false);
        objectiv.gameObject.SetActive(false);

        Time.timeScale = 1;     //allow movements around the enviroment
        Player.GetComponent<FirstPersonController>().enabled = true;// allow player movement
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        IsPause.escape_buttonsEnabled = true; // enable the pause button
    }
}
