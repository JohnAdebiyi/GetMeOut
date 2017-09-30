using UnityEngine;
using UnityEngine.EventSystems;
public class Terminal_1Script : MonoBehaviour {

    public Animator _animator;
    public GameObject openPanel = null;
    public GameObject destroyKeycardMinimalized005;// important! using Tag to destroy // for destroying the panel keycard obtainded
    public Camera fpsCam;
    public GameObject interf;//interface effect
    public GameObject pointerToBridge;
    public GameObject pointerToTerminal;
    public GameObject keycard_inserted;
    public ParticleSystem bridgeEntranceEff;
    public GameObject panel_insertTheCorrectCard;

    public bool inTrigger;
    public string openText = "Insert keycard to activate Terminal";
    public string closeText = "";
    private bool _isOpen = false;
    public static bool keyCardTerminal1 = false;// if player has a card set to true
    private bool interfaceHasAlreadyBeenActivated = false;

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();        
    }


    // for checking if the terminal panel is activ
    private bool IsOpenPanelActive
    {
        get
        {
            return openPanel.activeInHierarchy;
        }
    }

    // for updating the terminal panel text 
    private void UpdatePanelText()
    {
        UnityEngine.UI.Text panelText = openPanel.transform.FindChild("Text").GetComponent<UnityEngine.UI.Text>();
        if (panelText != null)
        {
            panelText.text = _isOpen ? closeText : openText;//if _isOpen is true return closeText or else return openText
        }
    }


    // if raycast hits the terminal collider, set _isInsideTrigger to true else set to false
    void _RaycastHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 2))
        {
            if (hit.collider.gameObject.tag == "slot_Terminal1" || hit.collider.gameObject.tag == "screen_Terminal1")
            {
                
                inTrigger = true;
                UpdatePanelText();
                openPanel.SetActive(true);
            }
        }
        else
        {
            panel_insertTheCorrectCard.SetActive(false);            
            inTrigger = false;
            openPanel.SetActive(false);
        }

    }
    //if _isInsideTrigger is true, key card is obtainded and mouse is pressed
    void InsideTrigger()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) //stop raycast on UI clicks. when UI is activ, gameObjects arent hit with raycast.
        {
            if (inTrigger == true)
            {
                if (keyCardTerminal1)
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        FindObjectOfType<SFX_Manager>().Play("openInterface");
                        _animator.SetBool("Terminal1Slot_Error", true);
                        _animator.SetBool("Terminal1Slot_NoError", true);
                        _animator.SetBool("Terminal_On", true);
                        openPanel.SetActive(false);
                        openPanel = null;
                        interf.SetActive(true);
                        interfaceHasAlreadyBeenActivated = true;
                        BridgeActivationScript.bridgeIsActiv = true;
                        bridgeEntranceEff.Play();
                        pointerToBridge.SetActive(true);
                        pointerToTerminal.SetActive(false);
                        keycard_inserted.SetActive(true);
                    }
                }else
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        FindObjectOfType<SFX_Manager>().Play("error");
                        _animator.SetBool("Terminal1Slot_Error", true);
                        panel_insertTheCorrectCard.SetActive(true);
                    }
                }
            }
        }
    }

    // destroy the mini panel "keycard obtainded" when terminal is activ
    void DestroyPanel()
    {
        destroyKeycardMinimalized005 = GameObject.FindGameObjectWithTag("keycard005");
        if (_animator.GetBool("Terminal_On") == true)
        {
            Destroy(destroyKeycardMinimalized005);            
        }
    }



    void OnTriggerExit(Collider other)
    {
        if (interfaceHasAlreadyBeenActivated)
        {
            if (other.tag == "Player")
            {          
                interf.SetActive(false);
                FindObjectOfType<SFX_Manager>().Play("closeInterface");
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (interfaceHasAlreadyBeenActivated)
        {
            if (other.tag == "Player")
            {       
                interf.SetActive(true);
                FindObjectOfType<SFX_Manager>().Play("openInterface");
            }
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
