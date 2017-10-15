using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MicrowaveScript : MonoBehaviour {


    public Animator _animator;

    public Collider microwaveCollider;

    public Camera fpsCam;

    public GameObject openPanel;

    public string openText = "Press the left mouse button to open slider";
    public string closeText = "Press the left mouse button to close slider";

    private bool _isOpen;
    private bool _isInsideTrigger;

    // Use this for initialization
    void Start ()
    {
        _animator = GetComponent<Animator>();
    
    }


    // for checking if the microwave panel is activ
    private bool IsOpenPanelActive
    {
        get
        {
            return openPanel.activeInHierarchy;
        }
    }

    // for updating the microwave panel text
    private void UpdatePanelText()
    {
        UnityEngine.UI.Text panelText = openPanel.transform.FindChild("Text").GetComponent<UnityEngine.UI.Text>();
        if (panelText != null)
        {
            panelText.text = _isOpen ? closeText : openText;//if _isOpen is true return closeText or else return openText
        }
    }


    // if raycast hits the microwave door collider, set _isInsideTrigger to true else set to false
    void _RaycastHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 2))
        {
            if (hit.collider.gameObject.tag == "door_Microwave")
            {
                _isInsideTrigger = true;
                UpdatePanelText();
                openPanel.SetActive(true);
            }


        }
        else
        {
            _isInsideTrigger = false;
            openPanel.SetActive(false);
        }

    }
    //if microwave panel is active, _isInsideTrigger is true and mouse is pressed then open the microwave door
    void InsideTrigger()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) //stop raycast on UI clicks. when UI is activ, gameObjects arent hit with raycast.
        {
            // when panel is visible show text 
            if (IsOpenPanelActive && _isInsideTrigger)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    _animator.SetBool("open_Microwave", true);
                    openPanel.SetActive(false);
                    microwaveCollider.enabled = false;
                    FindObjectOfType<SFX_Manager>().Play("openMicrowave");
                }
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
        _RaycastHit();
        InsideTrigger();
    }
}
