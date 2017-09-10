using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class EndPoint : MonoBehaviour {

    public GameObject isFinished_Panel;
    public Transform Player;

    void OnTriggerEnter(Collider other) 
    {
        isFinished_Panel.SetActive(true);
        Time.timeScale = 0; //stop every movement around the enviroment
        Player.GetComponent<FirstPersonController>().enabled = false; //stop the player from moving
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; // show cursor
        IsPause.escape_buttonsEnabled = false;//dont allow player to pause

    }
}
