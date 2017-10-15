using UnityEngine;
public class DoorOutsideScript : MonoBehaviour {

    public Animator _animator;

    public Camera fpsCam;

    public GameObject OpenPanel;
    public GameObject panel;//access to go outside denied
    public GameObject mouse;

    public string openText = "access denied";
    public string closeText = "";

    public bool inTrigger;
    private bool _isOpen;
    public static bool weaponObtained;

    // Use this for initialization
    void Start ()
    {
        _animator = GetComponent<Animator>();
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
                            if (weaponObtained)
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
                            if (weaponObtained)
                            {
                                //do nothing
                            }
                            else
                            {
                                inTrigger = false;
                                OpenPanel.SetActive(false);
                                panel.SetActive(false);//access to go outside denied
            }
        }

        //if _isInsideTrigger is true and mouse is pressed open outside door
        if (inTrigger)
        {

            if (weaponObtained == false)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    panel.SetActive(true);//access to go outside denied 
                    FindObjectOfType<SFX_Manager>().Play("error");                    
                    _animator.SetBool("OutsideSlot_showError", true);
                    _animator.SetBool("open_OutsideDoor", false);
                    // OpenPanel.SetActive(false);
                    //OpenPanel = null;
                }
            }
        }

        //if weapon obtained, open the ouside door and show no slot errors
        if (weaponObtained)
        {
            _animator.SetBool("OutsideSlot_showError", true);
            _animator.SetBool("OutsideSlot_showNoError", true);
            _animator.SetBool("open_OutsideDoor", true);
        }
    }
}
