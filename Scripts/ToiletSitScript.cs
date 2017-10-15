using UnityEngine;
using UnityEngine.EventSystems;
public class ToiletSitScript : MonoBehaviour
{


    public Animator _animator;

    public Camera fpsCam;

    public Collider toiletSitCollider;

    public GameObject openPanel;
    public GameObject keycard_Toilet;

    public string openText = "Open toilet seat";
    public string closeText = "";

    private bool _isOpen;
    private bool _isInsideTrigger;


    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
    }


    // for checking if the toilet lid panel is activ
    private bool IsOpenPanelActive
    {
        get
        {
            return openPanel.activeInHierarchy;
        }
    }

    // for updating the toilet lid panel text
    private void UpdatePanelText()
    {
        UnityEngine.UI.Text panelText = openPanel.transform.FindChild("Text").GetComponent<UnityEngine.UI.Text>();
        if (panelText != null)
        {
            panelText.text = _isOpen ? closeText : openText;//if _isOpen is true return CloseText or else return openText
        }
    }


    // if raycast hits the toiletsit collider, set _isInsideTrigger to true else set to false
    void _RaycastHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 3))
        {
            if (hit.collider.gameObject.tag == "ToiletSit")
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

    //if _isInsideTrigger is true and mouse is pressed open toilet lid
    void InsideTrigger()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) //stop raycast on UI clicks. when UI is activ, gameObjects arent hit with raycast.
        { 
                if (IsOpenPanelActive && _isInsideTrigger)
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        _animator.SetBool("open_ToiletSeat", true);
                        keycard_Toilet.SetActive(true);
                        toiletSitCollider.enabled = false;
                        FindObjectOfType<SFX_Manager>().Play("toiletSeatUp");
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

