using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerStatusScript : MonoBehaviour
{

    //public static bool alive = true;
    private float actualHealth;// used for converting float to int for the health text
    private int temp;// used for converting flaot to int for the health text
    private bool isRestarting = false;
    public bool buttonEnabled = false;


    // the current health is saved on every frame Update to respawnhealth and passed 
    //THEN to CheckPoint.cs => currentHealth = PlayerStatusScript.respawnHealth; when player enter the checkpoint trigger.
    //When player gets respawned, bar.fillamount then takes the value from = CheckPoint.currentHealth;
    public static float respawnHealth;


    private HealthBar_BlurScript healthbar_blur;//off on healthbar blur when damage is taken
    public Image bar;
    public Text healthText;
    public Transform Player;
    public GameObject isDead_Panel;
    public GameObject isDeadBackground;
    public Button buttonClickRestartCheckPoint;
    public GameObject blackFadeInOUtImage_CheckPoint;





    void Start()
    {     
        healthbar_blur = GetComponent<HealthBar_BlurScript>();
        bar.fillAmount = 1f;// full health on start

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }


    public void GetDamage(float damage)
    {
        healthbar_blur.ShowBlur();

        bar.fillAmount -= damage;// subtract from fillamout bar -> example.        (bar.fillAmount)1f - (damage)0.03 = 0.97
        actualHealth = bar.fillAmount * 100f;// multiply by 100 -> example. (actualHealth)97.000 = (bar.fillAmount)0.97 * 100
        temp = (Mathf.RoundToInt(actualHealth));//convert (actualHealth)97.000  to int
        healthText.text = "Health: " + temp.ToString();//health text


        // On Death
        if (bar.fillAmount <= 0)
        {
            FindObjectOfType<SFX_Manager>().Play("gameOver");
            isDead_Panel.gameObject.SetActive(true);// show isDead panel options
            isDeadBackground.gameObject.SetActive(true); // darken the background
            Time.timeScale = 0; //stop every movement around the enviroment
            Player.GetComponent<FirstPersonController>().enabled = false; //stop the player from moving
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; // show cursor
            IsPause.escape_buttonsEnabled = false;//dont allow player to pause


            if (isRestarting == false)//in case multiple checkpoints are needed
            {
                //Button btn = buttonClickRestartCheckPoint.GetComponent<Button>();
                //btn.onClick.AddListener(OnButtonClick_RestartCheckPoint);
                blackFadeInOUtImage_CheckPoint.SetActive(false);
            }
        }
    }

    //when checkpoint button is clicked
    public void OnButtonClick_RestartCheckPoint()
    {
        blackFadeInOUtImage_CheckPoint.SetActive(true);
        bar.fillAmount = CheckPoint.currentHealth;
        actualHealth = bar.fillAmount * 100f;// multiply by 100 -> example. (actualHealth)97.000 = (bar.fillAmount)0.97 * 100
        temp = (Mathf.RoundToInt(actualHealth));//convert (actualHealth)97.000  to int
        healthText.text = "Health: " + temp.ToString();//health text



        isRestarting = true;
        Player.transform.position = CheckPoint.reachedPoint; //POSITION THE PLAYER TO CHECKPOINT

        if (isDead_Panel.gameObject.activeInHierarchy == true)
        {
            blackFadeInOUtImage_CheckPoint.SetActive(false);
            isDead_Panel.gameObject.SetActive(false);// dont show isDead panel options
            isDeadBackground.gameObject.SetActive(false); //  dont darken the background
            Time.timeScale = 1; //allow every movement around the enviroment
            Player.GetComponent<FirstPersonController>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            IsPause.escape_buttonsEnabled = true;
        }
        isRestarting = false;

    }

    public void IncreaseHealth(float health)
    {
        bar.fillAmount += health;
        actualHealth = bar.fillAmount * 100f;
        temp = (Mathf.RoundToInt(actualHealth));
        healthText.text = "Health: " + temp.ToString();//health text
    }



    // if player enters the eye trigger of the enemy
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "eyesFront" || other.gameObject.name == "eyesFrontLeft" || other.gameObject.name == "eyesFrontRight"
            || other.gameObject.name == "eyesBehindRight" || other.gameObject.name == "eyesBehindFront")
        {
            other.transform.parent.GetComponent<EnemyAIScript>().CheckSight();
        }
    }



    void Update()
    {
        respawnHealth = bar.fillAmount;
    }
}
