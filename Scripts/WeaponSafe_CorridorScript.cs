using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class WeaponSafe_CorridorScript : MonoBehaviour
{

    public Animator _animator;
    public GameObject openPanel = null;

    public GameObject openPanel_go_Do_Activation = null;
    public GameObject destroyKeycardMinimalized004;// important! using Tag to destroy
    public GameObject destroyKeycardIsActiveMinimalized004;// important! using Tag to destroy
    public Collider weaponInSafe_Collider; // weaponInSafe and weapon_slot_Screen Gameobject needs to be assigned
    public Camera fpsCam;

    public GameObject panel_insertTheCorrectCard;
    public GameObject keycard_inserted;

    public bool inTrigger;

    public string openText = "Insert keycard to open";
    public string closeText = "";

    private bool _isOpen = false;



    public static bool keyCard_To_Laptop; // key card is now activ for the door to open from Laptop_GameScript3.cs => WeaponCorridorScript.keycardIsActiv = true
    public static bool keycardIsActiv;// dont show key needs to be activated since the answer is correct from Laptop_GameScript3.cs => WeaponCorridorScript.KeyCard_To_Laptop = false


    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
        weaponInSafe_Collider.enabled = false;

    }

    // for checking if the panel is activ
    private bool IsOpenPanelActive
    {
        get
        {
            return openPanel.activeInHierarchy;
        }
    }

    // for updating panel text 
    private void UpdatePanelText()
    {
        UnityEngine.UI.Text panelText = openPanel.transform.FindChild("Text").GetComponent<UnityEngine.UI.Text>();
        if (panelText != null)
        {
            panelText.text = _isOpen ? closeText : openText;//if _isOpen is true return CloseText or else return openText
        }
    }

    // wait for a certain amount of time till safe is opened and then enable the the weaponInSafe_Collider
    IEnumerator openSafe()
    {
        yield return new WaitForSeconds(1);
        weaponInSafe_Collider.enabled = true;
    }


    // if raycast hits the target, set _isInsideTrigger to true else set to false
    void _RaycastHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 2))
        {
            if (hit.collider.gameObject.tag == "glas_weapon" || hit.collider.gameObject.tag == "slot_weapon")
            {
                inTrigger = true;
                UpdatePanelText();
                if (keyCard_To_Laptop == true)//if key card is set to true from Laptop_GameScript3.KeyCard_To_Laptop
                {
                    panel_insertTheCorrectCard.SetActive(false);
                    openPanel_go_Do_Activation.SetActive(true);
                }
                else
                {
                    openPanel.SetActive(true);// panel can now being seen
                }
            }
        }
        else
        {
            panel_insertTheCorrectCard.SetActive(false);
            inTrigger = false;
            if (keyCard_To_Laptop == true)//if player has the keycard deactivate the go do activation panel
            {
                panel_insertTheCorrectCard.SetActive(false);
                openPanel_go_Do_Activation.SetActive(false);
            }
            else
            {
                openPanel.SetActive(false);// panel is invincible
            }
        }

    }

    // if panel is activ, if _isInsideTrigger is true and mouse pressed, then open the safe 
    void InsideTrigger()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) //stop raycast on UI clicks. when UI is activ, gameObjects arent hit with raycast.
        {
            if (inTrigger == true)
            {
                if (Input.GetMouseButtonDown(1))
                {
                  //if (true)
                     if (keycardIsActiv == true)
                    {
                        _animator.SetBool("openWeaponSafe", true);// open the safe
                        StartCoroutine(openSafe());// wait for a certain amount of time till safe is opened and then enable the the weaponInSafe_Collider

                        openPanel.SetActive(false);// panel is invincible
                        _animator.SetBool("weaponSlot_showError", true);
                        _animator.SetBool("weaponSlot_showNoError", true);
                        openPanel = null;
                        Laptop_GameScript_3.keyCard_To_Laptop = false; // deactivate panel "game"
                        FindObjectOfType<SFX_Manager>().Play("doorOpen");
                        keycard_inserted.SetActive(true);                        
                    }
                    else
                    {
                        if (Input.GetMouseButtonDown(1))
                        {
                            FindObjectOfType<SFX_Manager>().Play("error");
                            _animator.SetBool("weaponSlot_showError", true);
                            panel_insertTheCorrectCard.SetActive(true);
                        }
                    }
                }
            }
        }
    }


    void DestroyPanel()
    {
        destroyKeycardMinimalized004 = GameObject.FindGameObjectWithTag("keycard004");
        destroyKeycardIsActiveMinimalized004 = GameObject.FindGameObjectWithTag("keycardIsActive04");


        if (_animator.GetBool("openWeaponSafe") == true)
        {
            Destroy(destroyKeycardMinimalized004); // destroy the mini panel keycard obtainded
            Destroy(destroyKeycardIsActiveMinimalized004);// destroy the mini panel keycard is activ
        }

    }
    // Update is called once per frame
    void Update()
    {
        _RaycastHit();
        InsideTrigger();
        DestroyPanel();
    }
}

