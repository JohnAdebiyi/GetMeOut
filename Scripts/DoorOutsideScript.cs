using UnityEngine;
public class DoorOutsideScript : MonoBehaviour {

    public Animator _animator;
    public GameObject OpenPanel = null;
    public Camera fpsCam;
    public GameObject mouse;

    public bool inTrigger;

    public string openText = "access denied";
    public string closeText = "";

    private bool _isOpen = false;
    public static bool weaponObtained = false;

    // Use this for initialization
    void Start ()
    {
        _animator = GetComponent<Animator>();
    }


    // for checking if the outside door panel is activ
    private bool IsOpenPanelActive
    {
        get
        {
            return OpenPanel.activeInHierarchy;
        }
    }

    // for updating the outside door panel text
    private void UpdatePanelText()
    {
        UnityEngine.UI.Text panelText = OpenPanel.transform.FindChild("Text").GetComponent<UnityEngine.UI.Text>();
        if (panelText != null)
        {
            panelText.text = _isOpen ? closeText : openText;//if _isOpen is true return closeText or else return openText
        }
    }

    // Update is called once per frame
    void Update()
    {

        // if raycast hits the outside door collider, set _isInsideTrigger to true else set to false
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 2))
        {
            if (hit.collider.gameObject.tag == "door_Outside" || hit.collider.gameObject.tag == "slot_Outside")
            {
                            mouse.SetActive(false);
                            if (weaponObtained == true)
                            {
                                //do nothing
                            }
                            else
                            {
                                inTrigger = true;
                                UpdatePanelText();
                                OpenPanel.SetActive(true);                                
                            }
            }
        }
        else
        {
                            mouse.SetActive(true);
                            if (weaponObtained == true)
                            {
                                //do nothing
                            }
                            else
                            {
                                inTrigger = false;
                                OpenPanel.SetActive(false);
                            }
        }

        //if _isInsideTrigger is true and mouse is pressed open outside door
        if (inTrigger == true)
        {

            if (weaponObtained == false)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    FindObjectOfType<SFX_Manager>().Play("error");
                    _animator.SetBool("OutsideSlot_showError", true);
                    _animator.SetBool("open_OutsideDoor", false);
                    // OpenPanel.SetActive(false);
                    //OpenPanel = null;
                }
            }
        }

        //if weapon obtained, open the ouside door and show no slot errors
        if (weaponObtained == true)
        {
            _animator.SetBool("OutsideSlot_showError", true);
            _animator.SetBool("OutsideSlot_showNoError", true);
            _animator.SetBool("open_OutsideDoor", true);
        }
    }
}
