using UnityEngine;
using UnityEngine.EventSystems;
public class DoorCorridor_To_LivingRoomScript : MonoBehaviour {

    public Animator _animator;
    public GameObject openPanel = null;
    public Camera fpsCam;
    public ParticleSystem computerLocationEff;
    public bool inTrigger;

    public string openText = "Left mouse to Open door";
    public string closeText = "";

    private bool _isOpen = false;

    public static bool keyCardBedroom;


    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // for checking if the livingroom door panel is activ
    private bool IsOpenPanelActive
    {
        get
        {
            return openPanel.activeInHierarchy;
        }
    }

    // for updating the livingroom door panel text 
    private void UpdatePanelText()
    {
        UnityEngine.UI.Text panelText = openPanel.transform.FindChild("Text").GetComponent<UnityEngine.UI.Text>();
        if (panelText != null)
        {
            panelText.text = _isOpen ? closeText : openText;//if _isOpen is true return closeText or else return openText
        }
    }


    // if raycast hits the livingroom door collider, set _isInsideTrigger to true else set to false
    void _RaycastHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 2))
        {
            if (hit.collider.gameObject.tag == "door_Livingroom" || hit.collider.gameObject.tag == "slot_Livingroom")
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
        }
    }

    //if _isInsideTrigger is true and mouse is pressed, open livingroom door deactiviate the livingroom door panel
    void InsideTrigger()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) //stop raycast on UI clicks. when UI is activ, gameObjects arent hit with raycast.
        {
            if (inTrigger == true)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    _animator.SetBool("open_CorToLivDoor", true);
                    openPanel.SetActive(false);

                    openPanel = null;
                    FindObjectOfType<SFX_Manager>().Play("doorOpen");

                    computerLocationEff.Play();
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        _RaycastHit();
        InsideTrigger();
    }
}
