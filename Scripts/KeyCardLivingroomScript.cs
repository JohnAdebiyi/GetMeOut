using UnityEngine;
using UnityEngine.EventSystems;
public class KeyCardLivingroomScript : MonoBehaviour
{

    public bool inTrigger;
    public GameObject openPanel = null;
    public GameObject shownMinimizedKeycard;
    public Camera fpsCam;
    public GameObject keycard;

    public ParticleSystem computerLocationEff;// in case first game wasnt played then stop the effect when keycard has been obtained

    public string openText = "Take key card";
    public string closeText = "";

    private bool _isOpen = false;


    // for checking if the key card panel is activ
    private bool IsOpenPanelActive
    {
        get
        {
            return openPanel.activeInHierarchy;
        }
    }

    // for updating the key card panel text
    private void UpdatePanelText()
    {
        UnityEngine.UI.Text panelText = openPanel.transform.FindChild("Text").GetComponent<UnityEngine.UI.Text>();
        if (panelText != null)
        {
            panelText.text = _isOpen ? closeText : openText;//if _isOpen is true return closeText or else return openText
        }
    }
    // if raycast hits the key card collider, set _isInsideTrigger to true else set to fals
    void _RaycastHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 3))
        {
            if (hit.collider.gameObject.tag == "keycard_Livingroom")
            {
                inTrigger = true;
                UpdatePanelText();
                openPanel.SetActive(true);
            }
        }
        else
        {
            openPanel.SetActive(false);
            inTrigger = false;
        }

    }
    //if _isInsideTrigger is true and mouse is pressed destroy key card and display "key card obtained" 
    void InsideTrigger()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) //stop raycast on UI clicks. when UI is activ, gameObjects arent hit with raycast.
        {
            // when panel is visible show text 
            if (inTrigger)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    Laptop_GameScript.putOffPanel_Game1 = true;// in case first game wasnt played then dont show the panel
                    computerLocationEff.Stop();// in case first game wasnt played then stop the effect when keycard has been obtained
                    DoorBathroomScript.keyCard_To_Bathroom = true;
                    keycard.SetActive(false);
                    openPanel.SetActive(false);
                    shownMinimizedKeycard.SetActive(true);
                    Timer.countKeycards += 1;

                    FindObjectOfType<SFX_Manager>().Play("gotItem");
                }
            }
        }
    }

    void Update()
    {
        _RaycastHit();
        InsideTrigger();
    }
}