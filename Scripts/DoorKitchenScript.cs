using UnityEngine;
using UnityEngine.EventSystems;
public class DoorKitchenScript : MonoBehaviour {

    public Animator _animator;
    public GameObject openPanel = null;
 
    public GameObject openPanel_go_Do_Activation = null;

    public GameObject destroyKeycardMinimalized003;// important! using Tag to destroy
    public GameObject destroyKeycardIsActiveMinimalized003;// important! using Tag to destroy
    public Camera fpsCam;
    public GameObject panel_insertTheCorrectCard;
    public GameObject keycard_inserted;

    public bool inTrigger;

    public string openText = "Insert KeyCard to Open door";
    public string closeText = "";

    private bool _isOpen = false;



    public static bool keyCard_To_Laptop; // key card is now activ for the door to open from Laptop_GameScript2.cs => DoorKitchenScript.keycardIsActiv = true
    public static bool keycardIsActiv;// dont show key needs to be activated since the answer is correct from Laptop_GameScript3.cs => DoorKitchenScript.KeyCard_To_Laptop = false

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
    }


    // for checking if the kitchen door panel is activ
    private bool IsOpenPanelActive
    {
        get
        {
            return openPanel.activeInHierarchy;
        }
    }

    // for updating the kitchen door panel text
    private void UpdatePanelText()
    {
        UnityEngine.UI.Text panelText = openPanel.transform.FindChild("Text").GetComponent<UnityEngine.UI.Text>();
        if (panelText != null)
        {
            panelText.text = _isOpen ? closeText : openText;//if _isOpen is true return closeText or else return openText
        }
    }


    // if raycast hits the kitchen door collider, set _isInsideTrigger to true else set to false
    void _RaycastHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 2))
        {
            if (hit.collider.gameObject.tag == "door_Kitchen" || hit.collider.gameObject.tag == "slot_Kitchen")
            {
                inTrigger = true;
                UpdatePanelText();

                if (keyCard_To_Laptop == true)//if key card is set to true from Laptop_GameScript2.KeyCard_To_Laptop
                {
                    openPanel_go_Do_Activation.SetActive(true);
                }
                else
                {
                    openPanel.SetActive(true);
                }
            }
        }
        else
        {
            inTrigger = false;
            panel_insertTheCorrectCard.SetActive(false);
            if (keyCard_To_Laptop == true)
            {
                openPanel_go_Do_Activation.SetActive(false);
            }
            else
            {
                openPanel.SetActive(false);
            }
        }
    }
    // if _isInsideTrigger is true and mouse is pressed open kitchen door, show no error and deactivate kitchen door panel
    void InsideTrigger()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) //stop raycast on UI clicks. when UI is activ, gameObjects arent hit with raycast.
        {
            if (inTrigger == true)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    if (keycardIsActiv == true)
                    {
                        FindObjectOfType<SFX_Manager>().Play("doorOpen");
                        _animator.SetBool("openKitchenDoor", true);
                        openPanel.SetActive(false);// panel is invincible
                        _animator.SetBool("KitchenSlot_showError", true);
                        _animator.SetBool("KitchenSlot_showNoError", true);
                        openPanel = null;
                        Laptop_GameScript_2.keyCard_To_Laptop = false;// dont show the game on the laptop
                        keycard_inserted.SetActive(true);
                    }
                    else
                    {
                        if (Input.GetMouseButtonDown(1))
                        {
                            FindObjectOfType<SFX_Manager>().Play("error");
                            _animator.SetBool("KitchenSlot_showError", true);
                            panel_insertTheCorrectCard.SetActive(true);
                        }
                    }
                }
            }
        }
    }

    // destroy the mini panel "keycard obtainded" and "keycard is activ" when door opens
    void DestroyPanel()
    {
        destroyKeycardMinimalized003 = GameObject.FindGameObjectWithTag("keycard003");
        destroyKeycardIsActiveMinimalized003 = GameObject.FindGameObjectWithTag("keycardIsActive03");


        if (_animator.GetBool("openKitchenDoor") == true)
        {
            Destroy(destroyKeycardMinimalized003);
            Destroy(destroyKeycardIsActiveMinimalized003);
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
