using UnityEngine;
using UnityEngine.EventSystems;
public class DoorBathroomScript : MonoBehaviour {

    public Animator _animator;

    public Camera fpsCam;

    public GameObject openPanel;
    public GameObject destroyKeycardMinimalized002;// for destroying the panel keycard obtainded
    public GameObject panel_insertTheCorrectCard;
    public GameObject keycard_inserted;

    public string openText = "Insert KeyCard to Open door";
    public string closeText = "";

    public bool inTrigger;
    public static bool keyCard_To_Bathroom;
    private bool _isOpen;
    private bool update = true;// for getting rid of errors -> NullReferenceException: Object reference not set to an instance of an object


    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // for updating the bathroom door panel text 
    private void UpdatePanelText()
    {
        UnityEngine.UI.Text panelText = openPanel.transform.FindChild("Text").GetComponent<UnityEngine.UI.Text>();
        if (panelText != null)
        {
            panelText.text = _isOpen ? closeText : openText;//if _isOpen is true return closeText or else return openText
        }
    }


    // if raycast hits the bathroom door collider, set _isInsideTrigger to true else set to false
    void _RaycastHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 2))
        {
            if (hit.collider.gameObject.tag == "door_Bathroom" || hit.collider.gameObject.tag == "slot_Bathroom")
            {
                inTrigger = true;
                UpdatePanelText();
                openPanel.SetActive(true);
            }
        }
        else
        {
            inTrigger = false;
            openPanel.SetActive(false);
            panel_insertTheCorrectCard.SetActive(false);
        }
    }


    //if _isInsideTrigger is true and mouse is pressed show no error, open bathroom door and deactivate the bathroom door panel
    void InsideTrigger()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) //stop raycast on UI clicks. when UI is activ, gameObjects arent hit with raycast.
        {

            if (inTrigger)
            {
                if (keyCard_To_Bathroom)
                // if (true)
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        _animator.SetBool("OpenDoorBathroom", true);
                        openPanel.SetActive(false);// panel is invincible
                        _animator.SetBool("BathroomSlot_showError", true);
                        _animator.SetBool("BathroomSlot_showNoError", true);
                        openPanel = null;
                        FindObjectOfType<SFX_Manager>().Play("doorOpen");
                        keycard_inserted.SetActive(true);
                        update = false;
                    }
                }
                else
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        FindObjectOfType<SFX_Manager>().Play("error");
                        _animator.SetBool("BathroomSlot_showError", true);
                        panel_insertTheCorrectCard.SetActive(true);
                    }
                }
            }
        }
    }
    // destroy the mini panel "keycard obtainded" when door opens
    void DestroyPanel()
    {
        destroyKeycardMinimalized002 = GameObject.FindGameObjectWithTag("keycard002");


        if (_animator.GetBool("OpenDoorBathroom"))
        {
            Destroy(destroyKeycardMinimalized002);
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