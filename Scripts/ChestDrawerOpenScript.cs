using UnityEngine;
using UnityEngine.EventSystems;
public class ChestDrawerOpenScript : MonoBehaviour
{

    public Animator _animator;

    public Camera fpsCam;

    public GameObject paper;//paper.png
    public GameObject panel_Chestdrawer;//open drawer, close drawer
    public GameObject openPanel_ToReadPaper;
    public GameObject Keypad_Bedroom;

    public string openText = "open drawer";
    public string closeText = "close drawer";

    private bool _isInsideTrigger;
    private bool _isOpen = false;

    private float time;//timer for letting paper appear after 0.7 sec after drawer has been opened






    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
        paper.SetActive(false);
        time = Time.time;
    }



    // for checking if the drawer panel is activ
    private bool IsOpenPanelActive
    {
        get
        {
            return panel_Chestdrawer.activeInHierarchy;
        }
    }

    // for updating the drawer panel text 
    private void UpdatePanelText()
    {
        UnityEngine.UI.Text panelText = panel_Chestdrawer.transform.FindChild("Text").GetComponent<UnityEngine.UI.Text>();
        if (panelText != null)
        {
            panelText.text = _isOpen ? closeText : openText;//if _isOpen is true return closeText or else return openText
        }
    }


    // if raycast hits the chestdrawer collider, set _isInsideTrigger to true else set to false
    void _RaycastHit()
    {

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 3))
        {
            if (hit.collider.gameObject.tag == "chestdrawer_Bedroom")
            {
                _isInsideTrigger = true;
                UpdatePanelText();
                panel_Chestdrawer.SetActive(true);
            }else
                {
                    _isInsideTrigger = false;
                    panel_Chestdrawer.SetActive(false);
                    openPanel_ToReadPaper.SetActive(false);
                }
            if (hit.collider.gameObject.tag == "safe_Bedroom")// in case raycast hits the bedroom Safe then deactivate the chestdrawer or paper panel
            {
                panel_Chestdrawer.SetActive(false);
                openPanel_ToReadPaper.SetActive(false);
            }
        }
        else
        {
            _isInsideTrigger = false;
            panel_Chestdrawer.SetActive(false);
            openPanel_ToReadPaper.SetActive(false);
        }
    }



    void InsideTrigger()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) //stop raycast on UI clicks. when UI is activ, gameObjects arent hit with raycast.
        {
            // if the chestdrawer panel is activ and if _isInsideTrigger is true
            if (IsOpenPanelActive && _isInsideTrigger)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    _isOpen = !_isOpen;

                    UpdatePanelText();

                    _animator.SetBool("open", _isOpen);
                    FindObjectOfType<SFX_Manager>().Play("chestDrawer");                    
                }
            }


            if (_animator.GetBool("open"))
            {
                if (Time.time >= time + 0.7f)// time starts running when the drawer is opened. if time reaches 0.7sec then show paper
                {
                    openPanel_ToReadPaper.SetActive(true);
                    paper.SetActive(true);
                }
            }
            else// if panel is not opened put time to 0
            {
                openPanel_ToReadPaper.SetActive(false);
                paper.SetActive(false);
                time = Time.time;
            }

            if (_isInsideTrigger == false)
            {
                openPanel_ToReadPaper.SetActive(false);
                panel_Chestdrawer.SetActive(false);
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
