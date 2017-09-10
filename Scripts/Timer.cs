using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

//the keycardscountertext and imageTimer panel disappear when player takes the weapon. WeaponInSafeScript.cs
public class Timer : MonoBehaviour {

    public GameObject isDead_Panel;
    public GameObject isDeadBackground;
    public Transform player;

    public GameObject keycardsRemainingCounter_Panel;
    public GameObject Time_Icon_Panel;

    public Image imageTimer;
    public Text keycardsCounterText;
    public Text timeText;

    private float timeAmount = 300;
    private float time;

    public static int countKeycards = 4;
    private bool hasCollectedAll_Cards= false;

    void Start ()
    {
        time = timeAmount;
	}

    IEnumerator SwitchOffPanels()
    {      
        yield return new WaitForSeconds(2f);
        keycardsRemainingCounter_Panel.SetActive(false);
        Time_Icon_Panel.SetActive(false);
    }

    void Success()
    {
        keycardsCounterText.text = "Keycards Remaining: " + countKeycards.ToString();
        if (countKeycards == 0)
        {
            hasCollectedAll_Cards = true;
            timeText.color = Color.yellow;

            StartCoroutine(SwitchOffPanels());

        }
    }

    void Failed()
    {
        if (time <= 0)
        {
            Debug.Log("dead");
            if (isDead_Panel.gameObject.activeInHierarchy == false)
            {
                FindObjectOfType<SFX_Manager>().Play("gameOver");
                isDead_Panel.gameObject.SetActive(true);// show isDead panel options
                isDeadBackground.gameObject.SetActive(true); // darken the background
                Time.timeScale = 0; //stop every movement around the enviroment
                player.GetComponent<FirstPersonController>().enabled = false; //stop the player from moving
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true; // show cursor
                IsPause.escape_buttonsEnabled = false;
            }
        }
    }


    void Update()
    {
        if (time > 0)
        {
            if (hasCollectedAll_Cards)
                return;

            time -= Time.deltaTime;
            imageTimer.fillAmount = time / timeAmount;

            string min = ((int)time / 60).ToString();
            string sec = (time % 60).ToString("f2");

            timeText.text = min + ":" + sec;
        }
        //Debug.Log(countKeycards);


        Failed();
        Success();
    }
}
