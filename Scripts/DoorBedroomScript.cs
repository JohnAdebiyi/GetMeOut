using UnityEngine;
using UnityEngine.EventSystems;

public class DoorBedroomScript : MonoBehaviour
{

    public Animator _animator;
    public GameObject openPanel;
    public GameObject destroyKeycardMinimalized001;// important! using Tag to destroy // for destroying the panel keycard obtainded
    public Camera fpsCam;
    public GameObject panel_insertTheCorrectCard;
    public GameObject keycard_inserted;

    public string openText = "Insert KeyCard to Open door";
    public string closeText = "";

    public bool inTrigger;
    private bool _isOpen;
    public static bool keyCardBedroom;
    private bool update = true;// for getting rid of errors -> NullReferenceException: Object reference not set to an instance of an object


    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();        
    }

    // for updating the bedroom door panel text 
    private void UpdatePanelText()
    {
        UnityEngine.UI.Text panelText = openPanel.transform.FindChild("Text").GetComponent<UnityEngine.UI.Text>();
        if (panelText != null)
        {
            panelText.text = _isOpen ? closeText : openText;//if _isOpen is true return closeText or else return openText
        }
    }


    // if raycast hits the bedroom door collider, set _isInsideTrigger to true else set to false
    void _RaycastHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 2))
        {
            if (hit.collider.gameObject.tag == "door_Bedroom" || hit.collider.gameObject.tag == "slot_Bedroom")
            {              
                inTrigger = true;
                UpdatePanelText();
                openPanel.SetActive(true);
            }else
            {

            }
        }
        else
        {            
            inTrigger = false;
            openPanel.SetActive(false);
            panel_insertTheCorrectCard.SetActive(false);
        }
    }
    //if _isInsideTrigger is true and mouse is pressed open bedroom door, show no error and deactivate the bedroom door panel
    void InsideTrigger()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) //stop raycast on UI clicks. when UI is activ, gameObjects arent hit with raycast.
        {
            if (inTrigger)
            {
                if (keyCardBedroom)
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        _animator.SetBool("BedroomSlot_showError", true);
                        _animator.SetBool("BedroomSlot_showNoError", true);
                        _animator.SetBool("OpenDoorBedroom", true);
                        openPanel.SetActive(false);
                        openPanel = null;
                        keycard_inserted.SetActive(true);
                        FindObjectOfType<SFX_Manager>().Play("doorOpen");
                        update = false;
                    }
                }else
                    {
                    if (Input.GetMouseButtonDown(1))
                    {
                        FindObjectOfType<SFX_Manager>().Play("error");
                        _animator.SetBool("BedroomSlot_showError", true);
                        panel_insertTheCorrectCard.SetActive(true);
                    }
                }
            }
        }
    }

    // destroy the mini panel "keycard obtainded" when doors opens
    void DestroyPanel()
    {
        destroyKeycardMinimalized001 = GameObject.FindGameObjectWithTag("keycard001");
        if (_animator.GetBool("OpenDoorBedroom"))
        {
            Destroy(destroyKeycardMinimalized001);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (update)
        {
            _RaycastHit();
            InsideTrigger();
            DestroyPanel();
        }
    }
}