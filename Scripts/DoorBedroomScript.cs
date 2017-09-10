using UnityEngine;
using UnityEngine.EventSystems;

public class DoorBedroomScript : MonoBehaviour
{

    public Animator _animator;
    public GameObject openPanel = null;
    public GameObject destroyKeycardMinimalized001;// important! using Tag to destroy // for destroying the panel keycard obtainded
    public Camera fpsCam;

    public bool inTrigger;

    public string openText = "Insert KeyCard to Open door";
    public string closeText = "";

    private bool _isOpen = false;

    public static bool keyCardBedroom;


    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();        
    }


    // for checking if the bedroom door panel is activ
    private bool IsOpenPanelActive
    {
        get
        {
            return openPanel.activeInHierarchy;
        }
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
                _animator.SetBool("BedroomSlot_showError", true);
                inTrigger = true;
                UpdatePanelText();
                openPanel.SetActive(true);
            }
        }
        else
        {
            _animator.SetBool("BedroomSlot_showError", false);
            inTrigger = false;
            openPanel.SetActive(false);
        }
    }
    //if _isInsideTrigger is true and mouse is pressed open bedroom door, show no error and deactivate the bedroom door panel
    void InsideTrigger()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) //stop raycast on UI clicks. when UI is activ, gameObjects arent hit with raycast.
        {
            if (inTrigger == true)
            {

                if (keyCardBedroom)
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        _animator.SetBool("BedroomSlot_showNoError", true);
                        _animator.SetBool("OpenDoorBedroom", true);
                        openPanel.SetActive(false);
                        openPanel = null;
                        FindObjectOfType<SFX_Manager>().Play("doorOpen");
                    }
                }
            }
        }
    }

    // destroy the mini panel "keycard obtainded" when doors opens
    void DestroyPanel()
    {
        destroyKeycardMinimalized001 = GameObject.FindGameObjectWithTag("keycard001");
        if (_animator.GetBool("OpenDoorBedroom") == true)
        {
            Destroy(destroyKeycardMinimalized001);
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