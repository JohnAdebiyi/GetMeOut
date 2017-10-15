using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

//the keycardscountertext and imageTimer panel disappear when player takes the weapon. WeaponInSafeScript.cs
public class Timer : MonoBehaviour {



    public Image pauseBackground;
    public Image successKeycards;
    public Image imageTimer;

    public GameObject isDead_Panel;
    public GameObject isDeadBackground;

    public GameObject keycardsRemainingCounter_Panel;
    public GameObject Time_Icon_Panel;

    public Transform player;

    public Text keycardsCounterText;
    public Text timeText;

    private float timeAmount = 300;
    private float time;

    public static int countKeycards = 0;
    private bool hasCollectedAll_Cards;

    private bool timeRemaingWarning1 = true;
    private bool timeRemaingWarning2 = true;
    private bool timeRemaingWarning3 = true;
    private bool timeRemaingWarning4 = true;
    private bool timeRemaingWarningSeconds = true;
    public GameObject timeRemaining_Warning1min;
    public GameObject timeRemaining_Warning2min;
    public GameObject timeRemaining_Warning3min;
    public GameObject timeRemaining_Warning4min;


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
        keycardsCounterText.text = "Keycards found: " + countKeycards.ToString();
        if (countKeycards == 4)
        {
            hasCollectedAll_Cards = true;
            timeText.color = Color.yellow;

            StartCoroutine(SwitchOffPanels());

            FindObjectOfType<SFX_Manager>().Stop("4min");
            FindObjectOfType<SFX_Manager>().Stop("3min");
            FindObjectOfType<SFX_Manager>().Stop("2min");
            FindObjectOfType<SFX_Manager>().Stop("1min");
            FindObjectOfType<SFX_Manager>().Stop("10sec");
            timeRemaining_Warning1min.SetActive(false);
            timeRemaining_Warning2min.SetActive(false);
            timeRemaining_Warning3min.SetActive(false);
            timeRemaining_Warning4min.SetActive(false);


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


        if (timeRemaingWarningSeconds)
        {
            if (time <= 11)
            {
                Debug.Log("10sec");
                FindObjectOfType<SFX_Manager>().Play("10sec");
                timeRemaingWarningSeconds = false;
            }
        }

        if (timeRemaingWarning4)
        {
            if (time <= 240)
            {
                Debug.Log("4:00min");
                FindObjectOfType<SFX_Manager>().Play("4min");
                StartCoroutine(TimeRemaining_Warning4min());
                timeRemaingWarning4 = false;
            }
        }

        if (timeRemaingWarning3)
        {
            if (time <= 180)
            {
                Debug.Log("3:00min");
                FindObjectOfType<SFX_Manager>().Play("3min");
                StartCoroutine(TimeRemaining_Warning3min());
                timeRemaingWarning3 = false;
            }
        }

        if (timeRemaingWarning2)
        {
            if (time <= 120)
            {
                Debug.Log("2:00min");
                FindObjectOfType<SFX_Manager>().Play("2min");
                StartCoroutine(TimeRemaining_Warning2min());
                timeRemaingWarning2 = false;
            }
        }

        if (timeRemaingWarning1)
        {
            if (time <= 60)
            {
                Debug.Log("1:00min");
                FindObjectOfType<SFX_Manager>().Play("1min");
                StartCoroutine(TimeRemaining_Warning1min());
                timeRemaingWarning1 = false;
            }
        }
        

        Failed();
        Success();
    }

    IEnumerator TimeRemaining_Warning1min()
    {
        timeRemaining_Warning1min.SetActive(true);
        yield return new WaitForSeconds(10f);
        timeRemaining_Warning1min.SetActive(false);
    }
    IEnumerator TimeRemaining_Warning2min()
    {
        timeRemaining_Warning2min.SetActive(true);
        yield return new WaitForSeconds(10f);
        timeRemaining_Warning2min.SetActive(false);
    }
    IEnumerator TimeRemaining_Warning3min()
    {
        timeRemaining_Warning3min.SetActive(true);
        yield return new WaitForSeconds(10f);
        timeRemaining_Warning3min.SetActive(false);
    }
    IEnumerator TimeRemaining_Warning4min()
    {
        timeRemaining_Warning4min.SetActive(true);
        yield return new WaitForSeconds(10f);
        timeRemaining_Warning4min.SetActive(false);
    }


    public void CloseSuccessKeycards()
    {
        FindObjectOfType<SFX_Manager>().Play("buttonSound");

        pauseBackground.gameObject.SetActive(false);
        successKeycards.gameObject.SetActive(false);

        Time.timeScale = 1;     //allow movements around the enviroment
        player.GetComponent<FirstPersonController>().enabled = true;// allow player movement
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        IsPause.escape_buttonsEnabled = true; // enable the pause button
    }
}
