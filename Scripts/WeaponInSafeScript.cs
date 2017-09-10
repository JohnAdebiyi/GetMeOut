using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponInSafeScript : MonoBehaviour
{

    public Animator _animator;
    public GameObject openPanel = null;
    public GameObject outsideDoorIsOpened_Panel;
    public GameObject weapon;
    public GameObject weaponInSafe;
    public GameObject weaponIcon;
    public Camera fpsCam;

    public Image introGunBackground;
    public Image introWeapon;
    public Button closeIntro;
    public Transform Player;

    public Image pause_ButtonInfoWeapon;// show button in the pause panel when weapon has been recieved

    public string openText = "Take item";
    public string closeText = "";
    public bool inTrigger;
    private bool _isOpen = false;

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
    }


    private bool IsOpenPanelActive
    {
        get
        {
            return openPanel.activeInHierarchy;
        }
    }


    private void UpdatePanelText()
    {
        UnityEngine.UI.Text panelText = openPanel.transform.FindChild("Text").GetComponent<UnityEngine.UI.Text>();
        if (panelText != null)
        {
            panelText.text = _isOpen ? closeText : openText;//if _isOpen is true return CloseText or else return openText
        }
    }

    void Update()
    {

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 2))
        {
            if (hit.collider.gameObject.tag == "weaponInSafe")
            {
                inTrigger = true;
                UpdatePanelText();
                openPanel.SetActive(true);// panel can now being seen
            }
        }
        else
        {
            openPanel.SetActive(false);// panel is invincible
            inTrigger = false;
        }


        // when panel is visible show text 
        if (inTrigger)
        {
            if (!EventSystem.current.IsPointerOverGameObject()) //stop raycast on UI clicks. when UI is activ, gameObjects arent hit with raycast.
            {
                if (Input.GetMouseButtonDown(1))
                {
                    FindObjectOfType<SFX_Manager>().Play("gotSpecialItem");
                    FindObjectOfType<MusicManager>().Stop("song");
                    FindObjectOfType<MusicManager>().Play("song2");
                    DoorOutsideScript.weaponObtained = true;
                    //Destroy(this.gameObject);// destroy the gameobject that the script refers to
                    weaponInSafe.SetActive(false);
                    openPanel.SetActive(false);// panel is invincible
                    outsideDoorIsOpened_Panel.SetActive(true);// "outside door is now opened"
                    weapon.SetActive(true);// display weapon
                    weaponIcon.SetActive(true);// display weaponIcons
                    EnemyAIScript.isOutside = true;

                   pause_ButtonInfoWeapon.gameObject.SetActive(true);

                    introGunBackground.gameObject.SetActive(true);
                    introWeapon.gameObject.SetActive(true);
                    Time.timeScale = 0; //stop every movement around the enviroment
                    Player.GetComponent<FirstPersonController>().enabled = false; //stop the player from moving
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true; // show cursor
                    IsPause.escape_buttonsEnabled = false; // disable the pause button
                }
            }
        }
    }


    public void closeIntroWeapon()
    {
        FindObjectOfType<SFX_Manager>().Play("buttonSound");

        introGunBackground.gameObject.SetActive(false);
        introWeapon.gameObject.SetActive(false);

        Time.timeScale = 1;     //allow movements around the enviroment
        Player.GetComponent<FirstPersonController>().enabled = true;// allow player movement
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        IsPause.escape_buttonsEnabled = true; // enable the pause button
    }
}
