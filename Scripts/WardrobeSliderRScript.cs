using UnityEngine;
using UnityEngine.EventSystems;
public class WardrobeSliderRScript : MonoBehaviour
{

    public Animator _animator;
    public GameObject openPanel = null;// show panel when player gets near the wardrobeslider
    public GameObject keycard_Bedroom; //keycard
    public Collider wadrobeCollider;//deactivate the collider when slider is open
    public Camera fpsCam;// raycast

    private bool _isInsideTrigger = false;

    public string openText = "open slider";
    public string closeText = "close slider";

    private bool _isOpen = false;



    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // for checking if the wardrobeslider panel is activ
    private bool IsOpenPanelActive
    {
        get
        {
            return openPanel.activeInHierarchy;
        }
    }

    // for updating the wardrobeslider panel text    
    private void UpdatePanelText()
    {
        UnityEngine.UI.Text panelText = openPanel.transform.FindChild("Text").GetComponent<UnityEngine.UI.Text>();
        if (panelText != null)
        {
            panelText.text = _isOpen ? closeText : openText;//if _isOpen is true return CloseText or else return openText
        }
    }

    // if raycast hits the wardrobeslider collider, set _isInsideTrigger to true else set to false
    void _RaycastHit()
    {

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 2))
        {
            if (hit.collider.gameObject.tag == "WardrobeSliderR")
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

    void InsideTrigger()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) //stop raycast on UI clicks. when UI is activ, gameObjects arent hit with raycast.
        {

            // if the wardrobeslider panel is activ and if _isInsideTrigger is true
            if (IsOpenPanelActive && _isInsideTrigger)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    _animator.SetBool("open_WardrobeSliderR", true);
                    openPanel.SetActive(false);
                    keycard_Bedroom.SetActive(true);
                    wadrobeCollider.enabled = false;
                    FindObjectOfType<SFX_Manager>().Play("wardrobeOpen");
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



